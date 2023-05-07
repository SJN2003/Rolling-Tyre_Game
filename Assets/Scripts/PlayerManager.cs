using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    //public GameObject startingText2;


    public static int numberOfCoins;
    //public Text scoreText;
    public Text coinsText;
    //public Text myText;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            
        }
        //scoreText.text = score.ToString();
        //coinsText.text = numberOfCoins.ToString();
        //startingText.text = "Coins: " + numberOfCoins;

        if(SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }  
    }
}
