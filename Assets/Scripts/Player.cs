using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


public class Player : MonoBehaviour
{
    //public or private reference
    //data type (int, float, bool, string)
    //every variable has a name
    //optional value assigned
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _livesPlayer2 = 3;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private bool _shield;
    [SerializeField]
    private int _shieldstrength;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisual;


    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private int _killstreak = 0;
    private int _HighKillStreak = 0;

    private int _combinedlives = 6;

    [SerializeField]
    private GameObject _engine_right;
    [SerializeField]
    private GameObject _engine_left;
    [SerializeField]
    private GameObject _Thruster;
    [SerializeField]
    private int _enginenumber;
    [SerializeField]
    private AudioClip _LaserSound;
    [SerializeField]
    private AudioClip _PowerupSound;
    [SerializeField]
    private AudioClip _explode;

    private bool _onlyoneLaserHit = true;


    private SpriteRenderer _Left_Engine_Sprite;
    private SpriteRenderer _Right_Engine_Sprite;



    private BoxCollider2D _playerCollider;
    [SerializeField]
    private GameObject _explosionPrefab;
    AudioSource _Audiosource;
    SpriteRenderer _playersprite;
    SpriteRenderer _shieldsprite;

    private Animator _PlayerAnimator;

    //private bool _gameover = false;
    private UIManager _uiMAnager;



    private SpawnManager _spawnManager;
    private GameManager _Gamemanager;
    [SerializeField]
    private int _playerNumber;

    [SerializeField]
    private int playerID;
    [SerializeField]
    private Rewired.Player player;

    private bool _gamestarted = false;
    private float _introspeed = 10f;
    private int _difficulty;

    private bool _LevelDone = false;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        //PlayerPrefs.DeleteAll();
        if (_playerNumber == 1)
        {
            //transform.position = new Vector3(-5f, -2.7f, 0f);

        }
        else if (_playerNumber == 2)
        {
            //transform.position = new Vector3(5f, -2.7f, 0f);

        }
        else
        {
            //transform.position = new Vector3(0, 0, 0);

        }

        player = ReInput.players.GetPlayer(playerID);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiMAnager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _enginenumber = Random.Range(1, 3);
        _Audiosource = GetComponent<AudioSource>();
        _playersprite = GetComponent<SpriteRenderer>();
        _playerCollider = GetComponent<BoxCollider2D>();
        _PlayerAnimator = GetComponent<Animator>();
        _Gamemanager = GetComponent<GameManager>();




        //_Left_Engine_Sprite = GameObject.Find("Left_Engine").GetComponent<SpriteRenderer>();
        // _Right_Engine_Sprite = GameObject.Find("Right_Engine").GetComponent<SpriteRenderer>();

        if (_playerCollider == null)
        {
            Debug.LogError("The Player Collider is NULL.");

        }
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");

        }
        if (_uiMAnager == null)
        {
            Debug.LogError("The UI Manager is NULL.");

        }
        if (_Audiosource == null)
        {
            Debug.LogError("Audiosource on the player is NULL.");
        }
        _difficulty = PlayerPrefs.GetInt("Difficulty", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gamestarted == false)
        {
            transform.Translate(Vector3.up * _introspeed * Time.deltaTime);
            if (_introspeed > 3.5f)
            {
                _introspeed = _introspeed - 0.5f;
            }

            if (transform.position.y > 0f && _playerNumber == 1)
            {
                _gamestarted = true;

            }
            else if (transform.position.y > -1f && _playerNumber == 2)
            {
                _gamestarted = true;

            }
            else if (transform.position.y > -0f)
            {
                _gamestarted = true;

            }


        }
        else if (_LevelDone == false)
        {
            CalculateMovement();

            if (player.GetButtonDown("Shoot") && Time.time > _canFire)
            {
                ShootLaser();

            }



        }

        if (_LevelDone == true)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            _PlayerAnimator.SetTrigger("OnPlayerRest");

            _speed = _speed + 0.5f;
            //if (_introspeed > 3.5f)
            // {
            //_introspeed = _introspeed - 0.5f;
            // }

            if (transform.position.y > 12f && _playerNumber == 1)
            {
                this.gameObject.SetActive(false);

            }
            else if (transform.position.y > 12f && _playerNumber == 2)
            {
                this.gameObject.SetActive(false);

            }



        }


        //if (_gameover == true)
        //{
        //if (Input.GetKeyDown(KeyCode.R))
        // {
        //Restart Level
        //_gameover = false;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // }

        //}


    }

    [SerializeField]
    float horizontalInput;
    [SerializeField]
    float verticalInput;


    void CalculateMovement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        horizontalInput = player.GetAxis("Move_Horizontal");
        verticalInput = player.GetAxis("Move_Vertical");
        if (horizontalInput > 0f)
        {
            _PlayerAnimator.SetTrigger("OnPlayerGoRight");
            _engine_right.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            _engine_left.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

            _engine_right.transform.localPosition = new Vector3(1.3f, -3.3f, 0);
            _engine_left.transform.localPosition = new Vector3(-1.5f, -3.55f, 0);
            /*
            if (_engine_right.activeInHierarchy)
            {
                _Right_Engine_Sprite.sortingOrder = 1;
            }
            if (_engine_left.activeInHierarchy)
            {
                _Left_Engine_Sprite.sortingOrder = 3;
            }
            */
        }
        if (horizontalInput < 0f)
        {
            _PlayerAnimator.SetTrigger("OnPlayerGoLeft");
            _engine_left.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            _engine_right.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

            _engine_left.transform.localPosition = new Vector3(-1.3f, -3.3f, 0);
            _engine_right.transform.localPosition = new Vector3(1.5f, -3.55f, 0);
            /*
            if (_engine_right.activeInHierarchy)
            {
                _Right_Engine_Sprite.sortingOrder = 3;
            }
            if (_engine_left.activeInHierarchy)
            {
                _Left_Engine_Sprite.sortingOrder = 1;
            }
            */
        }
        if (horizontalInput == 0f)
        {
            _PlayerAnimator.SetTrigger("OnPlayerRest");
            _engine_right.transform.localScale = new Vector3(1f, 1f, 1f);
            _engine_left.transform.localScale = new Vector3(1f, 1f, 1f);

            _engine_right.transform.localPosition = new Vector3(1.5f, -3.55f, 0);
            _engine_left.transform.localPosition = new Vector3(-1.5f, -3.55f, 0);
            /*
            if (_engine_right.activeInHierarchy)
            {
                _Right_Engine_Sprite.sortingOrder = 3;
            }
            if (_engine_left.activeInHierarchy)
            {
                _Left_Engine_Sprite.sortingOrder = 3;
            }
            */
        }

        if (verticalInput > 0f)
        {

            _Thruster.transform.localScale = new Vector3(1f, 1.1f, 1f);
            _Thruster.transform.localPosition = new Vector3(0f, -3.3f, 0f);
        }
        if (verticalInput < 0)
        {
            _Thruster.transform.localScale = new Vector3(1f, 0.9f, 1f);
            _Thruster.transform.localPosition = new Vector3(0f, -3.1f, 0f);

        }
        if (verticalInput == 0)
        {
            _Thruster.transform.localScale = new Vector3(1f, 1f, 1f);
            _Thruster.transform.localPosition = new Vector3(0f, -3.2f, 0f);
        }




        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);

        }


    }

    void ShootLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            //GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0f, -1.35f), Quaternion.identity);
            //Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            GameObject PlayerLaser = Instantiate(_tripleShotPrefab, transform.position + new Vector3(-1.3f, 1.05f, 0), Quaternion.identity);
            Laser[] lasers = PlayerLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                if (_uiMAnager._p1lives > 0)
                {
                    if (_playerNumber == 1)
                    {
                        lasers[i].Player1Laser();

                    }
                    else
                    {
                        lasers[i].Player1Laser();

                    }
                }
                if (_uiMAnager._p2lives > 0)
                {
                    if (_playerNumber == 2)
                    {
                        lasers[i].Player2Laser();

                    }
                }



            }
        }
        else
        {
            GameObject PlayerLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            Laser[] lasers = PlayerLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                if (_uiMAnager._p1lives > 0)
                {
                    if (_playerNumber == 1)
                    {
                        lasers[i].Player1Laser();

                    }
                    else
                    {
                        lasers[i].Player1Laser();

                    }
                }
                if (_uiMAnager._p2lives > 0)
                {
                    if (_playerNumber == 2)
                    {
                        lasers[i].Player2Laser();

                    }
                }


            }


        }
        _Audiosource.clip = _LaserSound;
        _Audiosource.Play();

    }

    public void LevelDone()
    {

        Debug.Log("Boss Dead");
        _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _speed = 1;
        if (_Gamemanager.isCoopMode == false)
        {

            //_spawnManager.OnPlayerDeath();
            _uiMAnager.DisplayGameOverText();
            _Gamemanager.SetP1Score(_score);
            //_Gamemanager._isGameOver = true;
            // this.gameObject.SetActive(false);
            _LevelDone = true;
            _Gamemanager._isGameOver = true;
            //Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
            //Destroy(this.gameObject);



        }
        //_Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_Gamemanager.isCoopMode == true)
        {


            // _spawnManager.OnPlayerDeath();
            _uiMAnager.DisplayGameOverText();
            _Gamemanager.SetP1Score(_score);
            _Gamemanager.SetP2Score(_score);
            _LevelDone = true;

            _Gamemanager._isGameOver = true;

            //Destroy(this.gameObject);



        }



    }

    public void SetLevelDone()
    {
        _LevelDone = true;

    }



    public void Damage()
    {
        
        if (_onlyoneLaserHit == true)
        {
            _onlyoneLaserHit = false;
            if (_shield == true && _shieldstrength == 1)
            {


                _shieldsprite = GameObject.Find("Shields").GetComponent<SpriteRenderer>();
                 _shieldsprite.color = new Color(1f, 1f, 1f, 0.5f);
                //Do something with sprite here

               
                //_shieldVisual.SetActive(false);
                _onlyoneLaserHit = true;
                _shieldstrength--;
                return;

            }
            if (_shield == true && _shieldstrength == 0)
            {
                _shield = false;
                _shieldVisual.SetActive(false);
                _onlyoneLaserHit = true;
                return;

            }
            _playerCollider.enabled = false;
            StartCoroutine(PlayerFlicker());
            _Audiosource.clip = _explode;
            _Audiosource.Play();
           
            if (_playerNumber == 1)
            {
                _lives--;
                _uiMAnager.UpdateGlobalP1Lives(_lives);
            }
            else if (_playerNumber == 2)
            {
                _livesPlayer2--;
                _uiMAnager.UpdateGlobalP2Lives(_livesPlayer2);

            }
            




            if (_enginenumber == 2)
            {
                _enginenumber--;
                _engine_right.SetActive(true);
               _Right_Engine_Sprite = GameObject.Find("Right_Engine").GetComponent<SpriteRenderer>();
            }
            else if (_enginenumber == 1)
            {
                _enginenumber = 2;
                _engine_left.SetActive(true);
                _Left_Engine_Sprite = GameObject.Find("Left_Engine").GetComponent<SpriteRenderer>();

            }

            if (_playerNumber == 1)
            {
                _killstreak = 0;
                _uiMAnager.ClearCombo();
                _uiMAnager.UpdateLives(_lives);

            }
            else if (_playerNumber == 2)
            {
                _killstreak = 0;
                _uiMAnager.ClearComboPlayer2();
                _uiMAnager.UpdateLivesPlayer2(_livesPlayer2);

            }
            else
            {
                _killstreak = 0;
                _uiMAnager.ClearCombo();
                _uiMAnager.UpdateLives(_lives);

            }
            _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
            if (_uiMAnager._p1lives < 1 && _playerNumber == 1 && _Gamemanager.isCoopMode == false)
            {
               Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);

                
                if (_uiMAnager._p1lives < 1 && _Gamemanager.isCoopMode == false)
                {
                    _spawnManager.OnPlayerDeath();
                    _uiMAnager.DisplayGameOverText();
                    _Gamemanager.SetP1Score(_score);
                    //_Gamemanager._isGameOver = true;
                    this.gameObject.SetActive(false);
                    
                    _Gamemanager._isGameOver = true;
                    //Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
                    //Destroy(this.gameObject);


                }


                
                //Destroy(this.gameObject);


            }
            //_Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
            if (_uiMAnager._p1lives < 1 && _playerNumber == 1 && _Gamemanager.isCoopMode == true)
            {
                Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
               // _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
                if (_uiMAnager._p1lives < 1 && _uiMAnager._p2lives < 1)
                {


                    _spawnManager.OnPlayerDeath();
                    _uiMAnager.DisplayGameOverText();
                    
                    _Gamemanager._isGameOver = true;
                    //_gameover = true;
                    //Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
                    //Destroy(this.gameObject);


                }
                //Destroy(this.gameObject);
                _Gamemanager.SetP1Score(_score);
                this.gameObject.SetActive(false);

            }
            if (_uiMAnager._p2lives < 1 && _playerNumber == 2 && _Gamemanager.isCoopMode == true)
            {
                Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
               // _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
                if (_uiMAnager._p1lives < 1 && _uiMAnager._p2lives < 1)
                {
                    _spawnManager.OnPlayerDeath();
                    _uiMAnager.DisplayGameOverText();
                    
                    _Gamemanager._isGameOver = true;
                    //_gameover = true;
                    //Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
                    //Destroy(this.gameObject);


                }
                _Gamemanager.SetP2Score(_score);
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);

            }

            
        }
    }
    public void TripleShotPowerup()
    {
        _isTripleShotActive = true;
        _Audiosource.clip = _PowerupSound;
        _Audiosource.Play();
        StartCoroutine(PowerupCooldown(0));
        

    }

    IEnumerator PowerupCooldown(int powerup_ID)
    {
        

        yield return new WaitForSeconds(5.0f);
        switch (powerup_ID)
        { 
            case 0:
                _isTripleShotActive = false;
                break;
            case 1:
                _speed = 3.5f;
                break;
            //case 2:
                //_shield = false;
               // break;
            default:
                break;

        }

    }
    public void SpeedPowerup()
    {
        _speed = 8.5f;
        _Audiosource.clip = _PowerupSound;
        _Audiosource.Play();
        StartCoroutine(PowerupCooldown(1));


    }

    public void ShieldPowerup()
    {
        _shield = true;
        _shieldstrength = 1;
        _Audiosource.clip = _PowerupSound;
        _Audiosource.Play();
        _shieldVisual.SetActive(true);
        _shieldsprite = GameObject.Find("Shields").GetComponent<SpriteRenderer>();
        _shieldsprite.color = new Color(1f, 1f, 1f, 1f);
        //StartCoroutine(PowerupCooldown(2));


    }
    public void AddToScore(int points, bool enemyhit)
    {
        if (enemyhit == true)
        {
            _score = _score + (points * _difficulty) + _killstreak;
            _killstreak++;
            if (_killstreak > _HighKillStreak)
            {
                _HighKillStreak = _killstreak;
                
            }

            _uiMAnager.UpdateCombo(_killstreak, _HighKillStreak);
        }
        if (enemyhit == false)
        {
            _score = _score + (points * _difficulty);
        }

        _uiMAnager.UpdateScore(_score);


    }

    public void AddToScorePlayer2(int points, bool enemyhit)
    {
        if (enemyhit == true)
        {

            _score = _score + (points * _difficulty) + _killstreak;
            _killstreak++;
            if (_killstreak > _HighKillStreak)
            {
                _HighKillStreak = _killstreak;

            }

            _uiMAnager.UpdateComboPlayer2(_killstreak, _HighKillStreak);
        }
        if (enemyhit == false)
        {
            _score = _score + (points * _difficulty);
        }

        _uiMAnager.UpdateScorePlayer2(_score);
    }


    IEnumerator PlayerFlicker()
    {
        _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (_Gamemanager.isCoopMode == false)
                {
                    _playersprite.color = new Color(1f, 1f, 1f, 0.5f);
                    //_gameManager.GamerOver();
                    yield return new WaitForSeconds(0.3f);
                    _playersprite.color = new Color(1f, 1f, 1f, 1f);
                    //_gameManager.GamerOver();
                }
                else if (_Gamemanager.isCoopMode == true)
                {
                    if (_playerNumber == 1)
                    {
                        _playersprite.color = new Color(0.6179246f, 0.6938664f, 1f, 0.5f);
                        //_gameManager.GamerOver();
                        yield return new WaitForSeconds(0.3f);
                        _playersprite.color = new Color(0.6179246f, 0.6938664f, 1f, 1f);

                    }
                    if (_playerNumber == 2)
                    {
                        _playersprite.color = new Color(0.9716981f, 0.4720986f, 0.4720986f, 0.5f);
                        //_gameManager.GamerOver();
                        yield return new WaitForSeconds(0.3f);
                        _playersprite.color = new Color(0.9716981f, 0.4720986f, 0.4720986f, 1f);

                    }


                }





                yield return new WaitForSeconds(0.3f);
            }
            _playerCollider.enabled = true;
            _onlyoneLaserHit = true;
            break;

        }
    }
}
