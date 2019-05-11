using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField] Text textField;
    void Awake()
    {
        float highscore = ScoreSavingSysytem.ReadHighscore();

        textField.text = "Highscore: " + highscore;
        
    }

}
