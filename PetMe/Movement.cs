using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Movement : MonoBehaviour {
    public GameObject food, hand;

    public bool onAction, eating, petting;
    Sequence approachSeq, jumpSeq, rotateSeq;
    public Transform petPos;

    public Transform[] positions;

    public bool rotating = false;

    private void Start() {
        this.playFullSequence();
        onAction = eating = petting = false;

    }

    private void Update() {
        if (food.activeSelf && !onAction) {
            onAction = true;
            Debug.Log("comida  " + food.activeSelf + " food" + onAction);
            jumpSeq.Pause();
            approachSeq = approachFood();
            approachSeq.Play();

        }


        if (!food.activeSelf && onAction && !petting) {
            Debug.Log("COMIDA NEGATIVA");
            onAction = false;
            approachSeq.Pause();
            jumpSeq = laditosMood();
            jumpSeq.Play();
        }

        if (!onAction && !rotating && !petting) {
            this.transform.Translate(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * Time.deltaTime * 2.5f);
            transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);
        }

        if (Vector3.Distance(transform.position, food.transform.position) < 3f) {
            approachSeq.Kill();
        }

        if (hand.activeSelf) {
            Debug.Log("Mano activa");
            petting = true;
            jumpSeq.Kill();
        }

        if (!hand.activeSelf) {
            Debug.Log("NO MANO ACTIVA");
            petting = false;
            // Sé que no es recomendable :(
            transform.Find("Luces").gameObject.SetActive(false);
        }

    }

    public Sequence approachFood() {
        rotateSeq.Kill();
        rotating = false;

        Sequence approachSeq = DOTween.Sequence();

        Vector3 eatingPos = new Vector3(food.transform.position.x, food.transform.position.y + .7f, food.transform.position.z);

        approachSeq.Append(transform.DOLookAt(eatingPos, 1f));
        approachSeq.Join(transform.DOMove(eatingPos, 4));

        return approachSeq;
    }

    public void returnInit() {
        Sequence returnSeq = DOTween.Sequence();

        jumpSeq.Kill();

        returnSeq.Append(transform.DOMove(petPos.position, 2));
        returnSeq.Join(transform.DOLocalRotate(new Vector3(0, 90, 0), 1, RotateMode.Fast));

        onAction = true;

        returnSeq.Play();
    }

    public void playFullSequence() {
        //jumpSeq = caballitoMood();
        jumpSeq = laditosMood();

        jumpSeq.Play();
    }

    public void exicetement() {
        Sequence chachiSequence = DOTween.Sequence();

        chachiSequence.Append(transform.DOJump(this.transform.position, 1, 3, 1));

        chachiSequence.Play();
    }

    private Sequence laditosMood() {
        Sequence caballitoMood = DOTween.Sequence();

        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        caballitoMood.Append(transform.DORotate(new Vector3(0, 0, 5), .5f, RotateMode.LocalAxisAdd));

        for (int i = 0; i < 100; i++) {
            caballitoMood.Append(transform.DORotate(new Vector3(0, 0, -10), .5f, RotateMode.LocalAxisAdd));
            caballitoMood.Append(transform.DORotate(new Vector3(0, 0, 10), .5f, RotateMode.LocalAxisAdd));
        }

        return caballitoMood;
    }

    private Sequence caballitoMood() {
        Sequence caballitoMood = DOTween.Sequence();

        caballitoMood.Append(transform.DORotate(new Vector3(5, 0, 0), .5f, RotateMode.LocalAxisAdd));

        for (int i = 0; i < 100; i++) {
            caballitoMood.Append(transform.DORotate(new Vector3(-10, 0, 0), .5f, RotateMode.LocalAxisAdd));
            caballitoMood.Append(transform.DORotate(new Vector3(10, 0, 0), .5f, RotateMode.LocalAxisAdd));

        }

        return caballitoMood;
    }

    public void rotateSelf() {
        rotateSeq = DOTween.Sequence();

        Vector3 saltito = new Vector3(transform.position.x - .7f, 1.25f, transform.position.z);

        //transform.rotation = Quaternion.Euler(0, 0, 0);

        //rotateSeq.Append(transform.DOJump(saltito, .25f, 1, .5f));
        rotateSeq.Append(transform.DORotate(new Vector3(0, Random.Range(-179, 179), 0), 1.5f, RotateMode.Fast).SetRelative()).AppendCallback(() => {
            rotating = false;
            jumpSeq = laditosMood();
        });

        rotateSeq.Play();
    }

    public void reStart() {
        Start();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Wall") && !rotating) {
            Debug.Log("Me han llamao al entrar con " + rotating);
            if (rotateSeq != null) {
                rotateSeq.Kill();
            }
            rotating = true;
            jumpSeq.Kill();
            rotateSelf();
        }

        if (other.CompareTag("Petting")) {
            petting = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Wall")) {
            if (!rotateSeq.IsPlaying()) {
                rotating = true;
                jumpSeq.Kill();
                rotateSelf();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Wall")) {
            //rotating = false;

            //Debug.Log("Me han llamao con " + rotating);
            //jumpSeq = laditosMood();
        }

        if (other.CompareTag("Petting")) {
            petting = false;
        }
    }

    // Sorry
    public void cambiaCejas(int modo) {
        Debug.Log("Cejas llamadas" + modo);
        switch (modo) {
            case 0:
                transform.Find("CejasEnfado").gameObject.SetActive(true);
                transform.Find("CejasTriste").gameObject.SetActive(false);
                transform.Find("CejasNormal").gameObject.SetActive(false);
                break;
            case 1:
                transform.Find("CejasNormal").gameObject.SetActive(true);
                transform.Find("CejasTriste").gameObject.SetActive(false);
                transform.Find("CejasEnfado").gameObject.SetActive(false);
                break;
            case 2:
                transform.Find("CejasTriste").gameObject.SetActive(true);
                transform.Find("CejasNormal").gameObject.SetActive(false);
                transform.Find("CejasEnfado").gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

}
