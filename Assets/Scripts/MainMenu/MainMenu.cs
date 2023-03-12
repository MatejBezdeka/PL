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
    Saving memory;
    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        memory = gameObject.AddComponent<Saving>();
        memory.Load();
        ScoreBoardText.text = memory.scoreBoardText();
    }

    public void PressDifficultyButton(int difficulty) {
        Debug.Log(difficulty);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.manager.SetDifficulty(difficulty);
        SceneManager.LoadScene("MainScene");
    }
    
    public void PressStartButton() {
        Debug.Log("yyyy");
        ChangeVisibility(StartButtons, DifficultyButtons);
    }
    
    public void PressSettingsButton() {
        //ChangeVisibility(StartButtons, Settings);
    }

    public void PressExitButton() {
        Application.Quit();
        Debug.Log("you quit");
    }
    public void PressDifficultyBackButton() {
        ChangeVisibility(StartButtons, DifficultyButtons);
    }
    public void PressSettingsBackButton() {
        ChangeVisibility(StartButtons, Settings);
    }

    void ChangeVisibility(GameObject obj1, GameObject obj2) {
        obj1.SetActive(!obj1.activeSelf);
        obj2.SetActive(!obj2.activeSelf);
    }
}