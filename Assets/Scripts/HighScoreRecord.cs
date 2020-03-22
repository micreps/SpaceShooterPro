using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HighScoreRecord : MonoBehaviour
{


    public Transform[] obj;

    [SerializeField]
    int _recordnumber = 0;

    



    [SerializeField]
    private int[] _HighScores;
    [SerializeField]
    private string[] _HighScoreNames;
    [SerializeField]
    private int[] _HighStreakValues;
    [SerializeField]
    private string _Gametypestring;



    



    void Awake()
    {
       

        obj = GetComponentsInChildren<Transform>(true);

        foreach (Transform t in obj)
        {
            Debug.Log(t.name);
        }
        //obj[1].GetComponent<Text>();

        _Gametypestring = "StorySingle";
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

    }

    

    public void SetGameTypeString(string GameType)
    {
        _Gametypestring = GameType;


    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Assign_Record_Number(int recordnumber, string GameType)
    {
        _recordnumber = recordnumber;
        _Gametypestring = GameType;

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
        HighScoreText[] HighScoreTexts = this.gameObject.GetComponentsInChildren<HighScoreText>();

        for (int x = 0; x < HighScoreTexts.Length; x++)
        {
            HighScoreTexts[x].AssignRecordNumberToText(recordnumber, _HighScores[recordnumber-1], _HighStreakValues[recordnumber-1], _HighScoreNames[recordnumber-1]);



        }

    }


}
