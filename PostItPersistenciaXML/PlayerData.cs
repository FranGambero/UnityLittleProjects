using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData {
    public string ID;
    public string filesPath;
    public string userName;
    public float points;

    public PlayerData(string iD) {
        ID = iD;
        points = 0;
    }
    public PlayerData() {
        ID = Guid.NewGuid().ToString();
        this.filesPath = null;
        points = 0;
    }

}
