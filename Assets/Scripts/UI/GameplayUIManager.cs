using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    public Button switchSceneButton;
    // Start is called before the first frame update
    void Start()
    {
        switchSceneButton.onClick.AddListener(ClickAction);
    }

    void ClickAction()
    {
        Debug.Log("I AM BUTTON");
        if(SceneManager.GetActiveScene().name == "Gameplay")
        SceneManager.LoadScene("MainMenu");
        else
        SceneManager.LoadScene("Gameplay");

    }
}
