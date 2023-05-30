using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public static Action applySettings;
    public static Action loadSettings;

    void OnEnable() {
        loadSettings?.Invoke();
        Debug.Log("invoke");
    }
    public void ApplyChanges() {
        applySettings.Invoke();
    }
}
