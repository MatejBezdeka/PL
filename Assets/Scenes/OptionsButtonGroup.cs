using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButtonGroup : MonoBehaviour {
    private Button button;
    [SerializeField] GameObject body;
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    void Click() {
        button.interactable = false;
        body.SetActive(true);
    }

    void EnableButton() {
        button.interactable = true;
        body.SetActive(false);
    }
}
