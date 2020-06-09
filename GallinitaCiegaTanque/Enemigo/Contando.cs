using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contando : MonoBehaviour
{
    public Vector3 targetPos;

    private void Update() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0f, 0), Time.deltaTime * 5);
    }

    public void startState() {
        enabled = true;
    }

    public void stopState() {
        enabled = false;
    }

}
