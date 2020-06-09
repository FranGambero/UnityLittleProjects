using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerData player;
    int errorCount = 0;

    public GameObject instructionsPanel;
    private bool showingInstructions = false;

    private void Start() {
        instructionsPanel.SetActive(false);
        getPlayer();
    }
    private void getPlayer() {
        try {
            player = SerializerManager.Instance.DeserializePlayer();
            if (player == null) {
                player = SerializerManager.Instance.SerializeBinary(new PlayerData());
            }
        } catch (System.Exception e) {
            errorCount++;
            SerializerManager.Instance.SerializeBinary(new PlayerData());
            if (errorCount < 2)
                getPlayer();
            else
                Debug.Log("Que no lo encuentro " + e.Message);
        }
    }

    public void showInstructions() {
        showingInstructions = !showingInstructions;
        instructionsPanel.SetActive(showingInstructions);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
