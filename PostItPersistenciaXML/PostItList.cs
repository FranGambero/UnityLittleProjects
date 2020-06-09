using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("PostItList")]
[XmlInclude(typeof(PostItData))]
public class PostItList
{
    [XmlArrayItem("PostItData")]
    public List<PostItData> arrayPostItData;
    
    public void storeData() {

        SerializerManager.Instance.SerializeXML(this);
    }

    public PostItData storeOneElement(PostIt postIt) {
        PostItData postItData = new PostItData();

        postItData.texto = postIt.textito.text;

        postItData.xPos = postIt.transform.position.x;
        postItData.yPos = postIt.transform.position.y;
        postItData.ZPos = postIt.transform.position.z;

        postItData.id = postIt.playerID;

        arrayPostItData.Add(postItData);

        return postItData;
    }

    public void deleteElement(PostItData element) {
        arrayPostItData.Remove(element);
    }

}
