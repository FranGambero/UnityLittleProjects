using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public Transform picker1, picker2;
    private Transform item;
    private GameObject player, player2;
    private GameObject activePlayer, lastTouchedPlayer;

    public Collider[] collidersDetected;

    public GameObject targetPair;
    public GameObject particlePrefab;
    private bool isDestroyed = false;

    public Material pink, blue;

    Rigidbody myRb;


    private void Awake() {
        item = this.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2Tag");

        myRb = GetComponent<Rigidbody>();

    }

    public void Update() {

        if (checkDistance() != null) {
            if (Input.GetKey(KeyCode.Space) && player.GetComponent<Player>().hasObject == false && checkDistance() == "Hand") {
                this.pickObject(checkDistance());
            } else if (Input.GetMouseButton(0) && player2.GetComponent<Player>().hasObject == false && checkDistance() == "Hand2") {
                this.pickObject(checkDistance());
            }
        }
        // jfhufuwyuyweu
        else if (Input.GetKeyUp(KeyCode.Space)) {
            this.dropObject(player);
        } else if (Input.GetMouseButtonUp(0)) {
            this.dropObject(player2);
        }


        if (myRb) {

            if (myRb.velocity.magnitude > 5f) {
                myRb.velocity = Vector3.ClampMagnitude(myRb.velocity, 5f);
            }
        }

    }

    public void pickObject(string handTarget) {
        Debug.Log("Me ha llegado: " + handTarget);

        this.activePlayer = (handTarget == "Hand") ? player : player2;

        GetComponent<MeshCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = (handTarget == "Hand") ? picker1.position : picker2.position;
        this.transform.parent = GameObject.Find(handTarget).transform;

        this.activePlayer.GetComponent<Player>().hasObject = true;
        this.activePlayer.GetComponent<Player>().pickedObject = this.gameObject;

        this.gameObject.GetComponent<Renderer>().material = (activePlayer == player) ? pink : blue;

        GetComponent<Rigidbody>().isKinematic = true;


    }

    public void dropObject(GameObject dropperPlayer) {

        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<MeshCollider>().enabled = true;

        GetComponent<Rigidbody>().isKinematic = false;

        dropperPlayer.GetComponent<Player>().hasObject = false;
        dropperPlayer.GetComponent<Player>().pickedObject = null;
        this.targetPair.GetComponent<Pickup>().lastTouchedPlayer = null;
        this.lastTouchedPlayer = this.activePlayer;

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            this.lastTouchedPlayer = player;
        } else if (collision.gameObject.tag == "Player2Tag") {
            this.lastTouchedPlayer = player2;
        }
    }

    private float Distance() {

        return Vector3.Distance(item.position, player.transform.position);
    }

    private string checkDistance() {
        float distancePlayer1 = Vector3.Distance(item.position, player.transform.position);
        float distancePlayer2 = Vector3.Distance(item.position, player2.transform.position);

        // Debug.Log("Las distancias " + distancePlayer1 + " y " + distancePlayer2);
        float lowestDistance = Mathf.Min(distancePlayer1, distancePlayer2);


        if (lowestDistance == distancePlayer1 && lowestDistance <= 5 && Input.GetKey(KeyCode.Space)) {
            return "Hand";
        } else if (lowestDistance == distancePlayer2 && lowestDistance <= 5 && Input.GetMouseButton(0)) {
            return "Hand2";
        }
        //else if (distancePlayer1 == distancePlayer2) { 
        //    return "Hand";
        //} 
        else {
            // Debug.Log("Que devuelvoL " + lowestDistance);
            return null;
        }

    }

    private void OnTriggerEnter(Collider other) {


        if (other == this.targetPair.GetComponentInChildren<BoxCollider>() && !isDestroyed && other.gameObject != null) {
            Debug.Log("MI PAREJA trigger children con papi: " + this.activePlayer);
            this.startParticleSystem();
            //this.targetPair.GetComponent<Pickup>().isDestroyed = true;
            //Destroy(other);
            if ((this.activePlayer || this.lastTouchedPlayer) && !isDestroyed) {
                this.isDestroyed = true;
                // StartCoroutine("checkWin");
                // this.checkWin();
            }
            Destroy(this.gameObject);
        }

    }


    //private void OnTriggerExit(Collider other) {
    //    if (other.gameObject.tag == "Outdoors") {
    //        Debug.Log("Que me salgo!!!");
    //        this.transform.Translate(new Vector3(-3, 22.83f, 15.6f));
    //    }
    //}


    IEnumerator checkWin() {
        if (this.activePlayer == null) {
            this.lastTouchedPlayer.GetComponent<Player>().updatePoints();
        } else {
            this.activePlayer.GetComponent<Player>().updatePoints();
        }
        yield return null;
    }

    private void checkParentWin() {
        this.activePlayer.GetComponent<Player>().updatePoints();
    }

    private void startParticleSystem() {
        //this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        Instantiate(this.particlePrefab, this.transform.position, this.transform.rotation);
    }

    private void OnDestroy() {
        // checkWin();
        if (this.gameObject.GetComponent<Renderer>().sharedMaterial == pink) {
            Debug.Log("ME ASESINO PLAYER 1");
            this.player.GetComponent<Player>().updatePoints();
        } else if (this.gameObject.GetComponent<Renderer>().sharedMaterial == blue) {
            Debug.Log("ME ASESINO PLAYER 2");
            this.player2.GetComponent<Player>().updatePoints();
        }
        Debug.Log("Me han destrozado, soy " + gameObject);
    }
}
