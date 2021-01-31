using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueingEnemy : MonoBehaviour {

    public Animator animator;

    public float speed;
    public float detectionRadius;
    public float dread;
    public float roamingTimeMin;
    public float roamingTimeMax;
    public float idleTimeMin;
    public float idleTimeMax;

    private Transform player;
    private CharacterController charController;
    private bool isPursueing;
    private bool isRoaming;
    private float timeLeft;
    private Vector3 roamingDir;

    private bool playerCollisionThisFrame;
    private bool playerCollisionLastFrame;

    private Vector3 gravityVector = new Vector3(0f, -9.8f, 0f);


    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isPursueing = false;

        isRoaming = false;
        roamingDir = new Vector3();
        startIdling();

        playerCollisionThisFrame = false;
        playerCollisionLastFrame = false;

        animator.SetBool("isWalking", false);
    }


    // Update is called once per frame
    void Update() {
        if (playerCollisionThisFrame) {
            playerCollisionLastFrame = true;
            playerCollisionThisFrame = false;
        }
        else {
            playerCollisionLastFrame = false;
        }

        float playerDistance = Mathf.Infinity;
        if (player) {
            playerDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z));
        }
        else {

        }
        
        if (playerDistance < detectionRadius) {
            if (!isPursueing) {
                startPursueing();
            }
            else {
                Pursue();
            }
            
        }

        else {
            if (isPursueing) {
                startIdling();
            }
            else {
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

        }

        if (!charController.isGrounded) {
            charController.Move(gravityVector * Time.deltaTime);
        }
    }


    void startIdling() {
        isPursueing = false;
        isRoaming = false;
        timeLeft = Random.Range(idleTimeMin, idleTimeMax);

        animator.SetBool("isWalking", false);
    }


    void startRoaming() {
        isRoaming = true;
        timeLeft = Random.Range(roamingTimeMin, roamingTimeMax);
        roamingDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        roamingDir = Vector3.Normalize(roamingDir);

        animator.SetBool("isWalking", true);
        Roam();
    }


    void Roam() {
        float step = speed * Time.deltaTime;
        Vector3 movement = step * roamingDir;
        movement.y = 0f;
        charController.Move(movement);
        

        if (movement.magnitude != 0f) {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }


    void startPursueing() {
        isPursueing = true;
        isRoaming = false;

        animator.SetBool("isWalking", true);
    }


    void Pursue() {
        Vector3 movement = player.position - transform.position;
        movement.y = 0f;
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;

        if (movement.magnitude != 0f) {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        charController.Move(movement);



    }


    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Player")) {
            playerCollisionThisFrame = true;
            if (!playerCollisionLastFrame) {
                DreadMeter dreadMeter = hit.gameObject.GetComponent<DreadMeter>();
                dreadMeter.ModifyValue(dread);
            }
        }
    }

}
