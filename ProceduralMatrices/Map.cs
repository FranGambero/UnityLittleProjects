using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public GameObject emptyCell, factoryCell, houseCell, roadCell, schoolCell;
    public List<GameObject> prefabList;
    public Transform startRoadT, endRoadT;
    public int numCols, numRows;

    public List<List<GameObject>> mapMatrix;

    private void Awake() {
        numCols = 10;
        numRows = 10;
    }

    private void Start() {
        mapMatrix = new List<List<GameObject>>();
        createMap();
        randomRoad();
    }

    private void createMap() {
        for (int i = 0; i <= numCols - 1; i++) {
            mapMatrix.Add(new List<GameObject>());
            for (int j = 0; j <= numRows - 1; j++) {
                Vector3 newPos = new Vector3(i, 0, j);
                //GameObject nextPrefab = 
                mapMatrix[i].Add(Instantiate(emptyCell, newPos, Quaternion.identity));
                //casillaPrefab.GetComponent<Casilla>().generateCell(newPos, Random.Range(0, 2) == 0, i, j);
            }
        }
    }

    private void randomRoad() {
        int randomRow = Random.Range(0, numRows);
        int randomCol = Random.Range(0, numCols);

        Destroy(mapMatrix[randomCol][0]);
        mapMatrix[randomCol][0] = Instantiate(roadCell, mapMatrix[randomCol][0].transform.position, Quaternion.identity);
        Destroy(mapMatrix[0][randomRow]);
        mapMatrix[0][randomRow] = Instantiate(roadCell, mapMatrix[0][randomRow].transform.position, Quaternion.identity);

        buildRoad();
    }

    private void buildRoad() {
        // Vamos a buscar el coso

    }
}
