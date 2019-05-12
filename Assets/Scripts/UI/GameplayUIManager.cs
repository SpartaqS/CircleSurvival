using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public Button mainMenuButton;
    public Button endGameButton;

    void Start()
    {
        mainMenuButton.onClick.AddListener(ClickAction);
        endGameButton.onClick.AddListener(ClickAction);
    }

    void ClickAction()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
