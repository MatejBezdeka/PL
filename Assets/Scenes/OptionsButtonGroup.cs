using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonGroup : MonoBehaviour {
    Button button;
    [SerializeField] GameObject body;
    

    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        OptionsButtonHandler.groupButtonObserver.GroupButtonClicked += EnableButton;
    }

    void Click() {
        OptionsButtonHandler.groupButtonObserver.ClickedGroupButton();
        button.interactable = false;
        body.SetActive(true);
    }

    void EnableButton() {
        button.interactable = true;
        body.SetActive(false);
    }
}
