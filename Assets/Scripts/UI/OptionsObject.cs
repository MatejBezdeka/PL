using System;
using UnityEngine;

namespace Scripts.UI {
    public abstract class OptionsObject : MonoBehaviour {
        [SerializeField] protected type typeOfOption;
        
        public enum type {
            resolution,
            quality, 
            vsync,
            displayMode,
            display, 
            filters,
            maxFPS,
            fov,
            masterVolume,
            musicVolume,
            effectsVolume,
            sensitivity
        }

        protected virtual void Awake() {
            switch (typeOfOption) {
                case type.filters:
                    break;
            }
            Settings.applySettings += Save;
            Settings.applySettings += Apply;
            Settings.loadSettings += Load;
            Load();
            Apply();
        }
        protected abstract void Load();
        protected abstract void Save();
        protected abstract void Apply();

        protected virtual void OnDestroy() {
            Settings.applySettings -= Save;
            Settings.applySettings -= Apply;
            Settings.loadSettings -= Load;
        }
    }
}