using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    [SerializeField] Button resumeButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button exitButton;
    public void Start() {
        resumeButton.onClick.AddListener(Resume);
        optionButton.onClick.AddListener(Option);
        exitButton.onClick.AddListener(Exit);
    }

    void Resume() {
        GameManager.manager.Pause();
    }

    void Option() {
        //TODO:
    }

    void Exit() {
        GameManager.manager.Pause();
        SceneManager.LoadScene("MainMenu");
    }
}