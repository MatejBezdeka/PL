using System;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace Scripts.UI {
    public class OptionsButtonHandler : MonoBehaviour {
        public static OptionsButtonHandler groupButtonObserver = new OptionsButtonHandler();
        public event Action GroupButtonCliked;
        
        public void ClikedGroupButton() {
            GroupButtonCliked?.Invoke();
        }
    }
}