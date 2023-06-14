using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Scripts.Enviroment {
    public class AudioManager : MonoBehaviour {
        public static AudioManager audManager;
        public AudioMixer mixer;

        void Awake() {
            audManager = this;
        }

        public void SetMixerVolume(string group, float value) {
            if (value == 0) {
                value += 0.0001f;
            }
            mixer.SetFloat(group, Mathf.Log10(value / 120f) * 20);
        }
    }
}