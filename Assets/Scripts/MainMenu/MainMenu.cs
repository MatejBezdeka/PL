using RetroAesthetics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] GameObject StartButtons;
    [SerializeField] GameObject DifficultyButtons;
    [SerializeField] GameObject Settings;
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
        audio = gameObject.AddComponent<AudioSource>();
        cameraEffect = FindObjectOfType<RetroCameraEffect>();
        cameraEffect.FadeIn();
        memory.Load();
        ScoreBoardText.text = memory.scoreBoardText();
    }

    public void PressDifficultyButton(int difficulty) {
        Debug.Log(difficulty);
        PlayAudio(buttonSound);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.manager.SetDifficulty(difficulty);
        cameraEffect.FadeOut(0.5f);
        SceneManager.LoadSceneAsync("Loading");
        
        SceneManager.LoadSceneAsync("MainScene");
        }
    
    public void PressStartButton() {
        PlayAudio(buttonSound);
        ChangeVisibility(StartButtons, DifficultyButtons);
    }
    
    public void PressSettingsButton() {
        PlayAudio(buttonSound);
        ChangeVisibility(StartButtons, Settings);
    }

    public void PressExitButton() {
        Application.Quit();
        Debug.Log("you quit");
    }
    public void PressDifficultyBackButton() {
        PlayAudio(buttonSound);
        ChangeVisibility(StartButtons, DifficultyButtons);
    }
    public void PressSettingsBackButton() {
        PlayAudio(buttonSound);
        ChangeVisibility(StartButtons, Settings);
    }

    void ChangeVisibility(GameObject obj1, GameObject obj2) {
        obj1.SetActive(!obj1.activeSelf);
        obj2.SetActive(!obj2.activeSelf);
    }

    void PlayAudio(AudioClip clip) {
        audio.clip = clip;
        audio.Play();
    }
}