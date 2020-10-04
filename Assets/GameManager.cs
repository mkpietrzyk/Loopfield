using System;
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
    [SerializeField] GameObject goodEnding;
    [SerializeField] GameObject badEnding;
    [SerializeField] GameObject fleet;
    [SerializeField] GameObject passingCrafts;
    [SerializeField] private GameObject playerCraft;
    [SerializeField] private GameObject playerGameObject;
    public int score = 0;

    public float timer = 60;

    public float timePassed = 0;

    public bool gameStarted = false;

    public bool gamePaused = false;

    public bool gameEnd = false;

    public bool isPlayerDead = false;

    public int powerupsCollected = 0;

    public int buffsCollected = 0;

    public float fleetPositionX = -5f;
    public float craftsPosition = -50f;
    public float playerPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreField.text = "000000";
        scoreField.gameObject.SetActive(false);
        shieldTimer.gameObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        howToPlay.SetActive(false);
        goodEnding.SetActive(false);
        badEnding.SetActive(false);
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
            if (timer < 0)
            {
                timer = 60;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        if (timePassed > 15 && !isPlayerDead && gameStarted)
        {
            scoreField.gameObject.SetActive(false);
            shieldTimer.gameObject.SetActive(false);
            healthSlider.gameObject.SetActive(false);
            gameEnd = true;
            timer = 60;
            if (fleet.transform.position.x < 13f)
            {
                fleetPositionX += Time.deltaTime * 2f;
                fleet.transform.position = new Vector3(fleetPositionX, 0,0);
            }

            if (passingCrafts.transform.position.x < 50f)
            {
                craftsPosition += Time.deltaTime * 10f;
                passingCrafts.transform.position = new Vector3(craftsPosition, 0,0);
            }

            if (playerGameObject.transform.position.x < 6)
            {
                playerPosition += Time.deltaTime * 2f;
                playerGameObject.transform.position = new Vector3(playerPosition, 0,0);
            }

            if (playerGameObject.transform.position.x > 4)
            {
                goodEnding.SetActive(true);
            }
        }

        if (timePassed < 60 && isPlayerDead && gameStarted)
        {
            scoreField.gameObject.SetActive(false);
            shieldTimer.gameObject.SetActive(false);
            healthSlider.gameObject.SetActive(false);
            gameEnd = true;
            timer = 60;
            playerGameObject.SetActive(false);
            badEnding.SetActive(true);
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
        isPlayerDead = false;
        timePassed = 0;
        timer = 60;
        healthSlider.value = 100;
        playerCraft.GetComponent<PlayerController>().health = 100;
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
        passingCrafts.transform.position = new Vector3(-50f,0,0);
        fleet.transform.position = new Vector3(-18, 0 ,0);
        gameStarted = false;
        gameEnd = false;
        timer = 60;
        score = 0;
        healthSlider.value = 100;
        isPlayerDead = false;
        scoreField.text = "000000";
        pauseMenu.SetActive(false);
        howToPlay.SetActive(false);
        scoreField.gameObject.SetActive(false);
        shieldTimer.gameObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
        badEnding.SetActive(false);
        goodEnding.SetActive(false);
        mainMenu.SetActive(true);
        playerGameObject.SetActive(true);
        playerGameObject.transform.position = Vector3.zero;
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