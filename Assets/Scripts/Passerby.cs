using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passerby : MonoBehaviour {

    public float speed;
    public float roamingTimeMin;
    public float roamingTimeMax;
    public float idleTimeMin;
    public float idleTimeMax;

    private CharacterController charController;
    private bool isRoaming;
    private float timeLeft;
    private Vector3 roamingDir;

    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController>();
        isRoaming = false;
        roamingDir = new Vector3();
        startIdling();
    }

    // Update is called once per frame
    void Update() {
        timeLeft -= Time.deltaTime;

        if (isRoaming) {
            if (timeLeft < 0f) {
                startIdling();
            }
            else {
                Roam();
            }
        }

        else {
            if (timeLeft < 0f) {
                startRoaming();
            } 
        }
    }


    void startIdling() {
        isRoaming = false;
        timeLeft = Random.Range(idleTimeMin, idleTimeMax);
    }


    void startRoaming() {
        isRoaming = true;
        timeLeft = Random.Range(roamingTimeMin, roamingTimeMax);
        roamingDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        roamingDir = Vector3.Normalize(roamingDir);

        Roam();
    }


    void Roam() {
        float step = speed * Time.deltaTime;
        Vector3 movement = step * roamingDir;
        charController.Move(movement);

        if (movement.magnitude != 0f) {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }

}
