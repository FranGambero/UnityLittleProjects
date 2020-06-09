using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esperando : MonoBehaviour
{
    public void startState() {
        enabled = true;
        Invoke(nameof(wait), Random.Range(0f, 5f));
    }

    private void wait() {
    }

    public void stopState() {
        enabled = false;
    }
}
