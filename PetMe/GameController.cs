using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hand, food, dog, superDogo, miniDog;
    private bool stateHand, stateFood, dogSad;
    public float timer = 7f;

    public GameObject[] vidas;
    public int counter;
    private Vector3 originalScale;

    private AudioSource myAS;
    public AudioClip[] clips;

    public GameObject endPanel, endCube, botones;

    private void Awake() {
        stateHand = hand.activeSelf;
        stateFood = food.activeSelf;
        originalScale = vidas[0].transform.localScale;

        dogSad = false;

        myAS = GetComponent<AudioSource>();

        counter = 3;
    }

    private void Update() {
        timer -= Time.deltaTime;

        if (timer < 0 && counter >= 0) {
            deleteLife(vidas[counter], false);
        }

        if (counter < 0) {
            endCube.SetActive(true);
            endPanel.SetActive(true);
            botones.SetActive(false);

            //Time.timeScale = 0;
        }

        if (counter <= 2 && !dogSad) {
            Debug.Log("Dog is sad");
            dogSad = true;
            dog.GetComponent<DogSounds>().changeClip(true);

            //Cejas tristes
            dog.GetComponent<Movement>().cambiaCejas(2);
        }

        if(counter > 1  && dogSad) {
            Debug.Log("Dog is happy");
            dogSad = false;
            dog.GetComponent<DogSounds>().changeClip(false);
            dog.GetComponent<Movement>().cambiaCejas(Random.Range(0,1));
        }

        if (counter >= 5) {

            superDogo.SetActive(true);
            miniDog.SetActive(false);
        } else {
            //dog.GetComponent<Movement>().cambiaCejas(Random.Range(0,1));
            miniDog.SetActive(true);
            superDogo.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            exitGame();
        }
    }

    public void deleteLife(GameObject life, bool add) {
        Sequence lifeSeq = DOTween.Sequence();

        if (add) {
            Debug.Log("TIEMPOOOOOO");
            myAS.clip = clips[0];
            myAS.Play();
            lifeSeq.Append(life.transform.DOScale(originalScale, 2));
            counter++;
        } else {
            myAS.clip = clips[1];
            myAS.Play();
            lifeSeq.Append(life.transform.DOScale(0.08f, 2));
            counter--;
        }
            timer = 7f;

        lifeSeq.Play();
    }

    public void updateLife() {
        //if (counter >= 5) {

        //    superDogo.SetActive(true);
        //    miniDog.SetActive(false);
        //} else {
        //    miniDog.SetActive(true);
        //    superDogo.SetActive(false);
        //} 
        
        deleteLife(vidas[counter + 1], true);

    }


    public void activateHand() {
        stateHand = !stateHand;
        hand.SetActive(stateHand);
        if (stateHand) {
            Debug.Log("Activo mano");
            //dog.GetComponent<Movement>().rotateSelf();
            //dog.GetComponent<Movement>().reStart();
            dog.GetComponent<Movement>().returnInit();
        }   else {
            Debug.Log("Desactivo mano");
        }

        if (stateFood && stateHand) {
            activateFood();
        }
    }

    public void activateFood() {
        stateFood = !stateFood;
        food.SetActive(stateFood);

        if (stateHand && stateFood) {
            activateHand();
        }

    }

    public void startGame() {
        SceneManager.LoadScene(0);
        //Time.timeScale = 1;
    }

    private void exitGame() {
        Application.Quit();
    }
}
