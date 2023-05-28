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
            fpsCounter,
            fov,
            masterVolume,
            musicVolume,
            effectsVolume,
            sensitivity
        }
/*
 * UP,DOWN,LEFT,RIGHT
 * JUMP, DASH
 * FIRE
 * WEAPON 1,2,3,4
 * RELOAD
 * PAUSE?
 * 
 * ...
 */
        protected virtual void Awake() {
            switch (typeOfOption) {
                case type.filters:
                    break;
            }
            Settings.applySettings += Save;
            Settings.loadSettings += Load;
            Load();
            Apply();
        }
        protected abstract void Load();
        protected abstract void Save();
        protected abstract void Apply();
        protected abstract void ApplyAtStart();
    }
}