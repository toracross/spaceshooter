using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Power Ups
    public bool canTripleShot = false;
    //Movement
    [SerializeField] private float _speed = 5.0f;
    //Attacking
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _canFire = 0.0f;

    //Start is called before the first frame update
    private void Start() {
       
    }

    //Update is called once per frame
    private void Update() {
        Movement();
        Attack();
    }

    private void Movement() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical"); 

        transform.Translate(Vector3.right * _speed * horizontal * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * vertical * Time.deltaTime);

        if (transform.position.y > 0) {
            transform.position = new Vector3(transform.position.x, 0, 0);
        } else if (transform.position.y < -4.2f) {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f) {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        } else if (transform.position.x < -9.5f) {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            if (Time.time > _canFire) {
                if (canTripleShot) {
                    Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                } else {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                }

                _canFire = Time.time + _fireRate;
            }
        }
    }

    public void TripleShotPowerupOn() {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

}
