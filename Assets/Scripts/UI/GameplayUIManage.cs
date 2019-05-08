using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManage : MonoBehaviour
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
    }
}
