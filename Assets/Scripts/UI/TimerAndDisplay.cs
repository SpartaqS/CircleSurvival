using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndDisplay : MonoBehaviour
{
    public float time;
    private bool tickTime = true;
    string minutes, seconds, miliseconds;

    [SerializeField] Text displayedText = null;
    [SerializeField] GameObject gameOverButton = null;
    [SerializeField] GameObject gameOverMessage = null;

    ICoroutineRunner coroutineRunner = null;
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
    }
    public void EndGameActions()
    {
        Debug.Log("Timer got the message");
        tickTime = false;
        gameOverMessage.GetComponent<Text>().text += "\n"+ minutes + ":" + seconds + ":" + miliseconds;
        coroutineRunner.StartCoroutine(ActivateEndButtonDelayed());
        float latestTime = time;
        if (ScoreSavingSysytem.ReadHighscore() < latestTime)
        {
            ScoreSavingSysytem.SaveHighscore(latestTime);
            gameOverMessage.GetComponent<Text>().text += "\nNew highscore!";
        }
        else
        {
            gameOverMessage.GetComponent<Text>().text += "\nBetter luck next time!";
        }
        gameOverMessage.SetActive(true);
    }

    IEnumerator ActivateEndButtonDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverMessage.GetComponent<Text>().text += "\n\nTap anywhere\nto continue";
        gameOverButton.SetActive(true);

    }

    public void ObtainCoroutineRunner(ICoroutineRunner coroutineRunner)
    {
        this.coroutineRunner = coroutineRunner;
    }
}
