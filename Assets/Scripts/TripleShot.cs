using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour{

    [SerializeField] private readonly float _speed = 10.0f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //Destroy the laser if off screen.
        if (transform.position.y >= 6) {
            Destroy(this.gameObject);
        }
    }

}
