using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class PostItGenerator : Singleton<PostItGenerator> {
    public GameObject prefabPostIt;
    private GameObject tmpPostIt;

    public GameObject hierarchyPostItlist;
    public PostItList xmlPostItList;

    private void Awake() {
        xmlPostItList = new PostItList();
        //xmlPostItList.listtoSave = new List<PostIt>();
        xmlPostItList.arrayPostItData = new List<PostItData>();
    }

    private void Start() {
        try {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!Directory.Exists(path + "/POSTITDATA")) {
                Directory.CreateDirectory(path + "/POSTITDATA");
            } else if(Directory.Exists(path + "/POSTITDATA")){
                xmlPostItList = SerializerManager.Instance.DeserializeXML(path + "/POSTITDATA/PostItListXML.xml");
            }

            loadElements();
        } catch (System.Exception e) {
            Debug.LogError(e.Message);
            throw;
        }
    }

    public void startDrag() {
        tmpPostIt = Instantiate(prefabPostIt, transform.position, transform.rotation, hierarchyPostItlist.transform);
        tmpPostIt.GetComponent<PostIt>().beingDragged = true;

        tmpPostIt.GetComponent<PostIt>().textito.text = transform.GetComponentInChildren<TMP_InputField>().text;
        tmpPostIt.GetComponent<PostIt>().playerID = GameManager.Instance.player.ID;


        transform.GetComponentInChildren<TMP_InputField>().text = "";

    }

    public void endDrag() {
        if (tmpPostIt) {
            tmpPostIt.GetComponent<PostIt>().beingDragged = false;

            tmpPostIt.GetComponent<PostIt>().postItData = xmlPostItList.storeOneElement(tmpPostIt.GetComponent<PostIt>());

            tmpPostIt = null;
        }
    }

    private void loadElements() {
        for (int i = 0; i < xmlPostItList.arrayPostItData.Count; i++) {
            // El objeto lo crea, pero los datos se le cargan en la operacion loadPostIt
            GameObject existingPostIt = Instantiate(prefabPostIt, new Vector3(
                xmlPostItList.arrayPostItData[i].xPos,
                xmlPostItList.arrayPostItData[i].yPos,
                xmlPostItList.arrayPostItData[i].ZPos), transform.rotation, hierarchyPostItlist.transform);

            existingPostIt.GetComponent<PostIt>().loadPostIt(xmlPostItList.arrayPostItData[i]);
        }
    }

    [ContextMenu("Save your postIts")]
    public void seriliazeXML() {
        xmlPostItList.storeData();
    }
}
