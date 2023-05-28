using System;
using System.Collections.Generic;
using Scripts.UI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSelecter : OptionsObject {
    // resoluce se hádá s displaymodem
    // windowed zmizí rámeček
    static bool mod;
    

    [SerializeField] private List<string> choices;
    private int currentIndex = 0;
    private TextMeshProUGUI text;
    [SerializeField] private Button buttonRight;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private bool cycleable;

    [SerializeField] TextMeshProUGUI down;

    protected override void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        buttonRight.onClick.AddListener(ClickNext);
        buttonLeft.onClick.AddListener(ClickPrev);
        if (choices.Count == 0)
            switch (typeOfOption) {
                case type.resolution:
                    GetResolutions();
                    break;
                case type.display:
                    GetDisplays();
                    break;
            }
        base.Awake();
    }

    

    void ClickPrev() {
        currentIndex--;
        UpdateUI();
    }

    void ClickNext() {
        currentIndex++;
        UpdateUI();
    }

    void UpdateUI() {
        if (currentIndex < 0) currentIndex = choices.Count-1;
        if (currentIndex > choices.Count - 1) currentIndex = 0;
        buttonLeft.interactable = true;
        buttonRight.interactable = true;

        if (!cycleable && currentIndex == choices.Count - 1 || choices.Count <= 1)
            buttonRight.interactable = false;
        if (!cycleable && currentIndex == 0 || choices.Count <= 1)
            buttonLeft.interactable = false;
        text.text = choices[currentIndex];
    }

    #region Getters

    void GetDisplays() {
        Debug.Log(Display.displays.Length);
        int j = 0;
        foreach (var display in Display.displays) {
            choices.Add("Display" + j);
            j++;
        }
    }
    
    void GetResolutions() {
        int hz = Screen.resolutions[0].refreshRate;
        foreach (var resolution in Screen.resolutions) {
            if (resolution.refreshRate != hz) continue;
            choices.Add(resolution.width + "x" + resolution.height);
        }
    }
    #endregion
    protected override void Save() {
         switch (typeOfOption) {
            case type.resolution:
                var dimensions = choices[currentIndex].Split("x");
                PlayerPrefs.SetInt(typeOfOption + "W", int.Parse(dimensions[0]));
                PlayerPrefs.SetInt(typeOfOption + "H", int.Parse(dimensions[1]));
                break;
            default:
                //quality, Vsync, displayMode
                PlayerPrefs.SetInt(typeOfOption.ToString(), currentIndex);
                break;
        }
        Apply();
    }

    protected override void Apply() {
        switch (typeOfOption) {
            case type.resolution:
                var dimensions = choices[currentIndex].Split("x");
                Screen.SetResolution(int.Parse(dimensions[0]), int.Parse(dimensions[0]), mod);
                break;
            case type.quality:
                QualitySettings.SetQualityLevel(currentIndex, true);
                break;
            case type.vsync:
                QualitySettings.vSyncCount = currentIndex;
                break;
            case type.displayMode:
                switch (currentIndex) {
                    case 0:
                        mod = true;
                        break;
                    case 1:
                        mod = false;
                        break;
                    default:
                        mod = true;
                        break;
                }
                Screen.SetResolution(Screen.width, Screen.height, mod);
                break;
            case type.display:
                //Display.displays[currentIndex].Activate();
                //mainCamera.targetDisplay = currentIndex;
                //canvas.targetDisplay = currentIndex;
                break;
        }

        UpdateUI();
    }

    protected override void ApplyAtStart() {
        //filters
    }

    protected override void Load() {
        try {
            switch (typeOfOption) {
                case type.resolution:
                    var height = PlayerPrefs.GetInt(typeOfOption + "W");
                    var width = PlayerPrefs.GetInt(typeOfOption + "H");
                    bool found = false;
                    for (var index = 0; index <= choices.Count - 1; index++) {
                        var dimensions = choices[index].Split("x");
                        if (int.Parse(dimensions[0]) == height && int.Parse(dimensions[1]) == width) {
                            currentIndex = index;
                            found = true;
                        }
                        if (found) break;
                    }
                    break;
                default:
                    currentIndex = PlayerPrefs.GetInt(typeOfOption.ToString());
                    break;
            }
        }
        catch (Exception e) {
            currentIndex = 0;
        }
        UpdateUI();
    }
}