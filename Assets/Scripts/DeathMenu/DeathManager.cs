using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class DeathManager : MonoBehaviour {
    [SerializeField] TextMeshProUGUI scoreBoard;
    [SerializeField] TMP_InputField nameInput; 
    [SerializeField] TextMeshProUGUI newScore;
    [SerializeField] AudioClip buttonSound;
    [SerializeField] AudioClip errorSound;
    [SerializeField] AudioClip saveSuccesful;
    AudioSource audio;
    Saving memory;
    public static int score;
    void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        memory = gameObject.AddComponent<Saving>();
        memory.Load();
        audio = gameObject.AddComponent<AudioSource>();
        scoreBoard.text = memory.scoreBoardText();
        newScore.text = "Score: " + score;
    }

    public void Save(Button button) {
        if (nameInput.text is "" or " " or "  " or "   ") {
            PlayAudio(errorSound);
            Debug.Log("no name input");
            return;
        }
        PlayAudio(saveSuccesful);
        button.interactable = false;
        nameInput.interactable = false;
        memory.Save(score, nameInput.text);
        nameInput.text = "Saved";
        scoreBoard.text = memory.scoreBoardText();
    }
    
    public void Exit() {
        Application.Quit();
    }
    public void MainMenu() {
        PlayAudio(buttonSound);
        SceneManager.LoadScene("MainMenu");
    }
    void PlayAudio(AudioClip clip) {
        audio.clip = clip;
        audio.Play();
    }
}
