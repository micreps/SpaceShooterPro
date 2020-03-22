using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _smallexplosionprefab;

    [SerializeField]
    private int _enemymodifier = 1;
    [SerializeField]
    private int _EnemyID;


    private bool _enemyshot = false;
    [SerializeField]
    private int _boss1health = 100;
    private Player _player;
    private Player _player1;
    private Player _player2;

    private BoxCollider2D _colliderenemy;
    private UIManager _uiMAnager;
    private Animator _EnemyAnimator;

    [SerializeField]
    private GameObject _laserPrefab;

    private AudioSource _audiosource;
    private float _initialspeed = 3f;
    private float _speed = 3f;

    private float _fireRate = 3.0f;
    private float _CanFireBossLaser = -1;
    private float _CanFire = -1;
    private EnemyMovement _EnemyMove;
    private bool _enemyNotDead = true;
    private GameManager _Gamemanager;
    private int _playerlaservar;
    [SerializeField]
    private bool _boss1turned = false;
    [SerializeField]
    private bool _boss1movedown = false;
    [SerializeField]
    private int _boss1direction = 0;
    [SerializeField]
    private float _rand_down_position;

    [SerializeField]
    private LaserBoss1 _LaserRight;
    [SerializeField]
    private LaserBoss1 _LaserLeft;


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
        
        
        
       

        _EnemyAnimator = GetComponent<Animator>();

        _audiosource = GetComponent<AudioSource>();

        if (_EnemyAnimator == null)
        {
            Debug.LogError("The animator is NULL");
        }

        _colliderenemy = GetComponent<BoxCollider2D>();
        if (_colliderenemy == null)
        {
            Debug.LogError("The collider is NULL");
        }

       if (_EnemyID == 1)
        {
            _LaserLeft = GameObject.Find("boss_laser_left").GetComponent<LaserBoss1>();
            _LaserRight = GameObject.Find("boss_laser_right").GetComponent<LaserBoss1>();


        }


    }

    

    private bool _boss1movein = false;

    // Update is called once per frame
    void Update()
    {

        if (_boss1movein == false && _EnemyID == 1)
        {
            transform.Translate(Vector3.down * 2f * Time.deltaTime);


            if (transform.position.y < 5.4f)
            {
                _boss1movein = true;
            }


        }

        EnemyShoot();

        if (_EnemyID == 1)
        {
           CalculateBoss1Movement();
            if (Time.time > _CanFireBossLaser)
            {
                _fireRate = Random.Range(3f, 7f);
                _CanFireBossLaser = Time.time + _fireRate;
                Debug.Log("CAnfire");
                if (_enemyNotDead == true && _boss1movedown == false && _boss1movein == true)
                {
                    //here
                    Debug.Log("ShouldFire");
                    _LaserLeft.ShootBossLaser();
                    _LaserRight.ShootBossLaser();


                }

            }
        }




    }

    
   

    void CalculateBoss1Movement()
    {

        if (_boss1turned == false && _boss1movedown == true && _boss1movein == true && _boss1direction == 0)
        {
            _speed = _initialspeed;
            transform.Translate(Vector3.down * _speed * Time.deltaTime);


            if (transform.position.y < 4.31f)
            {
                _boss1movedown = false;
            }
        }

        if (_boss1turned == false && _boss1movedown == false && _boss1movein == true && _boss1direction == 0)
        {
            _boss1direction = Random.Range(1, 3);
           

        }
        if (_boss1turned == false && _boss1movedown == false && _boss1movein == true && _boss1direction == 1)
        {
            

            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            if (transform.position.x > 8.75f)
            {
                _boss1turned = true;
                _boss1direction = 2;
            }

        }
        if (_boss1turned == false && _boss1movedown == false && _boss1movein == true && _boss1direction == 2)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            if (transform.position.x < -8.75f)
            {
                _boss1turned = true;
                _boss1direction = 1;
            }

        }
        if (_boss1turned == true && _boss1movedown == false && _boss1movein == true && _boss1direction == 1)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            if (transform.position.x > 8.75f)
            {
                _boss1turned = true;
                _boss1direction = 2;
                _boss1movedown = true;
                _rand_down_position = Random.Range(-8f, 8f);
            }

        }
        if (_boss1turned == true && _boss1movedown == false && _boss1movein == true && _boss1direction == 2)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            if (transform.position.x < -8.75f)
            {
                _boss1turned = true;
                _boss1direction = 1;
                _boss1movedown = true;
                _rand_down_position = Random.Range(-8f, 8f);
            }

        }
        if (_boss1turned == true && _boss1movedown == true && _boss1movein == true && _boss1direction == 1)
        {
           
            if (transform.position.x < _rand_down_position)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                
            }
            if (transform.position.x >= _rand_down_position)
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);

                _speed = _speed + .1f;
                if (transform.position.y < -10f)
                {
                    float randomX = Random.Range(-8f, 8f);
                    transform.position = new Vector3(randomX, 11, -1);
                    _boss1turned = false;
                    //_boss1movedown = false;
                    _boss1direction = 0;

                }

            }

        }
        if (_boss1turned == true && _boss1movedown == true && _boss1movein == true && _boss1direction == 2)
        {
            
            if (transform.position.x > _rand_down_position)
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                
            }
            if (transform.position.x <= _rand_down_position)
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                _speed = _speed + .1f;

                if (transform.position.y < -10f)
                {
                    float randomX = Random.Range(-8f, 8f);
                    transform.position = new Vector3(randomX, 11, -1);
                    _boss1turned = false;
                    //_boss1movedown = false;
                    _boss1direction = 0;
                }

            }


        }

/*

        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -10f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 10, 0);
        }

        */



    }

    public void EnemyInDangerShoot()
    {
        _CanFire = 0;
        Debug.Log("Shooting");

    }

    void EnemyShoot()
    {
        if (Time.time > _CanFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _CanFire = Time.time + _fireRate;

            if (_enemyNotDead == true)
            {


                GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0f, -1.35f), Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].AssignEnemyLaser();
                    lasers[i].EnemyLaserNum();


                }
                //lasers[0].AssignEnemyLaser();
            }

        }


    }




    


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && _EnemyID == 0)
        {
            Player player = other.transform.GetComponent<Player>();
            
            if(player != null)
            {
                player.Damage();
            }
            _EnemyAnimator = GetComponent<Animator>();
            _EnemyAnimator.SetTrigger("OnEnemyDeath");
            _EnemyMove = gameObject.GetComponentInParent<EnemyMovement>();
            _EnemyMove.SetSpeedToZero(3.1f);
            //_speed = 0;
            _enemyNotDead = false;
            _colliderenemy.enabled = false;
            _audiosource.Play();
            
            Destroy(this.gameObject, 3.1f);
            


        }
        
        if (other.tag == "Laser" && _EnemyID == 0)
        {

            _playerlaservar = other.gameObject.GetComponent<Laser>()._PlayerLaserNum;

            if (_playerlaservar == 3)
            {

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
                    if (_enemyshot == false)
                    {
                        _audiosource.Play();
                        if (_Gamemanager.isCoopMode == true)
                        {
                            _player1.AddToScore(_enemymodifier, true);
                        }
                        if (_Gamemanager.isCoopMode == false)
                        {
                            _player.AddToScore(_enemymodifier, true);
                        }





                        _enemyshot = true;

                    }
                }
            }
           if (_uiMAnager._p2lives > 0)
            {


                if (_player2 != null && _playerlaservar == 2)
                {
                    if (_enemyshot == false)
                    {
                        _audiosource.Play();
                        _player2.AddToScorePlayer2(_enemymodifier, true);
                        //  _player2.AddToScorePlayer2(_enemymodifier);


                        _enemyshot = true;

                    }
                    // _player1 != null || _player2 != null


                }
            }
            Destroy(other.gameObject);
            _EnemyAnimator = GetComponent<Animator>();
            _EnemyAnimator.SetTrigger("OnEnemyDeath");
            _EnemyMove = gameObject.GetComponentInParent<EnemyMovement>();
            _EnemyMove.SetSpeedToZero(2.1f);
            _enemyNotDead = false;
            StartCoroutine(ExplodeThenDisableCollider());
            
            Destroy(this.gameObject, 2.1f);
            

        }
        if (other.tag == "Laser" && _EnemyID == 1)
        {

            _playerlaservar = other.gameObject.GetComponent<Laser>()._PlayerLaserNum;

            if (_playerlaservar == 3)
            {

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
                   
                        _audiosource.Play();
                        if (_Gamemanager.isCoopMode == true)
                        {
                            _player1.AddToScore(_enemymodifier, true);
                        }
                        if (_Gamemanager.isCoopMode == false)
                        {
                            _player.AddToScore(_enemymodifier, true);
                        }

                    _boss1health--;





                }
            }
            if (_uiMAnager._p2lives > 0)
            {


                if (_player2 != null && _playerlaservar == 2)
                {
                    
                        _audiosource.Play();
                        _player2.AddToScorePlayer2(_enemymodifier, true);
                    //  _player2.AddToScorePlayer2(_enemymodifier);

                    _boss1health--;

                    // _player1 != null || _player2 != null


                }
            }
            GameObject laserexplode = Instantiate(_smallexplosionprefab, other.transform.position, Quaternion.identity);
            laserexplode.transform.parent = this.transform;


            Destroy(other.gameObject);



            //if enemy health == 0
            if(_boss1health < 1f)
            {
                _LaserLeft.KillBossLaser();
                _LaserRight.KillBossLaser();
                _EnemyAnimator = GetComponent<Animator>();
                _EnemyAnimator.SetTrigger("OnEnemyDeath");
                _enemyNotDead = false;
                
                Debug.Log("EnemyDeath");
                _speed = 0;


                if ((_player != null && _playerlaservar == 1) || (_player1 != null && _playerlaservar == 1))
                {

                    
                    if (_Gamemanager.isCoopMode == true)
                    {
                        _player1.LevelDone();
                        if (_player2 != null)
                        {
                            _player2.SetLevelDone();
                        }

                    }
                    if (_Gamemanager.isCoopMode == false)
                    {
                        _player.LevelDone();
                    }


                }

                if (_player2 != null && _playerlaservar == 2)
                {

                    
                    _player2.LevelDone();
                    if (_player1 != null)
                    {
                        _player1.SetLevelDone();
                    }

                }

                StartCoroutine(ExplodeThenDisableCollider());
                
                Destroy(this.gameObject, 2.1f);
            }


            // _EnemyMove = gameObject.GetComponentInParent<EnemyMovement>();
            // _EnemyMove.SetSpeedToZero();


            


        }
        if (other.tag == "Asteroid" && _EnemyID == 0)
        {

            //Destroy(other.gameObject);
            _EnemyAnimator = GetComponent<Animator>();
            _EnemyAnimator.SetTrigger("OnEnemyDeath");
            _EnemyMove = gameObject.GetComponentInParent<EnemyMovement>();
            _EnemyMove.SetSpeedToZero(2.1f);
            _enemyNotDead = false;
            StartCoroutine(ExplodeThenDisableCollider());

            Destroy(this.gameObject, 2.1f);

        }

    }
    public void SetEnemyModifier(int enemmod)
    {

        _enemymodifier = enemmod;

    }
    IEnumerator ExplodeThenDisableCollider()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1.0f);
            _colliderenemy.enabled = false;
        }



    }
}
