using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _powerUpID;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        //Make sure the collider is the player
        if (other.tag == "Player") {
            //Access the Player
            Player player = other.GetComponent<Player>();

            //Make sure player is not null
            if (player != null) {
                switch (_powerUpID) {
                    case 0: player.TripleShotPowerupOn(); break;
                    case 1: player.SpeedBoostPowerupOn(); break;
                    case 2: player.EnableShields(); break;
                    default: break;
                }

                //Destroy the powerup.
                Destroy(this.gameObject);
            }

        }

    }
}
