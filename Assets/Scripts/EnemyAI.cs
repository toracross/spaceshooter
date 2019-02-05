using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] private readonly float _speed = 4.0f;
    [SerializeField] private GameObject _enemyExplosionPrefab;
    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start() {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7) {
            float randomX = Random.Range(-7, 7);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        int score = 0;

        switch (other.tag) {
            case "Laser":
                Destroy(other.gameObject);
                score = 15;
                break;
            case "Player":
                Player player = other.GetComponent<Player>();
                if (player != null) {
                    player.Damage();
                    score = -10;
                }
                break;
            default: break;
        }

        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        _uiManager.UpdateScore(score);
        Destroy(this.gameObject);
    }
}
