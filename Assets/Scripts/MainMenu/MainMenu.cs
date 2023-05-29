using System;
using RetroAesthetics;
using Scripts.UI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] GameObject StartButtons;
    [SerializeField] GameObject DifficultyButtons;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Credits;
    [SerializeField] GameObject ScoreBoard;
    [SerializeField] TextMeshProUGUI ScoreBoardText;
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioClip startSound;
    RetroCameraEffect cameraEffect;
    AudioSource audio;
    Saving memory;
    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        memory = gameObject.AddComponent<Saving>();
        audio = gameObject.GetComponent<AudioSource>();
        cameraEffect = FindObjectOfType<RetroCameraEffect>();
        cameraEffect.FadeIn();
        memory.Load();
        ScoreBoardText.text = memory.scoreBoardText();
    }

    public void PressDifficultyButton(int difficulty) {
        PlayAudio(buttonSound);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.manager.SetDifficulty(difficulty);
        SceneManager.LoadSceneAsync("Loading");
        SceneManager.LoadSceneAsync("MainScene");
    }
    public void PressExitButton() {
        Application.Quit();
        Debug.Log("you quit");
    }
    public void PressStartButton() {
        ChangeVisibility(StartButtons, DifficultyButtons);
    }
    public void PressSettingsButton() {
        ChangeVisibility(StartButtons, Settings);
    }
    public void PressCredit() {
        ChangeVisibility(StartButtons, Credits);
    }
    void ChangeVisibility(GameObject obj1, GameObject obj2) {
        PlayAudio(buttonSound);
        obj1.SetActive(!obj1.activeSelf);
        obj2.SetActive(!obj2.activeSelf);
    }
    
    void PlayAudio(AudioClip clip) {
        cameraEffect.Glitch();
        audio.clip = clip;
        audio.Play();
    }
}