using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class OptionsSelecter : MonoBehaviour {
    private enum type {
        resolution,
        quality
    }

    [SerializeField] private List<string> choices;
    [SerializeField] private type typeOfOption;
    private int currentIndex = 0;
    private TextMeshProUGUI text;
    [SerializeField] private Button buttonRight;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private bool cycleable;
    [SerializeField] private bool generatedOptions;

    private void Awake() {
        Settings.applySettings += Save;
        text = GetComponentInChildren<TextMeshProUGUI>();
        buttonRight.onClick.AddListener(ClickNext);
        buttonLeft.onClick.AddListener(ClickPrev);
        if (choices.Count == 0)
            switch (typeOfOption) {
                case type.resolution:
                    GetResolutions();
                    break;
            }
    }

    private void OnEnable() {
        Load();
    }

    private void ClickPrev() {
        currentIndex--;
        UpdateUI();
    }

    private void ClickNext() {
        currentIndex++;
        UpdateUI();
    }

    private void UpdateUI() {
        if (currentIndex < 0) currentIndex = choices.Count;
        if (currentIndex > choices.Count - 1) currentIndex = 0;
        buttonLeft.interactable = true;
        buttonRight.interactable = true;

        if (!cycleable && currentIndex == choices.Count - 1)
            buttonRight.interactable = false;
        if (!cycleable && currentIndex == 0)
            buttonLeft.interactable = false;
        text.text = choices[currentIndex];
    }

    private void GetResolutions() {
        foreach (var resolution in Screen.resolutions) choices.Add(resolution.width + "x" + resolution.height);
    }

    private void Save() {
        switch (typeOfOption) {
            case type.resolution:
                var dimensions = choices[currentIndex].Split("x");
                PlayerPrefs.SetInt(typeOfOption + "W", int.Parse(dimensions[0]));
                PlayerPrefs.SetInt(typeOfOption + "H", int.Parse(dimensions[1]));
                break;
            default:
                //quality
                PlayerPrefs.SetInt(typeOfOption.ToString(), currentIndex);
                break;
        }
        Apply();
    }

    void Apply() {
        switch (typeOfOption) {
            case type.resolution:
                var dimensions = choices[currentIndex].Split("x");
                Screen.SetResolution(int.Parse(dimensions[0]), int.Parse(dimensions[0]), Screen.fullScreen);
                break;
            case type.quality:
                QualitySettings.SetQualityLevel(currentIndex, true);
                break;
        }
        UpdateUI();
    }

    void Load() {
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