using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Button_Script : MonoBehaviour
{
    [SerializeField]
    private int _Button_ID;

    [SerializeField]
    private int _gametype = 0;
    // 1 - singleplayer, 2 - co-op, 3 - story mode, 4 - survival
    //private Button btn;
    private Button btn_single;
    private Button btn_coop;
    private Button btn_story;
    private Button btn_survival;
    private Button btn_startgame;
    private Button btn_options;
    private Button btn_highscores;
    private Button btn_Quit;
    private Button btn_back;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        btn_single = GameObject.Find("Single_Player_Button").GetComponent<Button>();
        if (btn_single == null)
        {
            Debug.LogError("The Single Player Button is NULL");
        }
        btn_coop = GameObject.Find("Co-Op_Button").GetComponent<Button>();
        if (btn_coop == null)
        {
            Debug.LogError("The Co-op Button is NULL");
        }
        btn_story = GameObject.Find("Story_Mode_Button").GetComponent<Button>();
        if (btn_story == null)
        {
            Debug.LogError("The Story Mode Button is NULL");
        }
        btn_survival = GameObject.Find("Survival_Button").GetComponent<Button>();
        if (btn_survival == null)
        {
            Debug.LogError("The Survival Mode Button is NULL");
        }
        if (sceneName == "Main_Menu")
        {
            btn_startgame = GameObject.Find("Start_Game_Button").GetComponent<Button>();
            if (btn_startgame == null)
            {
                Debug.LogError("The Start Game Button Button is NULL");
            }
            btn_Quit = GameObject.Find("Quit_button").GetComponent<Button>();
            if (btn_Quit == null)
            {
                Debug.LogError("The Quit Game Button Button is NULL");
            }
            btn_options = GameObject.Find("Options_Button").GetComponent<Button>();
            if (btn_options == null)
            {
                Debug.LogError("The Options Button is NULL");
            }
            btn_highscores = GameObject.Find("High_Scores_Button").GetComponent<Button>();
            if (btn_highscores == null)
            {
                Debug.LogError("The High Scores Button is NULL");
            }
            btn_startgame.onClick.AddListener(TaskOnClick5);
            btn_startgame.interactable = false;
            btn_Quit.onClick.AddListener(TaskOnClick6);
            btn_highscores.onClick.AddListener(TaskOnClick7);
            btn_options.onClick.AddListener(TaskOnClick8);

        }
        if (sceneName == "High_Scores")
        {
            btn_back = GameObject.Find("Back_button").GetComponent<Button>();
            if (btn_back == null)
            {
                Debug.LogError("The Back Button is NULL");
            }

            btn_back.onClick.AddListener(TaskOnClick9);
           //btn_single.interactable = false;
        }


            btn_single.Select();
        btn_single.onClick.AddListener(TaskOnClick);

        btn_coop.onClick.AddListener(TaskOnClick2);

        btn_story.onClick.AddListener(TaskOnClick3);

        btn_survival.onClick.AddListener(TaskOnClick4);
       


    }

    void TaskOnClick()
    {
        // 1 - singleplayer, 2 - co-op, 3 - story mode, 4 - survival

        btn_single.interactable = false;
        btn_coop.interactable = true;
        btn_story.interactable = true;
        btn_survival.interactable = true;
        
        if (sceneName != "High_Scores")
        {


            btn_startgame.interactable = false;
        }
        btn_story.Select();
        _gametype = 10;


        //btn_story.interactable = false;
        //btn_survival.interactable = true;
        // btn_story.interactable = true;
        // btn_survival.interactable = false;


    }
    void TaskOnClick2()
    {
        // 1 - singleplayer, 2 - co-op, 3 - story mode, 4 - survival
        _gametype = 20;


        btn_single.interactable = true;
        btn_coop.interactable = false;
        btn_story.Select();
        btn_story.interactable = true;
        btn_survival.interactable = true;
        if (sceneName != "High_Scores")
        {


            btn_startgame.interactable = false;
        }



        //btn.interactable = false;
    }
    void TaskOnClick3()
    {
        // 1 - singleplayer, 2 - co-op, 3 - story mode, 4 - survival


        btn_story.interactable = false;
        
        btn_survival.interactable = true;
        btn_startgame.interactable = true;
        
        if (_gametype == 10)
        {
            _gametype = 11;

        }
        if (_gametype == 20)
        {
            _gametype = 21;

        }
        if (sceneName != "High_Scores")
        {


            btn_startgame.interactable = true;
            btn_startgame.Select();
        }
        //btn.interactable = false;
    }

    void TaskOnClick4()
    {
        // 1 - singleplayer, 2 - co-op, 3 - story mode, 4 - survival

        btn_story.interactable = true;
        btn_survival.interactable = false;
        //btn_startgame.interactable = true;
        if (_gametype == 10)
        {
            _gametype = 12;

        }
        if (_gametype == 20)
        {
            _gametype = 22;

        }
        if (sceneName != "High_Scores")
        {


            btn_startgame.interactable = true;
            btn_startgame.Select();
        }
        
        //btn.interactable = false;
    }

    void TaskOnClick5()
    {
        // 1 - singleplayer, 2 - co-op, 3 - story mode, 4 - survival

        //btn_story.interactable = true;
        //btn_survival.interactable = false;
        if (_gametype == 11)
        {
            SceneManager.LoadScene("Single_Player");

        }
        if (_gametype == 21)
        {
            SceneManager.LoadScene("Co-Op_Mode");

        }
        if (_gametype == 12)
        {
            SceneManager.LoadScene("Single_PlayerSurvival");

        }
        if (_gametype == 22)
        {
            SceneManager.LoadScene("Co-Op_ModeSurvival");

        }

        //btn.interactable = false;
    }
    void TaskOnClick6()
    {
        Application.Quit();

    }
    void TaskOnClick7()
    {
        SceneManager.LoadScene("High_Scores");

    }
    void TaskOnClick8()
    {
        SceneManager.LoadScene("Options");

    }
    void TaskOnClick9()
    {
        SceneManager.LoadScene("Main_Menu");

    }
}
