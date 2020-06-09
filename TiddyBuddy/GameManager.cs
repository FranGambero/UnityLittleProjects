using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public float timer, maxTime;
    public float minutes, seconds;
    public Image timeBar;
    public Text timeText, finalTimeText;
    public Text points1, points2, winnerText;

    public GameObject winPanel, gameOverPanel, instPausePanel;
    private bool gameWon, gameLost, gamePaused;
    public bool onMainMenu;
    // public Player playerObject;

    private void Awake() {
        timer = maxTime = 90f;
        //minutes = seconds = 0;
        //minutes = 2;
        //seconds = 30;

        gameWon = gameLost = gamePaused = false;
        // onMainMenu = true;

        // playerObject = GetComponent<Player>();

        if (winPanel)
            winPanel.SetActive(false);
        if (gameOverPanel)
            gameOverPanel.SetActive(false);
        if (instPausePanel)
            instPausePanel.SetActive(false);
    }
    private void Update() {
        if (!onMainMenu) {
            if (!gameLost && !gamePaused) {
                this.updateTimer();
                //this.updateCounter();
                this.updateCounterLeft();
                this.checkObjects();


                if (minutes <= 0 && seconds <= 0) {
                    this.loseGame();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.showInstruction();
        }

    }

    private void checkObjects() {
        int numObjects = GameObject.FindGameObjectsWithTag("Furniture").Length;

        // Debug.Log("Quedan " + numObjects + " objectis");

        if (numObjects <= 1 && !gameWon) {
            this.winGame();
        }
    }

    private void updateCounter() {
        if (seconds <= 59)
            seconds += Time.deltaTime;
        else {
            minutes++;
            seconds = 0;
        }

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

    }

    private void updateCounterLeft() {
        if (seconds >= 0)
            seconds -= Time.deltaTime;
        else {
            minutes--;
            seconds = 59;
        }

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

    }

    private void updateTimer() {
        if (!gamePaused) {
            timer -= Time.deltaTime;

            timeBar.fillAmount = timer / maxTime;
        }
    }

    private void winGame() {
        // Time.timeScale = 0;

        // playerObject.se

        finalTimeText.text = timeText.text;

        winPanel.SetActive(true);

        gameWon = true;
        float player1FinalCount = float.Parse(points1.text);
        float player2FinalCount = float.Parse(points2.text);
        float maxPuntuacion = Mathf.Max(player1FinalCount, player2FinalCount);

        if(player1FinalCount == player2FinalCount) {
            winnerText.text = "DRAW, BOTH PLAYERS WON";
        } else if (maxPuntuacion == player1FinalCount) {
            winnerText.text = "PLAYER 1 WON, CONGRATS MR. BEAR";
        } else {
            winnerText.text = "PLAYER 2 WON, CONGRATS BAD BUNNY";
        }

        GameObject.Find("TimePanel").SetActive(false);
    }

    private void loseGame() {
        Debug.Log("Has perdido picha");
        gameLost = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void restartGameOnePlayer() {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void goToMultiplayer() {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    public void openSelectionMode() {
        SceneManager.LoadScene(1);
    }

    public void showInstruction() {
        this.gamePaused = !gamePaused;
        Time.timeScale = (gamePaused == true) ? 0 : 1;
        instPausePanel.SetActive(this.gamePaused);
    }

    public void exitGame() {
        Application.Quit();
    }

    public void goMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
