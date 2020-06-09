using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Casilla : MonoBehaviour {

    public Vector3 cellTransform;
    public int col, row;
    public bool isWater, beenChecked;
    public Sprite waterSprite, groundSprite;
    private SpriteRenderer mySpriteRenderer;

    //Coso Fran
    public void generateCell(Vector3 newPosition, bool waterType, int newCol, int newRow) {
        cellTransform = newPosition;
        isWater = waterType;

        col = newCol;
        row = newRow;

        checkType();
    }

    public void checkType() {
        Debug.Log("Mi tipo es " + isWater);
        if (isWater) {
            this.GetComponent<SpriteRenderer>().sprite = waterSprite;
        } else {
            this.GetComponent<SpriteRenderer>().sprite = groundSprite;
        }
    }

    /// En Fran

    ///// Coso David
    //private void Awake() {
    //    mySpriteRenderer = GetComponent<SpriteRenderer>();
    //}

    //public void initializeCell(float waterProbablity) {
    //               setCellType()
    //}

    //public void setCellType(bool water) {
    //    isWater = water;
    //    mySpriteRenderer.sprite = isWater ? waterSprite : groundSprite;
    //}
}
