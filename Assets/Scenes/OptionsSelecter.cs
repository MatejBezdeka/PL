using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSelecter : MonoBehaviour {
    enum type {
        resolution,
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
        text = GetComponentInChildren<TextMeshProUGUI>();
        buttonRight.onClick.AddListener(ClickNext);
        buttonLeft.onClick.AddListener(ClickPrev);
    }
    void ClickPrev() {
        currentIndex--;
        if (currentIndex < 0) {
            currentIndex = choices.Count;
        }
        text.text = choices[currentIndex];
        if (!cycleable && currentIndex == 0) {
            buttonLeft.enabled = false;
        }
        else {
            buttonLeft.enabled = true;
        }
    }

    void ClickNext() {
        currentIndex++;
        if (currentIndex > choices.Count) {
            currentIndex = 0;
        }
        text.text = choices[currentIndex];
        if (!cycleable && currentIndex == choices.Count) {
            buttonLeft.enabled = false;
        }
        else {
            buttonLeft.enabled = true;
        }
    }
}
