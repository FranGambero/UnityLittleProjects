using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlType("PostItData")]
public class PostItData
{
    [XmlElement("Texto")]
    public string texto;

    [XmlElement("XPos")]
    public float xPos;

    [XmlElement("YPos")]
    public float yPos;

    [XmlElement("ZPos")]
    public float ZPos;

    [XmlElement("Id")]
    public string id;

}
