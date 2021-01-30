using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float speed;

    CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0f,0f,0f);

        if (Input.GetKey("w")) {
            movement.z += 1f;
        }
        if (Input.GetKey("s")) {
            movement.z += -1f;
        }

        if (Input.GetKey("d")) {
            movement.x += 1f;
        }
        if (Input.GetKey("a")) {
            movement.x += -1f;
        }

        movement = speed * Vector3.Normalize(movement);
        movement.y = -9.8f;
        charController.Move(movement);
    }
}
