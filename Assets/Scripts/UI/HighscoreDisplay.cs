using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField] Text textField = null;
    public void CheckHighscore()
    {
        float highscore = ScoreSavingSysytem.ReadHighscore();

        if(highscore < 0f)
        textField.text = "Highscore:\nNo games finished yet!";
        else
        {
            string minutes = ((int)highscore / 60).ToString();
            string seconds = (highscore % 60).ToString("00");
            string miliseconds = ((highscore * 100) % 100).ToString("00");

            textField.text = "Highscore:\n" + minutes + ":" + seconds + ":" + miliseconds;
        }

        
    }

}
