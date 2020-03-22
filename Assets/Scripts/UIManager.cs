using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;
using System;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _comboText;
    [SerializeField]
    private Text _scoreTextPlayer2;
    [SerializeField]
    private Text _comboTextPlayer2;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Text _PressRText;
    [SerializeField]
    private Text _EnterInitialsText;

    [SerializeField]
    private Text _EnterInitialsP2Text;
    [SerializeField]
    private Text FirstInitialP2_Text;
    [SerializeField]
    private Text SecondInitialP2_Text;
    [SerializeField]
    private Text ThirdInitialP2_Text;

    [SerializeField]
    private Text _HighScoreText;

    [SerializeField]
    private Text _PressEscText;

    [SerializeField]
    private Text _HighScoreInitialUnderscore;
    [SerializeField]
    private Text _HighScoreInitialUnderscoreP2;

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    public int _p1lives = 3;
    [SerializeField]
    public int _p2lives = 3;

    [SerializeField]
    private Rewired.Player player;

    private GameManager _Gamemanager;
    [SerializeField]
    private GameObject _pauseResume_btn;

    private bool _isHighScore = false;

    [SerializeField]
    private GameObject _PausePanel;
    [SerializeField]
    private char[] _Alphabet;

    [SerializeField]
    private Image _LivesImgPlayer2;
    [SerializeField]
    private Sprite[] _liveSpritesPlayer2;

    private bool _activateInitials = false;

    [SerializeField]
    private int playerID = 0;
    [SerializeField]
    private Rewired.Player _player;

    [SerializeField]
    private Rewired.Player player2;
    private bool skipDisabledMaps = true;
    //private GameManager _gameManager;
    // Start is called before the first frame update

    private Char _InitialChar = 'A';
    [SerializeField]
    private Text FirstInitial_Text;
    [SerializeField]
    private Text SecondInitial_Text;
    [SerializeField]
    private Text ThirdInitial_Text;
    private int _alphanumbers = 0;

    [SerializeField]
    private float _timeBetweensteps = 0f;
    [SerializeField]
    private float lastStep;

    [SerializeField]
    private float _timeBetweenstepsP2 = 0f;
    [SerializeField]
    private float lastStepP2;

    [SerializeField]
    private Button _btn_SubmitHighScore;
    [SerializeField]
    private Button _btn_SubmitHighScoreP2;
    private bool _playerswitch = true;

    void Start()
    {
        _GameOverText.gameObject.SetActive(false);
        _comboText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        _Gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_Gamemanager.isCoopMode == true)
        {
            _comboTextPlayer2.gameObject.SetActive(false);
            _scoreTextPlayer2.text = "Score: " + 0;

        }
        //_gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        // _gameManager = GetComponent<GameManager>();
        //if (_gameManager == null)
        // {

        //     Debug.LogError("Game Manager is null");
        // }
        player = ReInput.players.GetPlayer(playerID);

        for (char c = 'A'; c <= 'Z'; c++)
        {

            _Alphabet[_alphanumbers] = c;
            //do something with letter 
            _alphanumbers = _alphanumbers + 1;
        }
        //_PausePanel.SetActive(true);
        //_PausePanel.SetActive(false);

    }


    private bool _secondplayersubmitted = false;

    [SerializeField]
    float verticalInput;
    private int _addLetter = 0;
    private int _addLetterP2 = 0;
    [SerializeField]
    private string _HighScoreInitials;
    [SerializeField]
    private int _initialnumber = 1;
    private bool _initialunderscore_Control = true;
    private bool _initialunderscoreP2_Control = true;
    [SerializeField]
    private int _initialnumberP2 = 1;
    [SerializeField]
    private string _HighScoreInitialsP2;


    private void Update()
    {
        /*
        if (_Gamemanager.isCoopMode == true)
        {
            if (_playerswitch == true)
            {
                _playerswitch = false;
                playerID = 0;
            }
            else if (_playerswitch == false)
            {
                _playerswitch = true;
                playerID = 1;

            }
            player = ReInput.players.GetPlayer(playerID);

        }
        */
        playerID = 0;
        player = ReInput.players.GetPlayer(playerID);

        if (_activateInitials == true && playerID == 0)
        {

            verticalInput = player.GetAxis("UIVertical");

            if (verticalInput > 0f)
            {

                if (Time.time - lastStep > _timeBetweensteps)
                {
                    //_InitialChar = _InitialChar + 1;

                    lastStep = Time.time;
                    if (_addLetter < 25)
                    {
                        _addLetter++;
                    }
                    if (_initialnumber == 1)
                    {
                        FirstInitial_Text.text = _Alphabet[_addLetter] + "";
                    }
                    if (_initialnumber == 2)
                    {
                        SecondInitial_Text.text = _Alphabet[_addLetter] + "";

                    }
                    if (_initialnumber == 3)
                    {
                        ThirdInitial_Text.text = _Alphabet[_addLetter] + "";
                    }




                }

                //StartCoroutine(SlowdownHighScoreInput());

            }



            if (verticalInput < -0f)
            {
                if (Time.time - lastStep > _timeBetweensteps)
                {
                    lastStep = Time.time;
                    //_InitialChar = _InitialChar + 1;
                    if (_addLetter > 0)
                    {
                        _addLetter--;
                    }
                    if (_initialnumber == 1)
                    {
                        FirstInitial_Text.text = _Alphabet[_addLetter] + "";
                    }
                    if (_initialnumber == 2)
                    {
                        SecondInitial_Text.text = _Alphabet[_addLetter] + "";

                    }
                    if (_initialnumber == 3)
                    {
                        ThirdInitial_Text.text = _Alphabet[_addLetter] + "";
                    }
                    //StartCoroutine(SlowdownHighScoreInput());
                }
            }

            if (player.GetButtonDown("UISubmit"))
            {
                if (Time.time - lastStep > _timeBetweensteps)
                {


                    lastStep = Time.time;
                    if (_initialnumber == 1)
                    {
                        _HighScoreInitials = FirstInitial_Text.text;
                        
                        _HighScoreInitialUnderscore.transform.localPosition = new Vector3(_HighScoreInitialUnderscore.transform.localPosition.x + 23.5f, _HighScoreInitialUnderscore.transform.localPosition.y, _HighScoreInitialUnderscore.transform.localPosition.z);
                        _addLetter = 0;
                        _initialnumber++;
                    }
                    else if (_initialnumber == 2)
                    {
                        _HighScoreInitials = _HighScoreInitials + SecondInitial_Text.text;
                        _HighScoreInitialUnderscore.transform.localPosition = new Vector3(_HighScoreInitialUnderscore.transform.localPosition.x + 23.5f, _HighScoreInitialUnderscore.transform.localPosition.y, _HighScoreInitialUnderscore.transform.localPosition.z);
                        _addLetter = 0;
                        _initialnumber++;
                    }
                    else if (_initialnumber == 3)
                    {
                        _HighScoreInitials = _HighScoreInitials + ThirdInitial_Text.text;
                        _addLetter = 0;
                        _initialnumber++;


                        _initialunderscore_Control = false;
                        _HighScoreInitialUnderscore.gameObject.SetActive(false);
                        _btn_SubmitHighScore.gameObject.SetActive(true);
                        _btn_SubmitHighScore.Select();


                    }
                    else if (_initialnumber == 4)
                    {
                        if (_Gamemanager.isCoopMode == true)
                        {

                            if (_secondplayersubmitted == true)
                            {


                                _Gamemanager.SaveHighScoresCoop(_HighScoreInitials + " + " + _HighScoreInitialsP2);
                                _Gamemanager.StopGameOverInput();
                            }

                            _secondplayersubmitted = true;
                            _btn_SubmitHighScore.gameObject.SetActive(false);
                            _initialnumber++;
                        }
                        else
                        {


                            _Gamemanager.SaveHighScores(_HighScoreInitials);
                            _btn_SubmitHighScore.gameObject.SetActive(false);
                            _Gamemanager.StopGameOverInput();
                            _initialnumber++;
                        }
                        //show and select submit button
                    }
                }
            }

            if (player.GetButtonDown("UICancel"))
            {
                if (Time.time - lastStep > _timeBetweensteps)
                {

                    lastStep = Time.time;
                    if (_initialnumber == 2)
                    {
                        _HighScoreInitials = "";
                        _HighScoreInitialUnderscore.transform.localPosition = new Vector3(_HighScoreInitialUnderscore.transform.localPosition.x - 23.5f, _HighScoreInitialUnderscore.transform.localPosition.y, _HighScoreInitialUnderscore.transform.localPosition.z);
                        _initialnumber--;
                    }
                    else if (_initialnumber == 3)
                    {
                        _HighScoreInitials = FirstInitial_Text.text;
                        _HighScoreInitialUnderscore.transform.localPosition = new Vector3(_HighScoreInitialUnderscore.transform.localPosition.x - 23.5f, _HighScoreInitialUnderscore.transform.localPosition.y, _HighScoreInitialUnderscore.transform.localPosition.z);
                        _initialnumber--;
                    }
                    else if (_initialnumber == 4)
                    {
                        _initialunderscore_Control = true;
                        _HighScoreInitialUnderscore.gameObject.SetActive(true);


                        _HighScoreInitials = FirstInitial_Text.text + SecondInitial_Text.text;
                        _initialnumber--;
                        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
                        _btn_SubmitHighScore.gameObject.SetActive(false);
                    }
                }
            }






        }
        playerID = 1;
        player = ReInput.players.GetPlayer(playerID);
        if (_activateInitials == true && _Gamemanager.isCoopMode == true && playerID == 1)
        {

            verticalInput = player.GetAxis("UIVertical");

            if (verticalInput > 0f)
            {

                if (Time.time - lastStepP2 > _timeBetweenstepsP2)
                {
                    //_InitialChar = _InitialChar + 1;

                    lastStepP2 = Time.time;
                    if (_addLetterP2 < 25)
                    {
                        _addLetterP2++;
                    }
                    if (_initialnumberP2 == 1)
                    {
                        FirstInitialP2_Text.text = _Alphabet[_addLetterP2] + "";
                    }
                    if (_initialnumberP2 == 2)
                    {
                        SecondInitialP2_Text.text = _Alphabet[_addLetterP2] + "";

                    }
                    if (_initialnumberP2 == 3)
                    {
                        ThirdInitialP2_Text.text = _Alphabet[_addLetterP2] + "";
                    }




                }

                //StartCoroutine(SlowdownHighScoreInput());

            }



            if (verticalInput < -0f)
            {
                if (Time.time - lastStepP2 > _timeBetweenstepsP2)
                {
                    lastStepP2 = Time.time;
                    //_InitialChar = _InitialChar + 1;
                    if (_addLetterP2 > 0)
                    {
                        _addLetterP2--;
                    }
                    if (_initialnumberP2 == 1)
                    {
                        FirstInitialP2_Text.text = _Alphabet[_addLetterP2] + "";
                    }
                    if (_initialnumberP2 == 2)
                    {
                        SecondInitialP2_Text.text = _Alphabet[_addLetterP2] + "";

                    }
                    if (_initialnumberP2 == 3)
                    {
                        ThirdInitialP2_Text.text = _Alphabet[_addLetterP2] + "";
                    }
                    //StartCoroutine(SlowdownHighScoreInput());
                }
            }

            if (player.GetButtonDown("UISubmit"))
            {
                if (Time.time - lastStepP2 > _timeBetweenstepsP2)
                {
                    lastStepP2 = Time.time;
                    if (_initialnumberP2 == 1)
                    {
                        _HighScoreInitialsP2 = FirstInitialP2_Text.text;
                        _HighScoreInitialUnderscoreP2.transform.localPosition = new Vector3(_HighScoreInitialUnderscoreP2.transform.localPosition.x + 23.5f, _HighScoreInitialUnderscoreP2.transform.localPosition.y, _HighScoreInitialUnderscoreP2.transform.localPosition.z);
                        _addLetterP2 = 0;
                        _initialnumberP2++;
                    }
                    else if (_initialnumberP2 == 2)
                    {
                        _HighScoreInitialsP2 = _HighScoreInitialsP2 + SecondInitialP2_Text.text;
                        _HighScoreInitialUnderscoreP2.transform.localPosition = new Vector3(_HighScoreInitialUnderscoreP2.transform.localPosition.x + 23.5f, _HighScoreInitialUnderscoreP2.transform.localPosition.y, _HighScoreInitialUnderscoreP2.transform.localPosition.z);
                        _addLetterP2 = 0;
                        _initialnumberP2++;
                    }
                    else if (_initialnumberP2 == 3)
                    {
                        _HighScoreInitialsP2 = _HighScoreInitialsP2 + ThirdInitialP2_Text.text;
                        _addLetterP2 = 0;
                        _initialnumberP2++;


                        _initialunderscoreP2_Control = false;
                        _HighScoreInitialUnderscoreP2.gameObject.SetActive(false);
                        _btn_SubmitHighScoreP2.gameObject.SetActive(true);
                        _btn_SubmitHighScoreP2.Select();


                    }
                    else if (_initialnumberP2 == 4)
                    {


                        if (_secondplayersubmitted == true)
                        {


                            _Gamemanager.SaveHighScoresCoop(_HighScoreInitials + " + " + _HighScoreInitialsP2);
                            _Gamemanager.StopGameOverInput();
                           // StopGameOverInput
                        }

                        _secondplayersubmitted = true;

                        _btn_SubmitHighScoreP2.gameObject.SetActive(false);
                        _initialnumberP2++;
                        //_Gamemanager.SaveHighScoresCoop(_HighScoreInitialsP2);
                        //show and select submit button
                    }
                }
            }
            if (player.GetButtonDown("UICancel"))
            {
                if (Time.time - lastStepP2 > _timeBetweenstepsP2)
                {
                    lastStepP2 = Time.time;
                    if (_initialnumberP2 == 2)
                    {
                        _HighScoreInitialsP2 = "";
                        _HighScoreInitialUnderscoreP2.transform.localPosition = new Vector3(_HighScoreInitialUnderscoreP2.transform.localPosition.x - 23.5f, _HighScoreInitialUnderscoreP2.transform.localPosition.y, _HighScoreInitialUnderscoreP2.transform.localPosition.z);
                        _initialnumberP2--;
                    }
                    else if (_initialnumberP2 == 3)
                    {
                        _HighScoreInitialsP2 = FirstInitial_Text.text;
                        _HighScoreInitialUnderscoreP2.transform.localPosition = new Vector3(_HighScoreInitialUnderscoreP2.transform.localPosition.x - 23.5f, _HighScoreInitialUnderscoreP2.transform.localPosition.y, _HighScoreInitialUnderscoreP2.transform.localPosition.z);
                        _initialnumberP2--;
                    }
                    else if (_initialnumberP2 == 4)
                    {
                        _initialunderscoreP2_Control = true;
                        _HighScoreInitialUnderscoreP2.gameObject.SetActive(true);


                        _HighScoreInitialsP2 = FirstInitialP2_Text.text + SecondInitialP2_Text.text;
                        _initialnumberP2--;
                        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
                        _btn_SubmitHighScoreP2.gameObject.SetActive(false);
                    }
                }
            }






        }



    }



    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();

    }


    public void UpdateScorePlayer2(int playerScore2)
    {
        _scoreTextPlayer2.text = "Score: " + playerScore2.ToString();

    }

    public void UpdateCombo(int killstreak, int highkillstreak)
    {
        _comboText.gameObject.SetActive(true);
        _comboText.text = killstreak.ToString() + " Combo!";
        _Gamemanager.SetHighKillStreakP1(highkillstreak);
    }

    public void UpdateComboPlayer2(int killstreak2, int highkillstreak2)
    {
        _comboTextPlayer2.gameObject.SetActive(true);
        _comboTextPlayer2.text = killstreak2.ToString() + " Combo!";
        _Gamemanager.SetHighKillStreakP2(highkillstreak2);
    }

    public void ClearCombo()
    {
        _comboText.gameObject.SetActive(false);

    }

    public void ClearComboPlayer2()
    {
        _comboTextPlayer2.gameObject.SetActive(false);

    }


    public void UpdateLives(int currentLives)
    {

        _LivesImg.sprite = _liveSprites[currentLives];

    }


    public void UpdateLivesPlayer2(int currentLives2)
    {

        _LivesImgPlayer2.sprite = _liveSpritesPlayer2[currentLives2];

    }


    public void UpdateGlobalP1Lives(int P1Lives)
    {

        _p1lives = P1Lives;

    }

    public void UpdateGlobalP2Lives(int P2Lives)
    {

        _p2lives = P2Lives;

    }

    public void PauseGame()
    {
        _PausePanel.SetActive(true);

        //_pauseResume_btn.SetActive(true);

    }

    public void ResumePlay()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        //_pauseResume_btn.SetActive(false);
        _PausePanel.SetActive(false);

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");

    }

    public void SetHighScoreUI()
    {


        _HighScoreText.gameObject.SetActive(true);
        _HighScoreInitialUnderscore.gameObject.SetActive(true);
        StartCoroutine(HighScoreTextChange());
        StartCoroutine(InitialUnderScoreChange());
        _Gamemanager.StartGameOverInput();
        _EnterInitialsText.gameObject.SetActive(true);

        FirstInitial_Text.gameObject.SetActive(true);
        SecondInitial_Text.gameObject.SetActive(true);
        ThirdInitial_Text.gameObject.SetActive(true);


        _activateInitials = true;



    }

    public void SetHighScoreUICoop()
    {


        _HighScoreText.gameObject.SetActive(true);
        _HighScoreInitialUnderscore.gameObject.SetActive(true);
        _HighScoreInitialUnderscoreP2.gameObject.SetActive(true);
        StartCoroutine(HighScoreTextChange());
        StartCoroutine(InitialUnderScoreChange());
        StartCoroutine(InitialUnderScoreChangeP2());

        _Gamemanager.StartGameOverInput();



        _EnterInitialsText.gameObject.SetActive(true);

        FirstInitial_Text.gameObject.SetActive(true);
        SecondInitial_Text.gameObject.SetActive(true);
        ThirdInitial_Text.gameObject.SetActive(true);


        _EnterInitialsP2Text.gameObject.SetActive(true);

        FirstInitialP2_Text.gameObject.SetActive(true);
        SecondInitialP2_Text.gameObject.SetActive(true);
        ThirdInitialP2_Text.gameObject.SetActive(true);

        _activateInitials = true;



    }

    private char MyValidate(char charToValidate)
    {
        //Checks if a dollar sign is entered....

        if ((charToValidate < 'a' || charToValidate > 'z') && (charToValidate < 'A' || charToValidate > 'Z'))
        {

            charToValidate = '\0';
        }
        else
        {
            // ... if it is change it to an empty character.
            charToValidate = Char.ToUpper(charToValidate);
        }
        return charToValidate;
    }


    public void DisplayGameOverText()
    {
        Rewired.Player _player = Rewired.ReInput.players.GetPlayer(0);
        //_gameManager.GamerOver();
        Debug.Log(_player.controllers.maps.GetFirstButtonMapWithAction("ReloadGame", skipDisabledMaps).elementIdentifierName);
        _PressRText.text = "Press '" + _player.controllers.maps.GetFirstButtonMapWithAction("ReloadGame", skipDisabledMaps).elementIdentifierName.ToString() + "' to restart the level";
        _PressRText.gameObject.SetActive(true);

        _PressEscText.text = "Press '" + _player.controllers.maps.GetFirstButtonMapWithAction("BackToMenu", skipDisabledMaps).elementIdentifierName.ToString() + "' to return to the Main Menu";
        _PressEscText.gameObject.SetActive(true);

        StartCoroutine(GameOverFlicker());
        //_GameOverText.gameObject.SetActive(true);


    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _GameOverText.gameObject.SetActive(true);
            //_gameManager.GamerOver();
            yield return new WaitForSeconds(0.5f);
            _GameOverText.gameObject.SetActive(false);
            //_gameManager.GamerOver();
            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator HighScoreTextChange()
    {
        while (true)
        {

            _HighScoreText.color = Color.red;

            yield return new WaitForSeconds(0.5f);
            _HighScoreText.color = Color.white;

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator InitialUnderScoreChange()
    {
        while (_initialunderscore_Control)
        {


            _HighScoreInitialUnderscore.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            _HighScoreInitialUnderscore.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator InitialUnderScoreChangeP2()
    {
        while (_initialunderscoreP2_Control)
        {


            _HighScoreInitialUnderscoreP2.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            _HighScoreInitialUnderscoreP2.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }


}
