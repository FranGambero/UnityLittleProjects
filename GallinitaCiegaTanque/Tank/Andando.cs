using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Andando : MonoBehaviour {
    public List<Transform> patrolPointsList;
    private Vector3 initPos;
    //private int /*currentPatrolIndex*/;
    public Vector3 targetPos;
    private Quaternion targetRot;

    //private NavMeshAgent navMeshAgent;
    private Animator anim;
    public float timer, dangerTime = .6f;
    private bool enemigo = false;

    private AudioSource myAS;

    private void Awake() {
        initPos = transform.position;
        myAS = GetComponent<AudioSource>();
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;

    }

    void Update() {
        movePalante();                  
        if(EnemigoFSM.Instance.estado == Estado.Mirando) {
            timer += Time.deltaTime;
            if(timer >= dangerTime) {
                timer = 0;
                goInit();
            }
        }
    }

    public void startState() {
        enabled = true;
        timer = 0;
    }

    public void stopState() {
        Invoke(nameof(makeStop), Random.Range(0, 3));
    }

    private void makeStop() {

        enabled = false;
        //TankFSM.Instance.estadoEsperando.startState();
        //TankFSM.Instance.setState(Estado.Esperando);
    }

    private void movePalante() {

       transform.Translate(Vector3.forward * Time.deltaTime);
 
    }

    private void goInit() {
        myAS.Play();
        transform.position = initPos;
    }

}
