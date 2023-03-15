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
    Saving memory;
    public static int score;
    void Start() {
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
        nameInput.interactable = false;
        memory.Save(score, nameInput.text);
        nameInput.text = "Saved";
        scoreBoard.text = memory.scoreBoardText();
    }
    
    public void Exit() {
        Application.Quit();
    }
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
