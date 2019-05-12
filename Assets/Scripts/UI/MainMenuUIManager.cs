using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(ClickAction);
    }

    void ClickAction()
    {
        SceneManager.LoadScene("Gameplay");
    }
}