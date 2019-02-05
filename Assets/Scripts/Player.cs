using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Player Statuses
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool shieldsActive = false;
    public int lives = 3;

    //Movement
    [SerializeField] private float _speed = 5.0f;

    //Attacking
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private readonly float _fireRate = 0.25f;
    [SerializeField] private float _canFire = 0.0f;

    //Shield
    [SerializeField] private GameObject _shieldGameObject;

    //Death
    [SerializeField] private GameObject _explosionPrefab;

    //Managers
    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    //Start is called before the first frame update
    private void Start() {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        _uiManager.UpdateLives(lives);
        _spawnManager.StartSpawnRoutines();
    }

    //Update is called once per frame
    private void Update() {
        Movement();
        Attack();
    }

    private void Movement() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (isSpeedBoostActive) {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontal * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * vertical * Time.deltaTime);
        }

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

    //Shields
    public void EnableShields() {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

    //Damage
    public void Damage() {

        if (shieldsActive) {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        lives--;

        if (_uiManager != null) {
            _uiManager.UpdateLives(lives);
        }

        if (lives < 1) {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _uiManager.ShowTitleScreen();
            _gameManager.gameOver = true;
            Destroy(this.gameObject);
        }
    }

    //Speed Boost
    public void SpeedBoostPowerupOn() {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

    //Triple Shot
    public void TripleShotPowerupOn() {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

}
