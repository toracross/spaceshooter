using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private GameObject[] powerups;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    // Update is called once per frame
    void Update() {
        
    }

    //Enemy Spawner
    IEnumerator EnemySpawnRoutine() {
        while (true) {
            int randomRange = Random.Range(-7, 7);

            Instantiate(enemyShipPrefab, new Vector3(randomRange, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine() {
        while (true) {
            int randomPowerup = Random.Range(0, 2);
            int randomRange = Random.Range(-7, 7);

            Instantiate(powerups[randomPowerup], new Vector3(randomRange, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }
}
