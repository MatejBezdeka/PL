using System;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace Scripts.UI {
    public class OptionsButtonHandler : MonoBehaviour {
        public static OptionsButtonHandler groupButtonObserver = new OptionsButtonHandler();
        public event Action GroupButtonClicked;
        
        public void ClickedGroupButton() {
            GroupButtonClicked?.Invoke();
        }
    }
}