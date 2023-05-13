using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class OptionsSelecter : MonoBehaviour {
    enum type {
        resolution,
        quality
    }
    [SerializeField] List<string> choices;
    [SerializeField] type typeOfOption;
    int currentIndex = 0;
    TextMeshProUGUI text;
    [SerializeField] Button buttonRight;
    [SerializeField] Button buttonLeft;
    [SerializeField] bool cycleable;
    [SerializeField] bool generatedOptions;
    void Start() {
        Settings.applySettings += Save;
        text = GetComponentInChildren<TextMeshProUGUI>();
        buttonRight.onClick.AddListener(ClickNext);
        buttonLeft.onClick.AddListener(ClickPrev);
        if (choices.Count == 0) {
            switch (typeOfOption) {
                case type.resolution:
                    GetResolutions();
                    break;
            }    
        }
        text.text = choices[currentIndex];
    }
    void ClickPrev() {
        currentIndex--;
        if (currentIndex < 0) {
            currentIndex = choices.Count;
        }
        text.text = choices[currentIndex];
        if (!cycleable && currentIndex == 0) {
            buttonLeft.interactable = false;
        }
        buttonRight.interactable = true;
    }

    void ClickNext() {
        currentIndex++;
        if (currentIndex > choices.Count-1) {
            currentIndex = 0;
        }
        text.text = choices[currentIndex];
        if (!cycleable && currentIndex == choices.Count-1) {
            buttonRight.interactable = false;
        }
        buttonLeft.interactable = true;
    }

    void GetResolutions() {
        foreach (Resolution resolution in Screen.resolutions) {
            choices.Add(resolution.width + "x" + resolution.height);
        }
    }

    void Save() {
        switch (typeOfOption) {
            case type.resolution:
                string[] dimensions = choices[currentIndex].Split("x");
                //PlayerPrefs.SetInt(typeOfOption.ToString()+"W", int.Parse(dimension[0]));
                //PlayerPrefs.SetInt(typeOfOption.ToString() + "H", int.Parse(dimension[1]));
                Debug.Log(dimensions[0] + "x" + dimensions[1]);
                Screen.SetResolution(int.Parse(dimensions[0]), int.Parse(dimensions[0]), false);
                break;
            default:
                //PlayerPrefs.SetInt(typeOfOption.ToString(), currentIndex);
                break;
        }
    }
}
