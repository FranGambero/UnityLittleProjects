﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starship : MonoBehaviour
{
    public float force = 15f;

    private void Update() {
        transform.Rotate(new Vector3(0, 180, 0), Time.deltaTime * force);
    }
}
