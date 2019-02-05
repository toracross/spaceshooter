using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private GameObject[] powerups;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start() {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void StartSpawnRoutines() {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //Enemy Spawner
    IEnumerator EnemySpawnRoutine() {
        while (!_gameManager.gameOver) {
            int randomRange = Random.Range(-7, 7);

            Instantiate(enemyShipPrefab, new Vector3(randomRange, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine() {
        while (!_gameManager.gameOver) {
            int randomPowerup = Random.Range(0, 2);
            int randomRange = Random.Range(-7, 7);

            Instantiate(powerups[randomPowerup], new Vector3(randomRange, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }
}
