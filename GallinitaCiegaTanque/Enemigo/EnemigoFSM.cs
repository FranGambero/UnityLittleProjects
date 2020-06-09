using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoFSM : Singleton<EnemigoFSM>
{
    public Estado estado;
    public Mirando estadoMirando;
    public Contando estadoContando;

    public float timer, maxTime;
    public bool estoyMirando = true;

    private void Start() {
        maxTime = Random.Range(0f, 6f);
        timer = 0;
        setState(Estado.Mirando);
        estadoMirando.startState();
    }

    [ContextMenu("Cambia")]
    private void changeEstado() {
        estoyMirando = !estoyMirando;
        timer = 0;
        maxTime = Random.Range(0f, 6f);
    }

    private void setState(Estado nuevoEstado) {
        estado = nuevoEstado;
    }

    private void Update() {
        if (timer >= maxTime) {
            // Va a cambiar el estado
            changeEstado();
        } else {
            timer += Time.deltaTime;
        }
        checkState();
    }

    private void checkState() {
        switch (estado) {
            case Estado.Contando:
                if (estoyMirando) {
                    estadoContando.stopState();
                    estadoMirando.startState();
                    setState(Estado.Mirando);
                }
                break;
            case Estado.Mirando:
                if (!estoyMirando) {
                    estadoMirando.stopState();
                    estadoContando.startState();
                    setState(Estado.Contando);
                }
                break;
            default:
                break;
        }
    }

}
