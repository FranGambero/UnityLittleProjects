using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankFSM : Singleton<TankFSM>
{
    public Estado estado;
    public Andando estadoAndando;
    public Esperando estadoEsperando;

    public float timer, maxTime;
    public bool enemigoMirando;
    public bool hazCambio = false;

    public GameObject winPanel;

    private void Start() {
        winPanel.SetActive(false);
        maxTime = Random.Range(0f, 10f);
        timer = 0;
        setState(Estado.Andando);
        estadoAndando.startState();
    }

    public void setState(Estado nuevoEstado) {
        estado = nuevoEstado;
    }

    private void Update() {

        checkState();
    }

    private void checkState() {
        switch (estado) {
            case Estado.Andando:
                //Invoke(nameof(checkEnemigo), 2f);
                enemigoMirando = checkEnemigo();
                if (enemigoMirando) {
                    estadoAndando.stopState();
                    estadoEsperando.startState();
                    timer = 0;
                    setState(Estado.Esperando);
                    hazCambio = false;
                }
                break;
            case Estado.Esperando:
                enemigoMirando = checkEnemigo();
                if (!enemigoMirando) {
                    estadoEsperando.stopState();
                    estadoAndando.startState();
                    timer = 0;
                    setState(Estado.Andando);
                }
                break;
            default:
                break;
        }
    }

    private bool checkEnemigo() {
        bool checkEnemigo = EnemigoFSM.Instance.estoyMirando;

        return checkEnemigo;
    }

    //private IEnumerator reacciona() {
    //    int waitTime = Random.Range(0, 10);
    //    Debug.Log("Voy a esperar " + waitTime);
    //    yield return new WaitForSeconds(waitTime);
    //    Debug.Log("Routina llamada, soy " + gameObject.name);
    //    hazCambio = true;
    //}

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Finish")) {
            finishGame();
        }
    }

    private void finishGame() {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
