using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyShelf : MonoBehaviour {
    public float gravityForce;
    public float radius;

    private Transform player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    // Update is called once per frame
    void Update() {
        if (!player) {
            return;
        }

        float playerDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z));
        if (playerDistance > radius) {
            return;
        }

        float currentForce = gravityForce * (1f - (playerDistance / radius));
        Vector3 forceDirection = Vector3.Normalize(new Vector3(transform.position.x - player.position.x, 0f, transform.position.z - player.position.z));

        player.GetComponent<CharacterController>().Move(forceDirection * currentForce);

    }
}
