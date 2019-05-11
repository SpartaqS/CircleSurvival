using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndDisplay : MonoBehaviour
{
    float time;

    [SerializeField] Text displayedText;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        displayedText.text = "Time: ";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        string minutes = ((int)time / 60).ToString();
        string seconds = (time % 60).ToString("00");
        string miliseconds = ((time *100)%100).ToString("00");

        displayedText.text = "Time: " + minutes + ":" + seconds + ":" + miliseconds;
    }
}
