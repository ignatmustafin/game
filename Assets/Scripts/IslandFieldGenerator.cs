using UnityEngine;

public class IslandFieldGenerator : MonoBehaviour
{
    public GameObject activeIslandPrefab; // Ссылка на префаб острова
    public GameObject inactiveIslandPrefab; // Ссылка на префаб острова
    public GameObject islandPrefabWithoutMine; // Ссылка на префаб острова
    public GameObject islandPrefabWithMine; // Ссылка на префаб острова

    public int rows; // Количество рядов
    public int columns; // Количество колонок
    public float spacingX; // Расстояние между островами по оси X
    public float spacingY; // Расстояние между островами по оси Y

    private void Start()
    {
        GenerateIslandField();
    }

    private void GenerateIslandField()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Вычисляем позицию для каждого острова
                Vector3 islandPosition = new Vector3(col * spacingX, 0f, row * spacingY);


                // Создаем остров из префаба и устанавливаем его позицию
                var island = row == 0
                    ? Instantiate(activeIslandPrefab, islandPosition, Quaternion.identity)
                    : Instantiate(inactiveIslandPrefab, islandPosition, Quaternion.identity);

                // Устанавливаем угол поворота для острова
                island.transform.eulerAngles = new Vector3(-89.98f, 0f, 0f);

                island.name = $"r{row}c{col}";

                // Присваиваем тег объекту
                island.tag = $"Row{row}";
            }
        }
    }
}