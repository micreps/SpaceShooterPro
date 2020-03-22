using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionScript : MonoBehaviour
{
    private int _difficulty;

    private Button _Easy_Button;
    private Button _Normal_Button;
    private Button _Hard_Button;
    private Button _Save_Button;
    [SerializeField]
    private float _musicvolume;
    [SerializeField]
    private float _SFXVolume;

    private Slider _MusicVolSlider;
    private Slider _SFXVolSlider;

    private void Start()
    {
        _difficulty = PlayerPrefs.GetInt("Difficulty", 2);
        _musicvolume = PlayerPrefs.GetFloat("MusicVolume",1);
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        _Easy_Button = GameObject.Find("Easy_button").GetComponent<Button>();
        _Normal_Button = GameObject.Find("Normal_button").GetComponent<Button>();
        _Hard_Button = GameObject.Find("Hard_button").GetComponent<Button>();
        _Save_Button = GameObject.Find("Save_button").GetComponent<Button>();
        _MusicVolSlider = GameObject.Find("Music_Volume_Slider").GetComponent<Slider>();
        _SFXVolSlider = GameObject.Find("SFX_Slider").GetComponent<Slider>();


        if (_difficulty == 1)
        {
            _Easy_Button.Select();


        }
        else if (_difficulty == 2)
        {
            _Normal_Button.Select();


        }
        else if (_difficulty == 3)
        {
            _Hard_Button.Select();


        }
        _MusicVolSlider.value = _musicvolume;
        _SFXVolSlider.value = _SFXVolume;


    }



    public void SetMusicVolume(float vol)
    {

        _musicvolume = vol;

    }

    public void SetSFXVolume(float vol)
    {

        _SFXVolume = vol;

    }

    public void Difficulty_Easy_Button()
    {
        _difficulty = 1;
        _Easy_Button.interactable = false;
        _Normal_Button.interactable = true;
        _Hard_Button.interactable = true;
        _Save_Button.Select();
        





    }
    public void Difficulty_Normal_Button()
    {

        _difficulty = 2;
        _Easy_Button.interactable = true;
        _Normal_Button.interactable = false;
        _Hard_Button.interactable = true;
        _Save_Button.Select();
    }
    public void Difficulty_Hard_Button()
    {
        _difficulty = 3;
        _Easy_Button.interactable = true;
        _Normal_Button.interactable = true;
        _Hard_Button.interactable = false;
        _Save_Button.Select();

    }
    public void Options_Save_Button()
    {

        PlayerPrefs.SetInt("Difficulty", _difficulty);
        PlayerPrefs.SetFloat("MusicVolume", _musicvolume);
        PlayerPrefs.SetFloat("SFXVolume", _SFXVolume);

    }
    public void Options_Back_Button()
    {

        SceneManager.LoadScene("Main_Menu");


    }
}
