using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPasserby : MonoBehaviour {

    public float chance;
    public GameObject passerby;

    // Start is called before the first frame update
    void Start() {
        if (Random.value <= chance) {
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), 0f, transform.position.z + Random.Range(-2f, 2f));
            Instantiate(passerby, spawnPosition, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
