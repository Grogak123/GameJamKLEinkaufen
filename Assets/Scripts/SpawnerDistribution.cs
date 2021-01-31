using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDistribution : MonoBehaviour {

    public int xSize;
    public int zSize;
    public int cellSize;

    public GameObject passerbySpawner;
    public GameObject enemySpawner;
    // Start is called before the first frame update
    void Start() {
        for (int x = 0; x < xSize - 3; x++) {
            for (int z = 0; z < zSize - 3; z++) {
                float xPos = ((x - xSize / 2) * cellSize) + (cellSize / 2);
                float zPos = ((z - zSize / 2) * cellSize) + (cellSize / 2);
                Vector3 pos = new Vector3(xPos, 0f, zPos);
                Instantiate(enemySpawner, pos, Quaternion.identity);
            }
        }

        for (int x = xSize - 3; x < xSize; x++) {
            for (int z = zSize - 3; z < zSize - 3; z++) {
                float xPos = ((x - xSize / 2) * cellSize) + (cellSize / 2);
                float zPos = ((z - zSize / 2) * cellSize) + (cellSize / 2);
                Vector3 pos = new Vector3(xPos, 0f, zPos);
                Instantiate(passerbySpawner, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
