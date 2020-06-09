using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public Transform leftWingPosition;
    public Transform rightWingPosition;
    public Transform enginePosition;
    public Transform dorsalPosition;
    public Transform leftGunPosition, rightGunPosition;

    public GameObject starship;
    public GameObject[] engineList, rightWingsList, leftWingsList, dorsalList, gunsList;
    public Material[] materialList;
    public Material selectedMaterial;

    private void Start() {
        generateStarship();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            generateStarship();
        }
    }

    private void generateStarship() {
        // Generamos cosica
        generateMaterial();
        generateEngine();
        generateWings();
        generateDorsalWings();
        //generateGuns();
    }

    private void generateEngine() {
        checkObject(enginePosition);
        GameObject selectedEngine = engineList[Random.Range(0, engineList.Length)];
        Instantiate(selectedEngine, enginePosition);
    }

    private void generateWings() {
        checkObject(leftWingPosition);
        checkObject(rightWingPosition);

        int index = Random.Range(0, rightWingsList.Length);

        GameObject selectedLeftWing = leftWingsList[index];
        GameObject selectedRightWing = rightWingsList[index];

        selectedLeftWing.GetComponent<Renderer>().material = selectedMaterial;
        selectedRightWing.GetComponent<Renderer>().material = selectedMaterial;

        //Instantiate(selectedLeftWing, leftWingPosition);
        //Instantiate(selectedRightWing, rightWingPosition);

        generateGuns(Instantiate(selectedLeftWing, leftWingPosition), Instantiate(selectedRightWing, rightWingPosition));
    }

    private void generateDorsalWings() {
        checkObject(dorsalPosition);

        int summonDorsal = Random.Range(0, 2);
        if(summonDorsal >= 1) {

            GameObject selectedDorsal = dorsalList[Random.Range(0, dorsalList.Length)];

            selectedDorsal.GetComponent<Renderer>().material = selectedMaterial;

            Instantiate(selectedDorsal, dorsalPosition);
        }
    }

    private void generateGuns(GameObject leftWing, GameObject rightWing) {
        checkObject(leftWing.transform.GetChild(0));
        checkObject(rightWing.transform.GetChild(0));

        int index = Random.Range(0, gunsList.Length);

        GameObject selectedLeftGun = gunsList[index];
        GameObject selectedRightGun = gunsList[index];

        Instantiate(selectedLeftGun, leftWing.transform.GetChild(0));
        Instantiate(selectedRightGun, rightWing.transform.GetChild(0));
    }

    private void generateMaterial() {
        selectedMaterial = materialList[Random.Range(0, materialList.Length)];
        starship.GetComponent<Renderer>().material = selectedMaterial;
    }

    private void checkObject(Transform checkTarget) {
        if(checkTarget.childCount > 0) {
            Destroy(checkTarget.GetChild(0).gameObject);
        }

    }
}
