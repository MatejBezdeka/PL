using System;
using System.Globalization;
using Scripts.Enviroment;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Scripts.UI {
    public class OptionsSlider : OptionsObject {
        private int value;
        [SerializeField] Slider slider;
        [SerializeField] TMP_InputField inputField;

        protected override void Load() {
            value = PlayerPrefs.GetInt(typeOfOption.ToString());
            slider.value = value;
            SliderChanged();
        }

        protected override void Save() {
            PlayerPrefs.SetInt(typeOfOption.ToString(), value);
        }

        protected override void Apply() {
            switch (typeOfOption) {
                case type.maxFPS:
                    //??? funguje ???//
                    if (slider.value == slider.maxValue) {
                        Application.targetFrameRate = -1;
                    }
                    else {
                        Application.targetFrameRate = value;
                    } 
                    break;
                case type.masterVolume:
                    AudioManager.audManager.SetMixerVolume("Volume", value);
                    break;
                case type.musicVolume:
                    AudioManager.audManager.SetMixerVolume("Music", value);
                    break;
                case type.effectsVolume:
                    AudioManager.audManager.SetMixerVolume("Effects", value);
                    break;
            }
        }
        // Mixer.SetFloat("Volume", Mathf.Log10(value/100) * 20);
        public void SliderChanged() {
            switch (typeOfOption) {
                case type.maxFPS:
                    value = (int)slider.value * 10;
                    if (slider.value == slider.maxValue) {
                        inputField.text = "unlimited";
                    }
                    else {
                        inputField.text = value.ToString();
                    }
                    break;
                
                case type.fov:
                    value = (int) slider.value;
                    inputField.text = value.ToString();
                    break;
                case type.masterVolume:
                case type.musicVolume:
                case type.effectsVolume:
                case type.sensitivity:
                    value = (int) slider.value;
                    inputField.text = value + "%";
                break;
            }
        }

        public void TextChanged() {
            switch (typeOfOption) {
                case type.maxFPS:
                    
                    value = (int.Parse(inputField.text) / 10) * 10;
                    if (value/10 < slider.minValue) {
                        value = (int) slider.minValue * 10;
                    }else if (value/10 > slider.maxValue) {
                        value = (int) slider.maxValue * 10;
                    }
                    slider.value = value / 10;
                    inputField.text = value.ToString(); 
                    break;
                
                default:
                    value = Int32.Parse(inputField.text);
                    if (value < slider.minValue) {
                        value = (int) slider.minValue;
                    }else if (value > slider.maxValue) {
                        value = (int) slider.maxValue;
                    }
                    break;
                
            }
        }
    }
}