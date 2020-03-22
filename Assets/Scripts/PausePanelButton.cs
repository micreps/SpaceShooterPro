using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelButton : MonoBehaviour
{

    private Button btn_ResumeGame;
    // Start is called before the first frame update
    void Start()
    {
        btn_ResumeGame = GetComponent<Button>();
        btn_ResumeGame.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
