using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writer : MonoBehaviour
{
    public GameObject[] messages;
    public GameObject group;
    private void Start() {
        loadMessages();
    }

    private void loadMessages() {
        for (int i = 0; i < messages.Length; i++) {
            messages[i].transform.parent = group.transform;
        }
    }

    public void createMessage() {

    }
}
