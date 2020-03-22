using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{


    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float _rotatespeed = 19.0f;
    [SerializeField]
    private ScrollBackGround _backgroundScroller;

    [SerializeField]
    private bool _startingAsteroid;

    [SerializeField]
    private bool _AsteroidMovement;

    [SerializeField]
    private int _RandomRotation;

    private float _speed = 2;
    private Player _player;
    private Player _player1;
    private Player _player2;
    private UIManager _uiMAnager;
    private GameManager _Gamemanager;
    private int _playerlaservar;
    private int _difficulty;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {

        _uiMAnager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_Gamemanager.isCoopMode == true)
        {
            if (_uiMAnager._p1lives > 0)
            {
                _player1 = GameObject.Find("Player_1").GetComponent<Player>();
            }
            if (_uiMAnager._p2lives > 0)
            {
                _player2 = GameObject.Find("Player_2").GetComponent<Player>();
            }
            if (_player1 == null)
            {
                Debug.LogError("The Player 1 is NULL");
            }
            if (_player2 == null)
            {
                Debug.LogError("The Player 2 is NULL");
            }
        }
        else
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("The Player is NULL");
            }
        }
        if (_startingAsteroid == true)
        {
            
            _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
            _backgroundScroller = GameObject.Find("BackgroundScroller Main").GetComponent<ScrollBackGround>();
            
        }
        else
        {
            _RandomRotation = Random.Range(1, 3);

        }
        _difficulty = PlayerPrefs.GetInt("Difficulty", 2);
        _speed = _speed * _difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if (_AsteroidMovement == false)
        {
            
            if (_RandomRotation == 1)
            {
                transform.Rotate(Vector3.forward * _rotatespeed * Time.deltaTime);
            }
            else if (_RandomRotation == 2)
            {
                transform.Rotate(Vector3.back * _rotatespeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.forward * _rotatespeed * Time.deltaTime);
            }
            
        }
        if (_AsteroidMovement == true)
        {
            if (_startingAsteroid == false)
            {
                CalculateMovement();

            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "EnemyLeft")
        {
            EnemyMovement _Enemy = other.gameObject.GetComponentInParent<EnemyMovement>();
            _Enemy.CalculateDodgeMovement(false);
            //MoveEnemyRight

        }
        if (other.tag == "EnemyRight")
        {
            EnemyMovement _Enemy = other.gameObject.GetComponentInParent<EnemyMovement>();
            _Enemy.CalculateDodgeMovement(true);
            //MoveEnemyLEft

        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_startingAsteroid == true)
        {
            if (other.tag == "Laser")
            {


                Destroy(other.gameObject);
                Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
                //_Audiosource.Play();

                _backgroundScroller.StartScrolling();
                _spawnManager.StartSpawning();


                //this.gameObject.SetActive(false);
                Destroy(this.gameObject);



            }
        }
        if (_startingAsteroid == false)
        {
            if (other.tag == "Laser")
            {

                _playerlaservar = other.gameObject.GetComponent<Laser>()._PlayerLaserNum;

                if (_playerlaservar == 3)
                {
                    Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
                    //Debug.Log("Explode")
                    //_Audiosource.Play();
                    //_backgroundScroller.StartScrolling();

                    //_spawnManager.StartSpawning();
                    Destroy(other.gameObject);

                    //this.gameObject.SetActive(false);
                    Destroy(this.gameObject);
                    return;
                }
                /*if (_player != null)
                {
                    if (_enemyshot == false)
                    {
                        _audiosource.Play();
                        _player.AddToScore(_enemymodifier);
                        _enemyshot = true;

                    }
                    // _player1 != null || _player2 != null


                }*/
                if (_uiMAnager._p1lives > 0)
                {
                    if ((_player != null && _playerlaservar == 1) || (_player1 != null && _playerlaservar == 1))
                    {
              
                            if (_Gamemanager.isCoopMode == true)
                            {
                                _player1.AddToScore(1, false);
                            }
                            if (_Gamemanager.isCoopMode == false)
                            {
                                _player.AddToScore(1, false);
                            }




                            

                        
                    }
                }
                if (_uiMAnager._p2lives > 0)
                {


                    if (_player2 != null && _playerlaservar == 2)
                    {
                       
                            _player2.AddToScorePlayer2(1,false);
                            //  _player2.AddToScorePlayer2(_enemymodifier);

                        
                        // _player1 != null || _player2 != null


                    }
                }
                //Destroy(other.gameObject);
                //Debug.Log("Destroy LAser")
                Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
                //Debug.Log("Explode")
                //_Audiosource.Play();
                //_backgroundScroller.StartScrolling();

                //_spawnManager.StartSpawning();
                Destroy(other.gameObject);

                //this.gameObject.SetActive(false);
                Destroy(this.gameObject);



            }
        }
        
        if (other.tag == "Enemy")
        {


            //Destroy(other.gameObject);
            //Debug.Log("Destroy LAser")
            Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
            //Debug.Log("Explode")
            //_Audiosource.Play();
            //_backgroundScroller.StartScrolling();

            //_spawnManager.StartSpawning();


            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);



        }



        if (other.tag == "EnemyForward")
        {
            Enemy Enemy = other.transform.GetComponent<Enemy>();
            if (Enemy != null)
            {
                Enemy.EnemyInDangerShoot();

            }
            
            //Enemy Shoot



        }
        



        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {

                player.Damage();
            }
           
            
            

           // Destroy(this.gameObject, 3.1f);

            //Destroy(other.gameObject);
            //Debug.Log("Destroy LAser")
            Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
            //Debug.Log("Explode")
            //_Audiosource.Play();
            //_backgroundScroller.StartScrolling();

            //_spawnManager.StartSpawning();


            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);



        }

    }



    void CalculateMovement()
    {
        Vector3 direction = new Vector3(-0.5f, -0.15f, 0);
        //transform.Translate(direction * _speed * Time.deltaTime);
        //var v3 = new Vector3(5, 0, 5);
        /*

        if (_RandomRotation == 1)
        {
            _asteroiddirection = new Vector3(-0.5f, 0f, -0.5f) - _rotatespeed;
            transform.Rotate(Vector3.forward * _rotatespeed * Time.deltaTime);
        }
        else if (_RandomRotation == 2)
        {
            transform.Rotate(Vector3.back * _rotatespeed * Time.deltaTime);
        }
        */
        //transform.localRotation
        transform.Translate(_speed * direction * Time.deltaTime);
        //this.CalculateMovement(transform.position + v3.normalized * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            //Destroy(this);
        }





    }
}
