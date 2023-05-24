using System;
using UnityEngine;

namespace Scripts.UI {
    public abstract class OptionsObject : MonoBehaviour {
        [SerializeField] protected type typeOfOption;
        
        protected enum type {
            resolution,
            quality,
            vsync,
            displayMode,
            display,
            maxFPS,
        }

        protected virtual void Awake() {
            Settings.applySettings += Save;
            Settings.loadSettings += Load;
            Load();
        }
        protected abstract void Load();
        protected abstract void Save();
        protected abstract void Apply();
    }
}