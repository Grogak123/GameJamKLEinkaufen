using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {
    public float speed;
    public float dread;
    public Transform waypointList;

    private CharacterController charController;
    private List<Transform> waypoints;
    private int currentWaypointIdx = 0;
    private Transform currentWaypoint;
    private bool walkingBackwards;

    private bool playerCollisionThisFrame;
    private bool playerCollisionLastFrame;

    // Start is called before the first frame update
    void Start() {
        waypoints = new List<Transform>();
        charController = GetComponent<CharacterController>();

        foreach (Transform waypoint in waypointList) {
            waypoints.Add(waypoint);
        }

        if (waypoints.Count > 1) {
            currentWaypoint = waypoints[currentWaypointIdx];
        }

        walkingBackwards = false;
        playerCollisionThisFrame = false;
        playerCollisionLastFrame = false;
        ;
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

        if (!currentWaypoint) {
            if (waypoints.Count > 1) {
                currentWaypoint = waypoints[currentWaypointIdx];
            }
            else {
                return;
            }
        }

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f) {
            if (walkingBackwards) {
                if (currentWaypointIdx == 0) {
                    walkingBackwards = false;
                    currentWaypointIdx += 1;
                }
                else {
                    currentWaypointIdx -= 1;
                }
            }
            else {
                if (currentWaypointIdx == waypoints.Count - 1) {
                    walkingBackwards = true;
                    currentWaypointIdx -= 1;
                }
                else {
                    currentWaypointIdx += 1;
                }
            }

            currentWaypoint = waypoints[currentWaypointIdx];
        }

        float step = speed * Time.deltaTime;
        Vector3 movement = step * Vector3.Normalize(currentWaypoint.position - transform.position);
        charController.Move(movement);

        transform.LookAt(new Vector3(currentWaypoint.position.x, transform.position.y, currentWaypoint.position.z));
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
