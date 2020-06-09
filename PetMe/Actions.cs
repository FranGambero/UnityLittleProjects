using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector2 direction;

    public Camera myCamera;
    public GameObject dog, gameController;
    private bool touching = false;
    private float timer;
    public bool soyComedero = false;

    private AudioSource myAS;

    private void Awake() {
        myAS = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        timer = 0;
        touching = false;
    }

    private void Update() {
        mousePosition = Input.mousePosition;

        if (mousePosition != null && !soyComedero) {
            mousePosition = new Vector3(Camera.main.ScreenToViewportPoint(mousePosition).x, Camera.main.ScreenToViewportPoint(mousePosition).y);
            this.transform.position = mousePosition;
        }

        if (touching) {
            // Sé que no es recomendable hacer esto...
            dog.transform.Find("Luces").gameObject.SetActive(true);
            timer += Time.deltaTime;
            Debug.Log(timer);
            if(timer > 3) {
                gameController.GetComponent<GameController>().updateLife();
                timer = 0;
            }
        }  else {
            dog.transform.Find("Luces").gameObject.SetActive(false);
            timer = 0;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Dog") && soyComedero) {
            myAS.Play();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Dog") && soyComedero) {
            Debug.Log("Estoy comiendo");
            timer += Time.deltaTime;
            if (timer > 3) {
                gameController.GetComponent<GameController>().updateLife();
                timer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Dog") && soyComedero) {
            timer = 0;
            myAS.Pause();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Dog") && !touching) {
            touching = true;
        }
    }


    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Dog")) {
            touching = false;
        }
    }
}