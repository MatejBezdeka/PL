using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateSettings : MonoBehaviour
{
    void Awake() {
        GameObject[] children = gameObject.GetComponentsInChildren<GameObject>();
        foreach (var child in children) {
            child.SetActive(!child.activeSelf);
            child.SetActive(!child.activeSelf);
        }        
    }
}
