using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueingEnemy : MonoBehaviour {
    public float speed;
    public float detectionRadius;
    
    public Transform waypointList;

    private Transform player;
    private CharacterController charController;
    private List<Transform> waypoints;
    private int currentWaypointIdx = 0;
    private Transform currentWaypoint;
    private bool isPursueing;



    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        waypoints = new List<Transform>();
        charController = GetComponent<CharacterController>();
        isPursueing = false;

        foreach (Transform waypoint in waypointList) {
            waypoints.Add(waypoint);
        }

        if (waypoints.Count > 0) {
            currentWaypoint = waypoints[currentWaypointIdx];
        }
    }


    // Update is called once per frame
    void Update() {
        if (!player) {
            Debug.Log("No player found!");
            return;
        }

        float playerDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z));
        if (playerDistance < detectionRadius) {
            Pursue();
        }

        else {
            Patrol();
        }

    }


    void Patrol() {
        if (!currentWaypoint) {
            if (waypoints.Count > 0) {
                currentWaypoint = waypoints[currentWaypointIdx];
            }
            else {
                return;
            }
        }

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f) {
            currentWaypointIdx = (currentWaypointIdx + 1) % waypoints.Count;
            currentWaypoint = waypoints[currentWaypointIdx];
        }

        float step = speed * Time.deltaTime;
        Vector3 movement = currentWaypoint.position - transform.position;
        movement.y = 0f;
        movement = step * Vector3.Normalize(movement);
        charController.Move(movement);

        //transform.LookAt(new Vector3(currentWaypoint.position.x, transform.position.y, currentWaypoint.position.z));
        if (movement.magnitude != 0f) {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }


    void Pursue() {
        float step = speed * Time.deltaTime;
        Vector3 movement = player.position - transform.position;
        movement.y = 0f;
        movement = step * Vector3.Normalize(movement);
        charController.Move(movement);

        if (movement.magnitude != 0f) {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }



}
