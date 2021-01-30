using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {
    public float speed;
    public Transform waypointList;

    private CharacterController charController;
    private List<Transform> waypoints;
    private int currentWaypointIdx = 0;
    private Transform currentWaypoint;

    // Start is called before the first frame update
    void Start() {
        waypoints = new List<Transform>();
        charController = GetComponent<CharacterController>();

        foreach (Transform waypoint in waypointList) {
            waypoints.Add(waypoint);
        }

        if (waypoints.Count > 0) {
            currentWaypoint = waypoints[currentWaypointIdx];
        }
    }

    // Update is called once per frame
    void Update() {
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
        Vector3 movement = step * Vector3.Normalize(currentWaypoint.position - transform.position);
        charController.Move(movement);

        transform.LookAt(new Vector3(currentWaypoint.position.x, transform.position.y, currentWaypoint.position.z));
    }
}
