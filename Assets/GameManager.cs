﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreField;
    [SerializeField] TextMeshProUGUI shieldTimer;
    [SerializeField] private Slider healthSlider;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject howToPlay;
    [SerializeField] GameObject fleet;
    [SerializeField] GameObject passingCrafts;
    [SerializeField] private GameObject player;
    public int score = 0;

    public float timer = 60;

    public float timePassed = 0;

    public bool gameStarted = false;

    public bool gamePaused = false;

    public bool gameEnd = false;

    public int powerupsCollected = 0;

    public int buffsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreField.text = "000000";
        scoreField.gameObject.SetActive(false);
        shieldTimer.gameObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        howToPlay.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void FixedUpdate()
    {
        if (gameStarted && !gamePaused)
        {
            timePassed += Time.deltaTime;
            if (timer < 0) {
                timer = 60;
            }
            else
            {
                timer -= Time.deltaTime;
            }   
        }

        if (timePassed > 60)
        {
            scoreField.gameObject.SetActive(false);
            shieldTimer.gameObject.SetActive(false);
            healthSlider.gameObject.SetActive(false);
            gameEnd = true;
            gameStarted = false;
            timer = 60;
            
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        SetPrettifiedScore(score);
    }

    public void RefreshBarrier()
    {
        timer = 60;
    }

    private void SetPrettifiedScore(int points)
    {
        var newPointsString = "";
        var pointsToString = points.ToString();
        var pointsCharCount = pointsToString.Length;
        var zerosToAdd = 6 - pointsCharCount;
        for (int i = 0; i < zerosToAdd; i++)
        {
            newPointsString += "0";
        }

        newPointsString += points;

        scoreField.text = newPointsString;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameStarted = true;
        scoreField.gameObject.SetActive(true);
        shieldTimer.gameObject.SetActive(true);
        healthSlider.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void ReturnToMenu()
    {
        pauseMenu.SetActive(false);
        howToPlay.SetActive(false);
        scoreField.gameObject.SetActive(false);
        shieldTimer.gameObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        gameStarted = false;
        timer = 60;
        score = 0;
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
       Application.Quit();
    }
}