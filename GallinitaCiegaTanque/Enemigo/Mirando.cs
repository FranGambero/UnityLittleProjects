using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Mirando : MonoBehaviour
{
    void Update() {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180f, 0), Time.deltaTime * 5);
    }

    public void startState() {
        enabled = true;
    }

    public void stopState() {
        enabled = false;
    }

}
