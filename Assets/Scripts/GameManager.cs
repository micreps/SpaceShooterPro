using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public bool _isGameOver = false;

    public bool isCoopMode = false;

    public bool isStoryMode = true;

    private SpawnManager _spawnManager;
    private Animator _PauseAnimator; 

    [SerializeField]
    private int playerID = 0;
    [SerializeField]
    private Rewired.Player player;

    [SerializeField]
    private int[] _HighScores;
    [SerializeField]
    private string[] _HighScoreNames;
    [SerializeField]
    private int[] _HighStreakValues;
    [SerializeField]
    private Button _pauseResume_btn;
    [SerializeField]
    private Button _MainMenu_btn;

    private string _Gametypestring;

    private HighScoreTable _HighScoreTable;
    [SerializeField]
    private GameObject _PausePanel;

    private UIManager _uimanager;

    private bool _needtoenterinitials = false;
    [SerializeField]
    private int _p1Score;
    [SerializeField]
    private int _p2Score;
    [SerializeField]
    private int _bothplayertotalscore;
    private bool _sethighscores = false;

    private HighScoreRecord _HighScoreRecord;

    private int HighKillStreakP1 = 0;
    private int HighKillStreakP2 = 0;
    private int _difficulty;

    


    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName != "Main_Menu" && sceneName != "High_Scores")
        {
            player = ReInput.players.GetPlayer(playerID);
            _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
            
            _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
            _PauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
            _PauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            Time.timeScale = 1;
            AudioListener.pause = false; 
            _PausePanel.SetActive(false);
            if (isStoryMode == false)
            {
                
                _spawnManager.SetSurvivalMode();
            }

        }

        
        if (isStoryMode == true && isCoopMode == false)
        {
            _Gametypestring = "StorySingle";

        }
        else if (isStoryMode == true && isCoopMode == true)
        {
            _Gametypestring = "StoryCoop";

        }
        else if (isStoryMode == false && isCoopMode == false)
        {
            _Gametypestring = "SurviveSingle";

        }
        else if (isStoryMode == false && isCoopMode == true)
        {
            _Gametypestring = "SurviveCoop";

        }
        

        for (int i = 0; i < 10; i++)
        {

            _HighScores[i] = PlayerPrefs.GetInt(_Gametypestring + "Score" + i, 0);


        }
        for (int i = 0; i < 10; i++)
        {

            _HighScoreNames[i] = PlayerPrefs.GetString(_Gametypestring + "Initials" + i, "AAA");


        }
        for (int i = 0; i < 10; i++)
        {

            _HighStreakValues[i] = PlayerPrefs.GetInt(_Gametypestring + "HighStreak" + i, 0);


        }
        _difficulty = PlayerPrefs.GetInt("Difficulty", 2);
       /* if (sceneName == "High_Scores")
        {

            _HighScoreTable = GameObject.Find("_HighScoreTable").GetComponent<HighScoreTable>();
            _HighScoreTable.Reset_Scores();
        }*/
        
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");

    }

    private void Update()
    {
        //player.GetButtonDown("ReloadGame") &&
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName != "Main_Menu" && sceneName != "High_Scores" && sceneName != "Options")
        {
            
            if (player.GetButtonDown("PauseGame") && _isGameOver == false)
            {
                _uimanager.PauseGame();
                
                    if (Time.timeScale == 1)
                    {
                        
                    Time.timeScale = 0;
                        AudioListener.pause = true;
                        _PauseAnimator.SetBool("isPaused", true);
                    EventSystem.current.SetSelectedGameObject(null);
                    _pauseResume_btn = GameObject.Find("Resume_Button").GetComponent<Button>();
                    _pauseResume_btn.Select();
                }
                    else
                    {

                    //_MainMenu_btn = GameObject.Find("Back_To_Main_Menu_Button").GetComponent<Button>();
                   //_MainMenu_btn.Select();
                    Time.timeScale = 1;
                        AudioListener.pause = false;
                        _PausePanel.SetActive(false);
                    }
                
            }
            if (player.GetButtonDown("ReloadGame") && _isGameOver == true && _needtoenterinitials == false)
            {
                if (isCoopMode == true && isStoryMode == true)
                {
                    SceneManager.LoadScene("Co-Op_Mode");

                }
                else if (isCoopMode == false && isStoryMode == true)
                {
                    SceneManager.LoadScene("Single_Player");
                }
                else if (isCoopMode == true && isStoryMode == false)
                {
                    SceneManager.LoadScene("Co-Op_ModeSurvival");

                }
                else if (isCoopMode == false && isStoryMode == false)
                {
                    SceneManager.LoadScene("Single_PlayerSurvival");
                }



            }
            if (player.GetButtonDown("BackToMenu") && _isGameOver == true && _needtoenterinitials == false)
            {
                
                    SceneManager.LoadScene("Main_Menu");

                
                



            }

            if (_isGameOver == true && _sethighscores == false && isCoopMode == false)
            {
                
                
                //_HighScores = PlayerPrefs.GetInt("Score1", 0);
                _sethighscores = true;
                if (_p1Score > _HighScores[9] && _HighScores[0] != 0)
                {

                    //New High Score!
                    _uimanager.SetHighScoreUI();
                    //Enter Initials



                }
                else if (_HighScores[0] == 0)
                {
                    _uimanager.SetHighScoreUI();
                }


             }
            if (_isGameOver == true && _sethighscores == false && isCoopMode == true)
            {
                _bothplayertotalscore = _p1Score + _p2Score;

                //_HighScores = PlayerPrefs.GetInt("Score1", 0);
                _sethighscores = true;
                if (_bothplayertotalscore > _HighScores[9] && _HighScores[0] != 0)
                {

                    //New High Score!
                    _uimanager.SetHighScoreUICoop();
                    //Enter Initials



                }
                else if (_HighScores[0] == 0)
                {
                    _uimanager.SetHighScoreUICoop();
                }


            }







        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");

        } 

    }


    public void SaveHighScores(string _HSinitials)
    {
        Debug.Log("Test1");
        if (_p1Score > _HighScores[9] && _HighScores[0] != 0)
        {
            Debug.Log("Test2");
            //New High Score!
            //_uimanager.SetHighScoreUI();
            //Enter Initials

            /*
             * 
                private int[] _HighScores;         
                private string[] _HighScoreNames;
                private int[] _HighStreakValues;
             * 
             * 
             * */

            for (int x = 0; x < 10; x++)
            {
                if (_p1Score > _HighScores[x])
                {
                    for (int y = 8; y >= 0; y--)
                    {
                        if (y > x)
                        {

                            PlayerPrefs.SetInt(_Gametypestring + "Score" + (y + 1), _HighScores[y]);
                            PlayerPrefs.SetString(_Gametypestring + "Initials" + (y + 1), _HighScoreNames[y]);
                            PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (y + 1), _HighStreakValues[y]);

                        }
                        else if (x == y)
                        {
                            PlayerPrefs.SetInt(_Gametypestring + "Score" + (y + 1), _HighScores[y]);
                            PlayerPrefs.SetString(_Gametypestring + "Initials" + (y + 1), _HighScoreNames[y]);
                            PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (y + 1), _HighStreakValues[y]);
                            PlayerPrefs.SetInt(_Gametypestring + "Score" + (y), _p1Score);
                            PlayerPrefs.SetString(_Gametypestring + "Initials" + (y), _HSinitials);
                            PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (y), HighKillStreakP1);

                            y = -1;
                            x = 10;
                            break;

                        }



                    }






                    break;
                }




            }


        }
        else if (_HighScores[0] == 0)
        {
            Debug.Log("Test3");
           // _uimanager.SetHighScoreUI();
            PlayerPrefs.SetInt(_Gametypestring + "Score" + (0), _p1Score);
            PlayerPrefs.SetString(_Gametypestring + "Initials" + (0), _HSinitials);
            PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (0), HighKillStreakP1);
        }




    }


    private bool _P1HighScore = false;
    private bool _P2HighScore = false;



    public void HighScoreP1()
    {
        _P1HighScore = true;

    }
    public void HighScoreP2()
    {
        _P2HighScore = true;

    }


    public void SaveHighScoresCoop(string _HSinitials)
    {
        Debug.Log("Testsavehighscores");
       // if (_P1HighScore == true && _P2HighScore == true)
        //{


            Debug.Log("Test1");
            if ((_p1Score + _p2Score) > _HighScores[9] && _HighScores[0] != 0)
            {
                Debug.Log("Test2");
                //New High Score!
                //_uimanager.SetHighScoreUI();
                //Enter Initials

                /*
                 * 
                    private int[] _HighScores;         
                    private string[] _HighScoreNames;
                    private int[] _HighStreakValues;
                 * 
                 * 
                 * */

                for (int x = 0; x < 10; x++)
                {
                    if ((_p1Score + _p2Score) > _HighScores[x])
                    {
                        for (int y = 8; y >= 0; y--)
                        {
                            if (y > x)
                            {

                                PlayerPrefs.SetInt(_Gametypestring + "Score" + (y + 1), _HighScores[y]);
                                PlayerPrefs.SetString(_Gametypestring + "Initials" + (y + 1), _HighScoreNames[y]);
                                PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (y + 1), _HighStreakValues[y]);

                            }
                            else if (x == y)
                            {
                                PlayerPrefs.SetInt(_Gametypestring + "Score" + (y + 1), _HighScores[y]);
                                PlayerPrefs.SetString(_Gametypestring + "Initials" + (y + 1), _HighScoreNames[y]);
                                PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (y + 1), _HighStreakValues[y]);
                                PlayerPrefs.SetInt(_Gametypestring + "Score" + (y), _p1Score + _p2Score);
                                PlayerPrefs.SetString(_Gametypestring + "Initials" + (y), _HSinitials);

                                if (HighKillStreakP2 > HighKillStreakP1)
                                {
                                    PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (y), HighKillStreakP2);
                                }
                                else
                                {
                                    PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (y), HighKillStreakP1);

                                }
                                

                                y = -1;
                                x = 10;
                                break;

                            }



                        }






                        break;
                    }




                }


            }
            else if (_HighScores[0] == 0)
            {
                Debug.Log("Test3");
                // _uimanager.SetHighScoreUI();
                PlayerPrefs.SetInt(_Gametypestring + "Score" + (0), _p1Score + _p2Score);
                PlayerPrefs.SetString(_Gametypestring + "Initials" + (0), _HSinitials);

                if (HighKillStreakP2 > HighKillStreakP1)
                {
                    PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (0), HighKillStreakP2);
                }
                else
                {
                    PlayerPrefs.SetInt(_Gametypestring + "HighStreak" + (0), HighKillStreakP1);

                }
                
            }

        //}


    }

    public void GamerOver()
    {

        _isGameOver = true;

    }


    public void SetP1Score(int p1score)
    {

        _p1Score = p1score;



    }
    public void SetP2Score(int p2score)
    {

        _p2Score = p2score;



    }

    public void StopGameOverInput()
    {

        _needtoenterinitials = false;
    }
    public void StartGameOverInput()
    {

        _needtoenterinitials = true;
    }
    

    public void SetHighKillStreakP1(int _HighKillStreakP1)
    {

        HighKillStreakP1 = _HighKillStreakP1;
    

    }
    public void SetHighKillStreakP2(int _HighKillStreakP2)
    {

        HighKillStreakP2 = _HighKillStreakP2;

    }




}
