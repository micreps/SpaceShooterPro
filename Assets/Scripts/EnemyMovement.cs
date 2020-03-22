using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private int _enemymodifier = 1;

    [SerializeField]
    private float _speed = 4f;
    private Enemy _Enemy;
    private int _difficulty;
    // Start is called before the first frame update
    void Start()
    {
        _Enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        _enemymodifier = Random.Range(1, 4);
        _difficulty = PlayerPrefs.GetInt("Difficulty", 2);
        _speed = _enemymodifier + 4 + _difficulty;
        _Enemy.SetEnemyModifier(_enemymodifier);

        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }



    void CalculateMovement()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }





    }

    public void CalculateDodgeMovement(bool threatdirection)
    {
        
        if (threatdirection == true)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);

        }
        if (threatdirection == false)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);

        }





    }

    public void SetSpeedToZero(float timetodestroy)
    {

        _speed = 0;
        Destroy(this.gameObject, timetodestroy);
    }

}
