using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshPro loseText;
    public TextMeshPro winText;
    public GameObject restartButton;

    private void Start()
    {
        // Начинаем с текста "YOU LOSE" и кнопки "Рестарт" отключенными
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Вызывается, когда игрок проиграл
    public void ShowLoseScreen()
    {
        loseText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    
    public void ShowWinScreen()
    {
        winText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "restartButton")
        {
            // Получаем номер текущей сцены
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    
            // Перезагружаем текущую сцену
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}