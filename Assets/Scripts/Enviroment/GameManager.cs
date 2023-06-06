using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;
using Random = System.Random;

public class GameManager : MonoBehaviour {
    public static GameManager manager = new GameManager();
    float difficultyComponent = 4.5f; // Difficulty parameter // smaller = worse at least 5 and max 1
    float waveAmplifier = 0.5f;       // Wave parameter of difficulty
    Random rn = new Random();
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] List<GameObject> typesOfEnemies = new List<GameObject>();
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas upgradeCanvas;
    [SerializeField] PlayerInput playerInput;
    AudioSource audioSource;
    UpgradeManager upgradeManager;
    
    List<GameObject> listOfSpawns;

    int difficulty;
    
    int minutes = 0;
    int seconds = 0;
    float milliseconds = 0;
    string minutesText = "00";
    string secondsText = "00";
    
    int enemiesCostInArena = 0;
    int enemiesCostLeft = 250;
    int baseDifficultyCost;
    public double enemyStrengthMultiplayer;
    double enemyCountMultiplyer;
    public gameState currentGameState { get; private set; } = gameState.go;

    double CalculateEnemyCountMultiplayer(int time) {
        if (time >= 25) {
            return (time / (difficultyComponent / 2)) + waveAmplifier * time - difficultyComponent * 3;
        }
        return (time / difficultyComponent) - waveAmplifier * Math.Cos(time) + 1;
    }
    double CalculateEnemyStrengthMultiplayer(int time) {
        if (time >= 25) {
            return (time / (difficultyComponent / 2)) + waveAmplifier * time - difficultyComponent * 4;
        }
        return (time / difficultyComponent) + waveAmplifier * Math.Cos(time) + 1;
    }
    public enum gameState {
        go,
        stop,
        upgrade,
        loading,
        boss
    }

    //List<Enemy> listOfEnemiesInArena;
    
    
    void Start() {
        currentGameState = gameState.go;
        Cursor.lockState = CursorLockMode.Locked;
        difficultyComponent -= difficulty;
        //find all spawnpoints    
        listOfSpawns = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
        
        upgradeManager = upgradeCanvas.GetComponent<UpgradeManager>();
        manager = GetComponent<GameManager>();
        
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.5f;

        enemyStrengthMultiplayer = CalculateEnemyStrengthMultiplayer(1);
        enemyCountMultiplyer = CalculateEnemyCountMultiplayer(1);
        baseDifficultyCost = enemiesCostLeft;
    }

    /*void FixedUpdate() {
        throw new NotImplementedException();
    }*/
    void Update() {
        //Debug.Log(enemiesWorthInArena);
       
        switch (currentGameState) {
            case gameState.go:
                if (/*Input.GetKeyDown(KeyCode.Escape)*/ playerInput.actions["Pause"].triggered) {
                    Pause();
                    return;
                }
                UpdateTime();
                break;
            case gameState.stop:
                if (/*Input.GetKeyDown(KeyCode.Escape)*/ playerInput.actions["Pause"].triggered) {
                    Pause();
                    return;
                }
                break;
            case gameState.upgrade:
            case gameState.loading:
            case gameState.boss:
                break;
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
        UpDifficulty();
        switch (minutes) {
            case 4:
                //smg 
                break;
            case 8:
                //melee
                break;
            case 10:
                //AR
                break;
            case 12:
                //shotgun
                break;
        }
        //up difficulty - 30 min mark death -> ramp up difficulty to make player's death imminent
        //at points add new enemies
        //make weapons available at some point in upgrade
        
    }

    void spawnEnemies() {
        while (enemiesCostLeft * enemyCountMultiplyer > enemiesCostInArena) {
            GameObject enemy = typesOfEnemies[rn.Next(0, typesOfEnemies.Count)];
            enemy = Instantiate(enemy, listOfSpawns[rn.Next(0, listOfSpawns.Count)].transform.position,
                UnityEngine.Quaternion.identity);
            Enemy comp = enemy.GetComponentInChildren<Enemy>();
            enemiesCostInArena += comp.value;
        }
    }

    void UpDifficulty() {
        enemyStrengthMultiplayer = CalculateEnemyStrengthMultiplayer(1);
        enemyCountMultiplyer = CalculateEnemyCountMultiplayer(1);
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

    public void SetDifficulty(int settingsDifficulty) {
        difficulty = settingsDifficulty;
    }
    public void EnemyDied(Enemy enemy) {
        enemiesCostInArena -= enemy.value;
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