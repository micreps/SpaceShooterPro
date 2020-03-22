using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{

   

    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyBoss1Prefab;
    [SerializeField]
    private GameObject _AsteroidPrefab;

    [SerializeField]
    private GameObject _EnemyContainer;

    [SerializeField]
    private GameObject _AsteroidContainer;

    AudioSource _Audiosource;

    [SerializeField]
    private AudioClip _alarm;
    //[SerializeField]
    //private GameObject _TripleShotPrefab;

    //[SerializeField]
    //private GameObject _SpeedPrefab;
    [SerializeField]
    private bool _SpawnAsteroids;
    private bool _bossspawned = false;

    [SerializeField]
    private GameObject _PowerupContainer;

    [SerializeField]
    private PostProcessFunctions _PPVolume;

    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = true;
    // Start is called before the first frame update
    private bool _stopemergency = true;

    [SerializeField]
    private GameObject _EnemyBoss1;

    private int _difficulty;
    [SerializeField]
    private float _LevelTimer = 0;

    private bool _levelstarted = false;
    [SerializeField]
    private float _TimerStart;


    private bool _survivalmode = false;

    void Start()
    {
        //coroutine = SpawnRoutine();
        _difficulty = PlayerPrefs.GetInt("Difficulty", 2);

        



    }

    // Update is called once per frame
    void Update()
    {
        if (_levelstarted == true)
        {
            _LevelTimer += Time.deltaTime;
        }
        if (_survivalmode == false)
        {


            if (_LevelTimer > _TimerStart + 200f)
            {
                if (_bossspawned == false)
                {
                    _stopSpawning = false;
                    _stopemergency = true;
                    _Audiosource = GetComponent<AudioSource>();
                    _Audiosource.clip = _alarm;
                    _Audiosource.Play();
                    StartCoroutine(EmergencyRoutine());
                    StartCoroutine(EndEmergencyRoutine());
                    Vector3 posToSpawn = new Vector3(0, 11, -1f);
                    //GameObject newEnemy = Instantiate(_EnemyBoss1Prefab, posToSpawn, Quaternion.identity);
                    // newEnemy.transform.parent = _EnemyContainer.transform;
                    _EnemyBoss1.SetActive(true);
                    _bossspawned = true;
                }
            }
        }

    }


    public void SetSurvivalMode()
    {
        _survivalmode = true;


    }


    public void StartSpawning()
    {
        _levelstarted = true;
        _TimerStart = Time.deltaTime;
        _Audiosource = GetComponent<AudioSource>();
        _PPVolume = GameObject.Find("Post Process Volume").GetComponent<PostProcessFunctions>();
        StartCoroutine(EmergencyRoutine());
        _Audiosource.clip = _alarm;
        _Audiosource.Play();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        if (_SpawnAsteroids == true)
        {
            StartCoroutine(SpawnAsteroidRoutine());
        }

    }


    IEnumerator EmergencyRoutine()
    {
       
        while (_stopemergency) { 
        _PPVolume.StartEmergency();
        yield return new WaitForSeconds(0.44f);
        _PPVolume.EndEmergency();
        yield return new WaitForSeconds(0.44f);
            }
    }

    IEnumerator EndEmergencyRoutine()
    {

        yield return new WaitForSeconds(3.0f);
        _Audiosource.Stop();
        _stopemergency = false;

    }

    IEnumerator SpawnEnemyRoutine()
    {
        //yield return null;
        //float randomX = Random.Range(-8f, 8f);
        //transform.position = new Vector3(randomX, 7, 0);
        yield return new WaitForSeconds(3.0f);
        _Audiosource.Stop();
        _stopemergency = false;
        while (_stopSpawning)
        {

            Vector3 posToSpawn = new Vector3(Random.Range(-12f, 12f), 7, -1f);
            GameObject newEnemy = Instantiate(_EnemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;

            yield return new WaitForSeconds(5.0f / _difficulty);
        }

    }

    IEnumerator SpawnAsteroidRoutine()
    {
        //yield return null;
        //float randomX = Random.Range(-8f, 8f);
        //transform.position = new Vector3(randomX, 7, 0);
        yield return new WaitForSeconds(3f);
        
        //_stopemergency = false;
        while (_stopSpawning)
        {

            Vector3 posToSpawn = new Vector3(12, Random.Range(-3f, 8f), 0f);
            GameObject newAsteroid = Instantiate(_AsteroidPrefab, posToSpawn, Quaternion.identity);
            newAsteroid.transform.parent = _AsteroidContainer.transform;

            yield return new WaitForSeconds(3.5f / _difficulty);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning)
        {
            
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, -1f);


            GameObject newPowerup = Instantiate(powerups[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            newPowerup.transform.parent = _PowerupContainer.transform;
 

            
            yield return new WaitForSeconds(Random.Range(5f, 8f) / _difficulty);

        }

           


    }

    public void OnPlayerDeath()
    {
        _stopSpawning = false;
    }

}
