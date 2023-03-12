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
    public static DeathManager manager = new DeathManager();
    Saving memory;
    int score;
    void Start() {
        manager = GetComponent<DeathManager>();
        memory = gameObject.AddComponent<Saving>();
        memory.Load();
        scoreBoard.text = memory.scoreBoardText();
        newScore.text = "Score: " + score;
    }

    public void Save(Button button) {
        if (nameInput.text is "" or " " or "  " or "   ") {
            Debug.Log("no name input");
            return;
        }
        button.interactable = false;
        nameInput.text = "Saved";
        nameInput.interactable = false;
        memory.Save(score, nameInput.text);
        scoreBoard.text = memory.scoreBoardText();
    }
    
    public void Exit() {
        Application.Quit();
    }
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void GiveScore(int score) {
        manager.score = score;
    }
}
