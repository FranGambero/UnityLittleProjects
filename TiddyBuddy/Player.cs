using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool hasObject = false;
    public GameObject pickedObject;

    public Collider[] collidersDetected;
    public float catchingDistance;
    public LayerMask playerLayer;

    public Text pointsText;

    private float points = 0;

    private void Update() {
        checkHear();
    }
    public void setPickBool(bool coso){
        hasObject = coso;
    }

    private Transform checkHear() {
        collidersDetected = Physics.OverlapSphere(transform.position, catchingDistance, playerLayer);
        if (collidersDetected.Length > 0 && collidersDetected[0].GetComponent<Player>().hasObject == false) {
            Debug.Log("ENCONTRE ARGO");
            return collidersDetected[0].transform;
        } else {
            return null;
        }
    }

    public void updatePoints() {
        points++;
        Debug.Log("Soy :" + this.gameObject.name + " mis puntos: " + this.points);
        this.pointsText.text = this.points.ToString();
    }
}
