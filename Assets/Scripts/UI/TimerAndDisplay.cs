using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndDisplay : MonoBehaviour
{
    public float time;
    private bool tickTime = true;
    string minutes, seconds, miliseconds;

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
        if (tickTime)
        {
            time += Time.deltaTime;

            minutes = ((int)time / 60).ToString();
            seconds = (time % 60).ToString("00");
            miliseconds = ((time *100)%100).ToString("00");

            displayedText.text = "Time: " + minutes + ":" + seconds + ":" + miliseconds;
        }
        else
        {
            float latestTime = time;
            if (ScoreSavingSysytem.ReadHighscore() < latestTime)
            {
                ScoreSavingSysytem.SaveHighscore(latestTime);
                displayedText.text = "Time: " + minutes + ":" + seconds + ":" + miliseconds + "\nNew highscore!";
            }
            else
            {
                displayedText.text = "Time: " + minutes + ":" + seconds + ":" + miliseconds+ "\nBetter luck next time!";
            }
        }


    }
    public void EndGameActions()
    {
        Debug.Log("Timer got the message");

        tickTime = false;

    }
}
