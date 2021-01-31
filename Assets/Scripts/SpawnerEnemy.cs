using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour {

    public float overallChance;
    public float enemyChance;
    public GameObject passerby;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start() {
        if (Random.value <= overallChance) {
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-2f, 2f), 0f, transform.position.z + Random.Range(-2f, 2f));
            if (Random.value <= enemyChance) {
                Instantiate(enemy, spawnPosition, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
            }
            else {
                Instantiate(passerby, spawnPosition, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
            }

        }
    }

    // Update is called once per frame
    void Update() {

    }
}
