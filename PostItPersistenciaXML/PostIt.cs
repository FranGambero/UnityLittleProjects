using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class PostIt : MonoBehaviour {
    public string playerID;
    public bool beingDragged;
    public LayerMask layerMask;

    public TextMeshProUGUI textito;

    public PostItData postItData;
    public GameObject subMenu;
    private bool showDelete = false;

    public void loadPostIt(PostItData data) {
        // Para asignar los datos al postIt una vez ha sido instanciado desde XML
        playerID = data.id;
        transform.position = new Vector3(data.xPos, data.yPos, data.ZPos);
        textito.text = data.texto;
        postItData = data;
        beingDragged = false;
    }

    private void Update() {

        if (beingDragged) {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, layerMask)) {
                transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - .25f);
            }

        }

    }          

    public void showDeleteButton() {
        if (playerID == GameManager.Instance.player.ID) {
            showDelete = !showDelete;
            subMenu.SetActive(showDelete);
        }
    }

    public void borrarPostIt() {
        PostItGenerator.Instance.xmlPostItList.deleteElement(postItData);
        Destroy(this.gameObject);
    }
}
