using System.Collections;
using System.Collections.Generic;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonGroup : MonoBehaviour {
    private Button button;
    [SerializeField] GameObject body;
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        OptionsButtonHandler.groupButtonObserver.GroupButtonClicked += EnableButton;
    }

    void Click() {
        OptionsButtonHandler.groupButtonObserver.ClikedGroupButton();
        button.interactable = false;
        body.SetActive(true);
    }

    void EnableButton() {
        button.interactable = true;
        body.SetActive(false);
        
    }
}
