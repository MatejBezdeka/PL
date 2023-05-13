using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public static Action applySettings;
    [SerializeField] Slider FPSslider;
    [SerializeField] TMP_InputField FPSinputField;
    int FPSTarget;
    Resolution[] resolutions;
    List<Resolution> list;
    void Start()
    {
        //load
        
        //get resolutions
        resolutions = Screen.resolutions;
        list = new List<Resolution>();
        foreach (Resolution res in resolutions) {
            list.Add(res);
        }
    }

    public void FPSsliderChanged() {
        FPSTarget = (int)FPSslider.value * 10;
        FPSinputField.text = FPSTarget.ToString();
    }

    public void FPStextChanged() {
        FPSTarget = (int)(int.Parse(FPSinputField.text)/10) *10;
        if (FPSTarget/10 < FPSslider.minValue) {
            FPSTarget = (int) FPSslider.minValue * 10;
        }else if (FPSTarget/10 > FPSslider.maxValue) {
            FPSTarget = (int)FPSslider.maxValue * 10;
        }
        FPSslider.value = FPSTarget / 10;
        FPSinputField.text = FPSTarget.ToString();
    }

    public void ApplyChanges() {
        applySettings.Invoke();
    }
}
