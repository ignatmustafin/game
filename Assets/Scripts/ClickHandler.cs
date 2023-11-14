using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.EventSystems;
using Newtonsoft.Json;


public class ClickHandler : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public Animator animator;
    private float targetXPosition = 19.5f; // Целевая позиция по оси X
    private float targetYPosition = 1.8f; // Целевая позиция по оси X
    private float targetZPosition = -44.5f; // Целевая позиция по оси X

    public GameObject cat;
    public Material mineMaterial;
    public Material successMaterial;
    public Material activeMaterial;
    public Material activeMaterialForChildren;

    private bool isMoving = false;
    private Transform targetTransform;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    async void Start()
    {
        

    }

    async void Awake()
    {
        await GetToken();
        await TestToken();
    }

    // void Update()
    // {
    //     // Проверяем, нужно ли перемещаться
    //     if (isMoving)
    //     {
    //         Debug.Log("IN MOVING");
    //         // Вычисляем расстояние между текущей позицией и целевой позицией
    //         float distance = Vector3.Distance(cat.transform.position, targetTransform.position);
    //
    //         // Если мы не достигли цели, продолжаем перемещение
    //         if (distance > 0.01f)
    //         {
    //             Debug.Log(moveSpeed * Time.deltaTime);
    //             // Вычисляем новую позицию, используя Lerp для плавного перемещения
    //             Vector3 newPosition = Vector3.Lerp(cat.transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
    //             cat.transform.position = newPosition;
    //         }
    //         else
    //         {
    //             // Если достигли цели, завершаем перемещение
    //             isMoving = false;
    //         }
    //     }
    // }

    private async void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject clickedObject = null;

        // Проверяем, пересек ли луч какой-либо объект
        if (Physics.Raycast(ray, out hit))
        {
            // hit.collider содержит информацию о коллайдере объекта, с которым было взаимодействие
            clickedObject = hit.collider.gameObject;
            Debug.Log($"Clicked on: {clickedObject.name}");
        }

        if (clickedObject != null)
        {
            targetTransform = clickedObject.transform;
            StartCoroutine(MoveAndJumpCoroutine());
        }

        var tagName = gameObject.tag;
        string numberPart = tagName.Substring(3);
        int rowNumber = int.Parse(numberPart);

        var objectGrid = ObjectCollection._instance.GetParamsForGameObject(gameObject.name);

        var stepForwardResponse = await StepForward(2, objectGrid.Row, objectGrid.Column);
        if (stepForwardResponse.success)
        {
            var minesOnRow = stepForwardResponse.minesOnRow;
            var objects = GameObject.FindGameObjectsWithTag(tagName);
            List<GameObject> rowObjectsList = new List<GameObject>(objects);

            rowObjectsList.Sort((a, b) => string.Compare(a.name, b.name));

            var mineIndex = Array.FindIndex(minesOnRow, mine => mine);

            var gameObjectWithMine = rowObjectsList[mineIndex];
            Debug.Log(gameObjectWithMine.name);

            if (!gameObjectWithMine)
            {
                throw new Exception("No mine, game error");
            }

            var gameObjectWithMineRenderer = gameObjectWithMine.GetComponent<Renderer>();
            Debug.Log(gameObjectWithMine.name);

            if (gameObjectWithMineRenderer != null)
            {
                var materials = gameObjectWithMineRenderer.materials;
                materials[1] = mineMaterial;
                gameObjectWithMineRenderer.materials = materials;
            }

            if (clickedObject != null)
            {
                Debug.Log(clickedObject);
                var pushedGoRenderer = clickedObject.GetComponent<Renderer>();
                if (pushedGoRenderer != null)
                {
                    var materials = pushedGoRenderer.materials;
                    materials[1] = successMaterial;
                    pushedGoRenderer.materials = materials;
                }
            }

            if (stepForwardResponse.game?.isFinished == true & stepForwardResponse.game?.playerWin == true)
            {
                UIManager uiManager = FindObjectOfType<UIManager>(); // Поиск UIManager в сцене
                if (uiManager != null)
                {
                    uiManager.ShowWinScreen(); // Вызываем метод ShowLoseScreen
                    return;
                }
                else
                {
                    Debug.LogWarning("UIManager не найден в сцене.");
                }
            }


            var newRowNumber = rowNumber + 1;
            var newRowTag = $"Row{newRowNumber}";

            var nextRowObjects = GameObject.FindGameObjectsWithTag(newRowTag);
            List<GameObject> nextRowObjectsList = new List<GameObject>(nextRowObjects);
            nextRowObjectsList.Sort((a, b) => string.Compare(a.name, b.name));

            foreach (var go in nextRowObjectsList)
            {
                foreach (Transform childTransform in go.transform)
                {
                    // childTransform - это компонент Transform дочернего объекта
                    GameObject child = childTransform.gameObject;

                    var childRenderer = child.GetComponent<Renderer>();

                    var materials = childRenderer.materials;
                    materials[0] = activeMaterialForChildren; // Заменяем первый материал
                    childRenderer.materials = materials;
                }


                // Получаем рендерер island
                var goRenderer = go.GetComponent<Renderer>();

                // Заменяем материалы
                if (goRenderer != null)
                {
                    var materials = goRenderer.materials;
                    materials[1] = activeMaterial; // Заменяем первый материал
                    goRenderer.materials = materials;
                }
            }
        }
        else
        {
            Debug.Log("YOU LOST");
            var game = stepForwardResponse.game;
            var closedRows = game.minesGrid.GetRange(rowNumber, game.minesGrid.Count - rowNumber);

            foreach (var row in closedRows)
            {
                var newRowNumber = game.minesGrid.IndexOf(row);
                var newRowTag = $"Row{newRowNumber}";

                var objects = GameObject.FindGameObjectsWithTag(newRowTag);
                List<GameObject> rowObjectsList = new List<GameObject>(objects);

                rowObjectsList.Sort((a, b) => string.Compare(a.name, b.name));

                var mineIndex = row.IndexOf(true);

                var gameObjectWithMine = rowObjectsList[mineIndex];

                if (!gameObjectWithMine)
                {
                    throw new Exception("No mine, game error");
                }

                Renderer gameObjectWithMineRenderer = gameObjectWithMine.GetComponent<Renderer>();

                if (gameObjectWithMineRenderer != null)
                {
                    var materials = gameObjectWithMineRenderer.materials;
                    materials[1] = mineMaterial; // Заменяем второй материал
                    gameObjectWithMineRenderer.materials = materials;
                }
            }

            UIManager uiManager = FindObjectOfType<UIManager>(); // Поиск UIManager в сцене
            if (uiManager != null)
            {
                uiManager.ShowLoseScreen(); // Вызываем метод ShowLoseScreen
            }
            else
            {
                Debug.LogWarning("UIManager не найден в сцене.");
            }
        }
    }

    public static async Task<StepForwardResponse> StepForward(int userId, int row, int column)
    {
        HttpService _http = new();
        var qwe = await _http.Post("/api/game/stepForward",
            new StepForwardRequest {gameId = ObjectCollection.gameId, row = row, column = column, userId = userId});
        Debug.Log(qwe);
        StepForwardResponse response = JsonConvert.DeserializeObject<StepForwardResponse>(qwe);
        return response;
    }
    
    public static async Task<string> GetToken()
    {
        HttpService http = new();
        var response = await http.TestGet("/api/game/get");
        Debug.Log($"GET: {response}");
        return response;
    }
    
    public static async Task<string> TestToken()
    {
        HttpService http = new();
        var response = await http.TestGet("/api/game/test");
        Debug.Log($"TEST: {response}");
        return response;
    }

    private IEnumerator MoveAndJumpCoroutine()
    {
        // Устанавливаем флаг перемещения
        isMoving = true;

        // Получаем начальную позицию и целевую позицию
        Vector3 startPos = cat.transform.position;
        Vector3 endPos = targetTransform.position;
        endPos.x += 2;
        endPos.y += 3;
        endPos.z -= 3;

        // Время начала движения
        float startTime = Time.time;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float journeyTime = 1.0f;

        while (Time.time - startTime < journeyTime)
        {
            // Определяем, где находится анимация в текущий момент
            float fractionOfJourney = (Time.time - startTime) / journeyTime;

            // Вычисляем высоту перемещения с использованием AnimationCurve
            float yOffset = animationCurve.Evaluate(fractionOfJourney) * 2;

            // Вычисляем новую позицию объекта
            Vector3 newPosition = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            newPosition.y += yOffset; // Добавляем смещение по высоте

            // Перемещаем объект
            cat.transform.position = newPosition;

            yield return null;
        }

        // Завершаем перемещение
        isMoving = false;
    }


    public void MoveToTarget()
    {
        // MoveAndJumpCoroutine();
    }
}

class StartGameRequest
{
    public int UserId;
    public int Rows;
    public int Columns;

    public StartGameRequest(int userId, int rows, int columns)
    {
        UserId = userId;
        Rows = rows;
        Columns = columns;
    }
}

class StepForwardRequest
{
    public int gameId;
    public int userId;
    public int row;
    public int column;
}

[Serializable]
public class StepForwardResponse
{
    public bool success;
    public bool[] minesOnRow;
    public Game game;
}

[Serializable]
public class Game
{
    public int id;
    public int userId;
    public bool isFinished;
    public int rows;
    public int columns;
    public int currentRow;
    public List<List<bool>> minesGrid;
    public bool playerWin;
}