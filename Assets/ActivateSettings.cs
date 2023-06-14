using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateSettings : MonoBehaviour {
    [SerializeField] List<GameObject> children;
    void OnEnable() {
        foreach (GameObject child in children) {
            child.SetActive(!child.activeSelf);
            child.SetActive(!child.activeSelf);
        }
        Destroy(this);
    }
}
