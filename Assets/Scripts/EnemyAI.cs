using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] private readonly float _speed = 2.0f;
    [SerializeField] private GameObject _enemyExplosionPrefab;

    // Start is called before the first frame update
    void Start() {
        
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
        switch (other.tag) {
            case "Laser":
                Destroy(other.gameObject);
                break;
            case "Player":
                Player player = other.GetComponent<Player>();
                if (player != null) {
                    player.Damage();
                }
                break;
            default: break;
        }

        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
