using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class LoadingText : MonoBehaviour
{
    Text loadingText; 
    int length;
    float cooldown = 0.75f;
    float currentCooldown = 0;
    void Start() {
        loadingText = gameObject.GetComponent<Text>();
        length = loadingText.text.Length;
    }

    void Update() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= cooldown) {
            if (loadingText.text.Length == length) {
                loadingText.text = "PLEASE STAND BY";
            }
            else if (loadingText.text.Length == length-1) {
                loadingText.text = "PLEASE STAND BY..";
            }
            else {
                loadingText.text = "PLEASE STAND BY.";
            }
            currentCooldown = 0;
        }
        //Debug.Log(length);
    }
}
