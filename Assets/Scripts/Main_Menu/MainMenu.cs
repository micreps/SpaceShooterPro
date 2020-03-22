using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager _Gamemanager;

    private void Start()
    {
        _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }
    public void LoadGame()
    {
        if (_Gamemanager.isCoopMode == true)
        {
            SceneManager.LoadScene("Co-Op_Mode");

        }
        else if (_Gamemanager.isCoopMode == false)
        {
            SceneManager.LoadScene("Single_Player");

        }
    }
}
