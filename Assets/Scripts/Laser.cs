using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Laser : MonoBehaviour
{


    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 8f;

    [SerializeField]
    public bool _isEnemyLaser = false;
    [SerializeField]
    public int _PlayerLaserNum;
    private int _difficulty;
    void Start()
    {
        _difficulty = PlayerPrefs.GetInt("Difficulty", 2);
    }

    // Update is called once per frame
    void Update()
    {

        if (_isEnemyLaser == false)
        {
            
            Shoot();
            KillLaser();

        }
        else
        {
            _speed = _speed + _difficulty;
            ShootDown();
            KillLaserDown();

        }
        
    }

    void Shoot()
    {
        //float _speed = 5f;

        //Vector3 direction = new Vector3(0, 5, 0);
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void KillLaser()
    {
        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Laser.Destroy(transform.parent.gameObject);
            }
            else
            {
                Laser.Destroy(this.gameObject);
            }
           

        }
    }


    void ShootDown()
    {
        //float _speed = 5f;

        //Vector3 direction = new Vector3(0, 5, 0);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void KillLaserDown()
    {
        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Laser.Destroy(transform.parent.gameObject);
            }
            else
            {
                Laser.Destroy(this.gameObject);
            }


        }
    }

    public void AssignEnemyLaser()
    {

        _isEnemyLaser = true;
    }

    public void Player1Laser()
    {

        _PlayerLaserNum = 1;
    }
    public void Player2Laser()
    {

        _PlayerLaserNum = 2;
    }

    public void EnemyLaserNum()
    {
        _PlayerLaserNum = 3;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Laser.Destroy(this.gameObject);
            }
        }
       
    }
}
