using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    private PlayerController playerController;
    public float timeLeft;
    public bool timerOn;
    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        PlayerWin();
    }
    void Timer()
    {
        if (timeLeft > 0 && !playerController.playerWin)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timeLeft);
        }
        else
        {
            timeLeft = 0;
            timerOn = false;
            GameOver();
        }
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = "Time Left: " + string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
    void PlayerWin()
    {
        if (playerController.playerWin)
        {
            GameOver();
        }
    }
}
