using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioVolume : MonoBehaviour
{
    // Start is called before the first frame update
    private float _musicVolume;
    private float _SFXVolume;
    [SerializeField]
    private bool _ismusic = false;

    private AudioSource AudioVolumeControl;


    void Start()
    {
        _musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        AudioVolumeControl = GetComponent<AudioSource>();

        if (_ismusic == false)
        {
            AudioVolumeControl.volume = _SFXVolume;
        }
        else
        {
            AudioVolumeControl.volume = _musicVolume;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
