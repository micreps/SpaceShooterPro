using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Powerup : MonoBehaviour
{
    

    [SerializeField]
    private float _speed = 3f;

    [SerializeField] //0 = Triple Shot, 1 = Speed, 2 = Shields
    private int powerup_ID;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.88f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                switch (powerup_ID)
                {
                    case 0:
                        player.TripleShotPowerup();
                        break;
                    case 1:
                        player.SpeedPowerup();
                        break;
                    case 2:
                        player.ShieldPowerup();
                        break;
                    default:
                        break;
                }
            }

            Destroy(this.gameObject);



        }
        //if (other.tag == "Laser")
        //{
            
        //    Destroy(this.gameObject);


        //}

    }



}
