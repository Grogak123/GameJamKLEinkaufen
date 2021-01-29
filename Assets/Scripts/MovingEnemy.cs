using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {
    public float speed;
    public Transform waypointList;

    private CharacterController charController;
    private List<Transform> waypoints;
    private int nextWaypointIdx = 0;
    private Transform nextWaypoint;

    // Start is called before the first frame update
    void Start() {
        waypoints = new List<Transform>();
        charController = GetComponent<CharacterController>();

        foreach (Transform waypoint in waypointList) {
            waypoints.Add(waypoint);
        }

        if (waypoints.Count > 0) {
            nextWaypoint = waypoints[nextWaypointIdx];
        }
    }

    // Update is called once per frame
    void Update() {
        if (!nextWaypoint) {
            if (waypoints.Count > 0) {
                nextWaypoint = waypoints[nextWaypointIdx];
            }
            else {
                return;
            }
        }

        if (Vector3.Distance(transform.position, nextWaypoint.position) < 0.1f) {
            nextWaypointIdx = (nextWaypointIdx + 1) % waypoints.Count;
            nextWaypoint = waypoints[nextWaypointIdx];
        }

        Debug.Log(Vector3.Distance(transform.position, nextWaypoint.position));

        float step = speed * Time.deltaTime;
        Vector3 movement = step * Vector3.Normalize(nextWaypoint.position - transform.position);
        charController.Move(movement);

        transform.LookAt(new Vector3(nextWaypoint.position.x, transform.position.y, nextWaypoint.position.z));
    }
}
