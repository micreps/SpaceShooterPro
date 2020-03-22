using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    [SerializeField]
    private float _scrollspeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _scrollspeed * Time.deltaTime);
    }

    public void StartScrolling()
    {
        _scrollspeed = 0.1f;


    }
}
