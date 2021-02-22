using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour 
{
    [Header("HUD UI Elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Transform strikeSprites;
    [SerializeField] private Slider fruitSlider;
    [SerializeField] private Slider veggieSlider;

    [Header("Game Over UI Elements")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text highestScoreText;

    [Header("Baskets")]
    [SerializeField] private Transform fruitBasket;
    [SerializeField] private Transform veggieBasket;

    public static int strikes = 0;
    public static int score = 0;

    private string highestScoreName;
    private int highestScore;

    private void Start()
    {
        gameOverScreen.SetActive(false);

        //on startup, check if highest score exists and if so, load it into private variables
        if (PlayerPrefs.HasKey("HighestScore"))
        {
            highestScoreName = PlayerPrefs.GetString("HighestScoreName");
            highestScore = PlayerPrefs.GetInt("HighestScore");
            highestScoreText.text = highestScoreName + "........................" + highestScore.ToString();
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();

        if(strikes >= 1)
        {
            strikeSprites.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            if(strikes >= 2)
            {
                strikeSprites.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                if(strikes >= 3)
                {
                    strikeSprites.GetChild(3).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    //3 strikes, you're out! (GAME OVER)

                    //first disable gameplay input
                    fruitSlider.interactable = false;
                    veggieSlider.interactable = false;

                    //next, setup UI elements on game over screen
                    finalScoreText.text = "Final Score: " + score.ToString();
                    HighestScoreCheck();

                    //lastly, turn on game over screen
                    gameOverScreen.SetActive(true);
                }
            }
        }
    }
    
    public void FruitSliderChanged(float value)
    {
        fruitBasket.position = new Vector3(value, -2, 0);
    }

    public void VeggieSliderChanged(float value)
    {
        veggieBasket.position = new Vector3(value, -2, 0);
    }

    public void PlayAgain()
    {
        strikes = 0;
        score = 0;
        SceneManager.LoadScene(1); 
    }

    public void ReturnToMainMenu()
    {
        strikes = 0;
        score = 0;
        SceneManager.LoadScene(0);
    }

    private void HighestScoreCheck()
    {
        if(highestScoreName == null || score > highestScore)
        {
            //if no previous highest score or final score is higher, player set new record
            PlayerPrefs.SetString("HighestScoreName", MainMenu.currentUsername);
            PlayerPrefs.SetInt("HighestScore", score);

            //this bit may be uneccessary but better safe than sorry
            highestScoreName = MainMenu.currentUsername;
            highestScore = score;

            highestScoreText.text = MainMenu.currentUsername + "........................" + score.ToString();

            finalScoreText.transform.GetChild(0).gameObject.SetActive(true); //<--also remember to turn on "new record" text
        }
    }
}
