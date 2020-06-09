using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour {
    public int numRows, numCols;
    //public int rowPos, colPos;
    public GameObject[,] matrix;
    public List<List<GameObject>> listMatrix;
    public GameObject casillaPrefab;
    public float offset = 0.3f;
    public Transform ogPos;

    public GameObject agua, tierra;

    private void Awake() {
        numRows = 10;
        numCols = 10;

        //rowPos = colPos = 0;

        ogPos = agua.transform;
    }

    void Start() {
        createMatrix();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            reChangeMatrix();
        }

    }

    private void createMatrix() {
        matrix = new GameObject[numRows, numCols];
        listMatrix = new List<List<GameObject>>();
        fillMatrix();

    }

    private void fillMatrix() {
        for (int i = 0; i <= numCols - 1; i++) {
            listMatrix.Add(new List<GameObject>());
            for (int j = 0; j <= numRows - 1; j++) {
                Vector3 newPos = new Vector3(i * offset, j * offset, 0);
                listMatrix[i].Add(Instantiate(casillaPrefab, newPos, Quaternion.identity));
                casillaPrefab.GetComponent<Casilla>().generateCell(newPos, Random.Range(0, 2) == 0, i, j);
            }
        }
    }

    private void reChangeMatrix() {
        Debug.Log("Cambiamos la matrizzz");
        for (int i = 0; i <= numCols - 1; i++) {
            for (int j = 0; j <= numRows - 1; j++) {
                changeCell(listMatrix[i][j], i, j);
            }
        }
    }

    private void changeCell(GameObject celda, int currentCol, int currentRow) {

        int totalWater = 0;
        int totalGround = 0;

        Debug.Log("MODIFICANDO Nodo: " + currentCol + currentRow);

        for (int i = currentCol - 1; i <= currentCol + 1; i++) {
            for (int j = currentRow - 1; j <= currentRow + 1; j++) {
                Debug.Log("Vas a entrar a " + i + j + ".");
                if (i >= 0 && i != numCols && j >= 0 && j != numRows && (i != currentCol || j != currentRow)) {
                    Debug.Log("|-- Checkeando: " + i + " " + j + " - soy " + listMatrix[i][j].GetComponent<Casilla>().isWater);
                    if (listMatrix[i][j].GetComponent<Casilla>().isWater) {
                        totalWater++;
                    } else {
                        totalGround++;
                    }
                }
            }
        }

        

        if (totalWater > totalGround) {
            celda.GetComponent<Casilla>().isWater = true;
        } else {
            celda.GetComponent<Casilla>().isWater = false;
        }


        celda.GetComponent<Casilla>().checkType();
    }
}
