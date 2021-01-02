using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CircleSurvival
{
    public class MainMenuUIManager : MonoBehaviour
    {
        public Button playButton;
        public GameObject highscoreDisplay;
        public Button resetHighscoreButton;

        void Start()
        {
            highscoreDisplay.GetComponent<HighscoreDisplay>().CheckHighscore();
            playButton.onClick.AddListener(ClickAction);
            resetHighscoreButton.onClick.AddListener(ClickActionReset);
        }

        void ClickAction()
        {
            SceneManager.LoadScene("Gameplay");
        }
        void ClickActionReset()
        {
            ScoreSavingSysytem.SaveHighscore(-1f);
            highscoreDisplay.GetComponent<HighscoreDisplay>().CheckHighscore();
        }
    }
}