using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{
    [SerializeField]
    private int _recordnumber;
    [SerializeField]
    private int _TextCategory;

    private Text _HighScoreText;

public void AssignRecordNumberToText(int recordnumber, int Scorevalue, int streakvalue, string Initials)
    {

        _recordnumber = recordnumber;
        _HighScoreText = this.GetComponent<Text>();
        

        int rank = recordnumber;
        
        if (_TextCategory == 1)
        {
            string _rankstring;
            switch (rank)
            {
                default: _rankstring = rank + "TH"; break;
                case 1: _rankstring = "1ST"; break;
                case 2: _rankstring = "2ND"; break;
                case 3: _rankstring = "3RD"; break;


            }
            _HighScoreText.text = _rankstring;

        }
        else if (_TextCategory == 2)
        {
            _HighScoreText.text = Scorevalue.ToString();


        }
        else if (_TextCategory == 3)
        {

            _HighScoreText.text = Initials;
        }
        else if (_TextCategory == 4)
        {

            _HighScoreText.text = streakvalue.ToString();
        }

    }
}
