using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Quaternion = System.Numerics.Quaternion;
using Random = System.Random;

public class GameManager : MonoBehaviour {
    public static GameManager manager = new GameManager();
    static int dif;
    Random rn = new Random();
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] List<GameObject> typesOfEnemies = new List<GameObject>();
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas upgradeCanvas;
   // [SerializeField] Slider volume;
    AudioSource audioSource;
    UpgradeManager upgradeManager;
    public int difficulty { get; private set; }
    int minutes = 0;
    int seconds = 0;
    float milliseconds = 0;
    string minutesText = "00";
    string secondsText = "00";
    int enemiesWorthInArena = 0;
    public gameState currentGameState { get; private set; } = gameState.go;

    public enum gameState {
        go,
        stop,
        upgrade,
        loading,
        boss
    }

    List<Enemy> listOfEnemiesInArena;
    List<GameObject> listOfSpawns;
    int gameProgress = 200;
    int rampUpMultiplayer = 1;
    
    void Start() {
        currentGameState = gameState.go;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        //find all spawnpoints    
        listOfSpawns = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
        enemiesWorthInArena = 0;
        upgradeManager = upgradeCanvas.GetComponent<UpgradeManager>();
        manager = GetComponent<GameManager>();
        manager.SetDifficulty(difficulty);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

    /*void FixedUpdate() {
        throw new NotImplementedException();
    }*/
    void Update() {
        //Debug.Log(enemiesWorthInArena);
       
        switch (currentGameState) {
            case gameState.go:
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Pause();
                    return;
                }
                UpdateTime();
                break;
            case gameState.stop:
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Pause();
                    return;
                }
                break;
            case gameState.upgrade:
            case gameState.loading:
            case gameState.boss:
                break;
        }
        if (Input.GetKeyDown(KeyCode.U)) {
            Upgrade();
        }
        
    }

    //Invoke(nameof(shootBullet), 0.1f * i);
    void UpdateTime() {
        
        milliseconds += Time.deltaTime;
        if (milliseconds >= 1) {
            milliseconds = 0;
            seconds++;
            if (seconds == 60) {
                minutes++;
                MinuteChange();
                seconds = 0;
                if (minutes < 10) {
                    minutesText = "0" + minutes;
                }
                else {
                    minutesText = minutes.ToString();
                }
            }

            if (seconds < 10) {
                secondsText = "0" + seconds;
            }
            else {
                secondsText = seconds.ToString();
            }

            SecondChange();
        }
    }

    void SecondChange() {
        spawnEnemies();
        timeText.text = minutesText + ":" + secondsText;

    }

    void MinuteChange() {
        switch (minutes) {
            case 2:
                UpDifficulty();
                break;
            case 60:

                break;
            //atd
        }
        //up difficulty - 30 min mark death -> ramp up difficulty to make player's death imminent
        //at points add new enemies
        //make weapons available at some point in upgrade
        //čas 15min boss I 30min boss II -> infinite
    }

    void spawnEnemies() {
        while (gameProgress - enemiesWorthInArena > 0) {
            GameObject enemy = typesOfEnemies[rn.Next(0, typesOfEnemies.Count)];
            enemy = Instantiate(enemy, listOfSpawns[rn.Next(0, listOfSpawns.Count)].transform.position,
                UnityEngine.Quaternion.identity);
            Enemy comp = enemy.GetComponentInChildren<Enemy>();
            enemiesWorthInArena += comp.value;
            //listOfEnemiesInArena.Add(comp);
        }
    }

    void UpDifficulty() {
        gameProgress += 100 * rampUpMultiplayer;
    }

    public void Upgrade() {
        mainCanvas.enabled = !mainCanvas.enabled;
        upgradeCanvas.enabled = !upgradeCanvas.enabled;
        PauseStart(gameState.upgrade);
        if (upgradeCanvas.enabled) {
            upgradeManager.LevelUp();
        }

    }

    public void Pause() {
        mainCanvas.enabled = !mainCanvas.enabled;
        pauseCanvas.enabled = !pauseCanvas.enabled;
        PauseStart(gameState.stop);
    }

    public void SetDifficulty(int settingDifficulty) {
        dif = settingDifficulty;
    }
    public void EnemyDied(Enemy enemy) {
        enemiesWorthInArena -= enemy.value;
        //listOfEnemiesInArena.Remove(enemy);
    }

    void PauseStart(gameState state) {
        if (currentGameState == state) {
            currentGameState = gameState.go;
            Time.timeScale = 1;
            LockCursor();
        }
        else {
            UnlockCursor();
            Time.timeScale = 0;
            currentGameState = state;
        }
        Cursor.visible = !Cursor.visible;
    }
    void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Die(int score) {
        UnlockCursor();
        Destroy(audioSource);
        //remove audio
        DeathManager.score = score;
        SceneManager.LoadScene("DeathScene");
    }

    public void PlayAudioCLip(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}