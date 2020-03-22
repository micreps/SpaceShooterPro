using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessFunctions : MonoBehaviour
{
    public PostProcessVolume volume;
    private void Start()
    {
        
    }
    private ColorGrading colorGradingLayer;

    public void StartEmergency()
    {
        //volume = gameObject.GetComponent<PostProcessVolume>();
        //Debug.Log("PostProcess");
        volume.profile.TryGetSettings(out colorGradingLayer);
        colorGradingLayer.temperature.value = 100;
        colorGradingLayer.tint.value = 100;


        //colorGradingLayer.temperature.value = -28;
        //colorGradingLayer.tint.value = -17;
    }
    public void EndEmergency()
    {
        //volume = gameObject.GetComponent<PostProcessVolume>();
        //Debug.Log("PostProcess");
        volume.profile.TryGetSettings(out colorGradingLayer);
        colorGradingLayer.temperature.value = -28;
        colorGradingLayer.tint.value = -17;


        //colorGradingLayer.temperature.value = -28;
        //colorGradingLayer.tint.value = -17;
    }


}
