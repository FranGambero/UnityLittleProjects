using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSingle : MonoBehaviour
{
     public Transform picker;
     private Transform item;
     private GameObject player;

    public Collider[] collidersDetected;
    //public float catchingDistance;
    //private LayerMask playerLayer;

    public GameObject targetPair;
    public GameObject particlePrefab;


    //  public Player playerScript;

    private void Awake()
     {
         item = this.transform;
         player = GameObject.FindGameObjectWithTag("Player");

        //this.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    public void Update(){
        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightShift)) && Distance() <= 5f && player.GetComponent<Player>().hasObject == false) {
            this.pickObject();
        }else if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.RightShift))) {
            this.dropObject();
        }

    }

    public void pickObject(){
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = picker.position;
        this.transform.parent = GameObject.Find("Hand").transform;

        player.GetComponent<Player>().hasObject = true;

        GetComponent<Rigidbody>().isKinematic = true;

        player.GetComponent<Player>().pickedObject = this.gameObject;


        // Movemos a mano
        // ponemos kinematic
        // desactivo mesh colladier



        // playerScript.setPickBool(true);
    }

    public void dropObject(){
        // Debug.Log("Llamada al up");
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<MeshCollider>().enabled = true;

        GetComponent<Rigidbody>().isKinematic = false;

        player.GetComponent<Player>().hasObject = false;

        player.GetComponent<Player>().pickedObject = null;


        // playerScript.setPickBool(false);

    }

    private float Distance()
     {
         return Vector3.Distance(item.position, player.transform.position);
     }

    //private Transform checkHear() {
    //    collidersDetected = Physics.OverlapSphere(transform.position, catchingDistance, playerLayer);
    //    if (collidersDetected.Length > 0 && collidersDetected[0].GetComponent<Player>().hasObject == false) {
    //        return collidersDetected[0].transform;
    //    } else {
    //        return null;
    //    }
    //}

    private void OnTriggerEnter(Collider other) {
      

        if(other == this.targetPair.GetComponentInChildren<BoxCollider>()) {
            Debug.Log("MI PAREJA trigger children");
            this.startParticleSystem();
            Destroy(other);
            Destroy(this.gameObject);
        }
    }

    private void startParticleSystem() {
        //this.gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = true;            
        //this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        Instantiate(this.particlePrefab, this.transform.position, this.transform.rotation);
    }

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.gameObject == this.targetPair) {
    //        Debug.Log("MI PAREJA");
    //        Destroy(collision.gameObject);
    //    }

    //}
}
