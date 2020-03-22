using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Button btn_SingleStory;
    [SerializeField]
    private GameObject _entryContainerPrefab;
    [SerializeField]
    private GameObject _entryTemplate;
    [SerializeField]
    private GameObject _HighScorePanel;

    [SerializeField]
    private GameObject _entryContainer;

    [SerializeField]
    private GameObject[] _ScoreTexts;

    private Transform[] _textobj;

    [SerializeField]
    private string _Gametypestring;

    private void Awake()
    {
        btn_SingleStory = GameObject.Find("Single_Player_Button").GetComponent<Button>();
        btn_SingleStory.Select();
        //Reset_Scores();
        SetGameTypeSPStory();
       //StartCoroutine(FirstScoreDisplay());


        /*
         Vector3 posToSpawn = new Vector3(Random.Range(-12f, 12f), 7, -1f);
            GameObject newEnemy = Instantiate(_EnemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
         */
    }

    IEnumerator FirstScoreDisplay()
    {

        yield return new WaitForSeconds(1f);
        Reset_Scores();

    }


    public void Reset_Scores()
    {

        
        Vector3 posToSpawnContainer = new Vector3(0f, 0, 0f);
        GameObject _entryContainer = Instantiate(_entryContainerPrefab, posToSpawnContainer, Quaternion.identity);


        // _entryContainer = GameObject.Find("HighScoreEntryTemplate(Clone)");
        _entryContainer.transform.SetParent(_HighScorePanel.transform);


        _entryContainer.transform.localPosition = posToSpawnContainer;
        //_entryContainer.transform.parent = _HighScorePanel.transform;
        //_entryTemplate = GameObject.Find("HighScoreEntryTemplate");

        //_entryTemplate.gameObject.SetActive(false);

        float _templateheight = 20f;

        for (int i = 0; i < 10; i++)
        {


            Vector3 posToSpawn = new Vector3(130, 110f - _templateheight * i, 0f);
            GameObject entryTransform = Instantiate(_entryTemplate, posToSpawn, Quaternion.identity);
            entryTransform.transform.SetParent(_entryContainer.transform);
            entryTransform.transform.localPosition = posToSpawn;
            //entryTransform.transform.parent = _entryContainer.transform;
            // RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            // entryRectTransform.anchoredPosition = new Vector2(0, -_templateheight * i);
            entryTransform.gameObject.SetActive(true);



            //GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0f, -1.35f), Quaternion.identity);
            HighScoreRecord[] HighScoreRecords = entryTransform.GetComponentsInChildren<HighScoreRecord>();

            for (int x = 0; x < HighScoreRecords.Length; x++)
            {
                HighScoreRecords[x].Assign_Record_Number(i + 1, _Gametypestring);



            }

        }



    }

    public void SetGameTypeSPStory()
    {
        _Gametypestring = "StorySingle";
        //GameObject.Destroy(entryTransform);
        //for (int i = 0; i < 10; i++)
       // { 
            _entryContainer = GameObject.Find("HighScoreEntryContainer(Clone)");
            GameObject.Destroy(_entryContainer);
            Reset_Scores();
        //}
    }
    public void SetGameTypeCOOPStory()
    {
        _Gametypestring = "StoryCoop";
        _entryContainer = GameObject.Find("HighScoreEntryContainer(Clone)");
        GameObject.Destroy(_entryContainer);
        Reset_Scores();


    }
    public void SetGameTypeSPSurvive()
    {
        _Gametypestring = "SurviveSingle";
        _entryContainer = GameObject.Find("HighScoreEntryContainer(Clone)");
        GameObject.Destroy(_entryContainer);
        Reset_Scores();


    }
    public void SetGameTypeCOOPSurvive()
    {
        _Gametypestring = "SurviveCoop";
        _entryContainer = GameObject.Find("HighScoreEntryContainer(Clone)");
        GameObject.Destroy(_entryContainer);
        Reset_Scores();


    }

}
