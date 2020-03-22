using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoss1 : MonoBehaviour
{
    // Start is called before the first frame update
    private float _shutoffbosslaser = 0;
    private bool _firebosslaser = false;
    [SerializeField]
    private float _speed = 1f;
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {

        if (_firebosslaser == true)
        {
            //transform.Translate(Vector3.down * 1.5f * Time.deltaTime);
            Vector3 p = transform.localPosition;
            if (_shutoffbosslaser == 0)
            {
                _shutoffbosslaser = Time.time + 3f;
            }
            if (p.y > -2.5)
            {
                transform.localScale += new Vector3(0f, .1f, 0f);


                p.y = p.y - .15f;
                transform.localPosition = p;
            }
        }

        if (_shutoffbosslaser < Time.time && _firebosslaser == true)
        {
            _shutoffbosslaser = 0;
            _firebosslaser = false;
            transform.localScale = new Vector3(0.3333333f, 0f, 0f);
            Vector3 p = transform.localPosition;
            p.y = 0f;
            transform.localPosition = p;

        }
    }

    public void ShootBossLaser()
    {


        _firebosslaser = true;

    }
    public void KillBossLaser()
    {

        
        _firebosslaser = false;
        _shutoffbosslaser = 0;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                //Laser.Destroy(this.gameObject);
            }
        }

    }

} 
