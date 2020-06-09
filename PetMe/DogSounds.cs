using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSounds : MonoBehaviour
{
    public AudioSource myAS;
    public AudioClip[] clips;

    private void Awake() {
        myAS = GetComponent<AudioSource>();
    }

    public void changeClip(bool sad) {
        if (sad) {
            myAS.clip = clips[1];
        } else {
            myAS.clip = clips[0];
        }

        myAS.Play();
    }

}
