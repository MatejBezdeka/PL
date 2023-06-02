using System;
using UnityEngine;

namespace Scripts.UI {
    public class Settings : MonoBehaviour {
        public static Action applySettings;
        public static Action loadSettings;

        void OnEnable() {
            loadSettings?.Invoke();
            Debug.Log("invoke");
        }
        public void ApplyChanges() {
            applySettings.Invoke();
        }
    }
}
