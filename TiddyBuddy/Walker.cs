using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{ 

    public string moveAxis, rotateAxis;
    public float moveSpeed, rotateSpeed;
    Rigidbody myRb;
    public bool secondPlayer;

    public Animator myAnimator;

    private void Awake() {
        myRb = GetComponent<Rigidbody>();
        myAnimator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //To rotate the player, the keys are specified for each controller in the unity project settings
        float h = Input.GetAxisRaw(rotateAxis);
        transform.Rotate(Vector3.up * rotateSpeed * h * Time.deltaTime);

        //if (myRb.velocity.magnitude > 2) {
        //    myRb.velocity = Vector3.ClampMagnitude(myRb.velocity, 2);
        //}

    }

    private void FixedUpdate() {
        //To move the tank to the front or to the back, getting the axis from project settings
        float v = Input.GetAxis(moveAxis);
        if (v > 0) {
            Vector3 desp = transform.forward * moveSpeed * v * Time.deltaTime;
            myRb.MovePosition(myRb.position + desp);
            myAnimator.SetBool("IsWalking", true);
        } else {
            myAnimator.SetBool("IsWalking", false);
        }
    }

    // public KeyCode upKey, downKey, rightKey, leftKey;
    // public float moveSpeed, rotateSpeed;

    // void Update()
    // {
    //     if (Input.GetKey(upKey)) {
    //         transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    //     }
    //     if (Input.GetKey(downKey)) {
    //         transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    //     }
    //     if (Input.GetKey(rightKey)) {
    //         transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    //     }
    //     if (Input.GetKey(leftKey)) {
    //         transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
    //     }
    // }
}
