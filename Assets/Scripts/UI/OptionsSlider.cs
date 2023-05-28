using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI {
    public class OptionsSlider : OptionsObject {
        private int value;
        [SerializeField] Slider slider;
        [SerializeField] TMP_InputField inputField;

        protected override void Load() {
            PlayerPrefs.GetInt(typeOfOption.ToString(), value);
            SliderChanged();
            TextChanged();
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
            }
        }

        protected override void ApplyAtStart() {
            //sensitivity, fov jinde
        }

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
                case type.sensitivity:
                    value = (int) slider.value;
                    inputField.text = value + "%";
                    break;
                case type.fov:
                    value = (int) slider.value;
                    inputField.text = value.ToString();
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
                case type.sensitivity:
                case type.fov:
                    value = (int.Parse(inputField.text));
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