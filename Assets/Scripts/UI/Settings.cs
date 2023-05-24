using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public static Action applySettings;
    public static Action loadSettings;
    
    int FPSTarget;
    Resolution[] resolutions;
    List<Resolution> list;
    void Start()
    {
        loadSettings?.Invoke();
        resolutions = Screen.resolutions;
        list = new List<Resolution>();
        foreach (Resolution res in resolutions) {
            list.Add(res);
        }
    }

    private void OnEnable() {
        loadSettings?.Invoke();
    }

    public void ApplyChanges() {
        applySettings.Invoke();
    }
}
