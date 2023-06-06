using System.Collections.Generic;
using UnityEngine;
public class Saving : MonoBehaviour {
    public List<int> scores = new List<int>();
    public List<string> names = new List<string>();
    const int n = 10;

    int LoadScore(int index) {
        return PlayerPrefs.GetInt(index.ToString());
    }
    void SaveData(){
        for (int i = 0; i < n; i++) {
            PlayerPrefs.SetInt(i.ToString(), scores[i]);
            PlayerPrefs.SetString(i + "name", names[i]);
        }
    }

    string LoadName(int index) {
        return PlayerPrefs.GetString(index + "name");
    }
    public void Load() {
        for (int i = 0; i < n; i++) {
            scores.Add(LoadScore(i));
            names.Add(LoadName(i));
        }
    }

    public void Save(int score, string name) {
        for (int i = 0; i < n; i++) {
            if (scores[i]<= score ) {
                for (int j = n-1; j > i; j--) {
                    Debug.Log(j + "tt");
                    scores[j] = scores[j-1];
                    names[j] = names[j-1];
                }
                scores[i] = score;
                names[i] = name;
                break;
            }
        }
        SaveData();
    }

    public string scoreBoardText() {
        string text = "";
        for (int i = 0; i < n; i++) {
            if (names[i] == "") {
                continue;
            }
            text += names[i] + ": " + scores[i] + "\n";
        }
        return text;
    }
}