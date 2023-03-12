using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour {
    CharacterController controller;
    public PlayerUpgrader playerUpgrader;
    Vector3 playerVelocity;
    public Vector3 PlayerVelocity => playerVelocity;
    bool groundedPlayer;
    bool jumped;
    bool jumpedTwo;
    public int maxHp { get; internal set; }
    public int hp { get; protected internal set; }
    public float xpMultiplayer { get; internal set; } = 1;
    public float scoreMultiplayer { get; internal set; }  = 1;
    protected float playerSpeed = 6.5f;
    float jumpHeight = 1.5f;
    float gravity = 20;
    int armor = 1;
    //float dmgMultiplayer = 1;
    public StatsHandler statsHandler { get; protected set; }
    int score = 0;
    
    // dash
    float dashLength = 0.3f;
    float dashRecharge = 2;
    float currentDashRecharge = 0;
    float dashCooldown = 0.8f;
    float currentDashCooldown = 0;
    int dashCount = 3;
    protected int maxDashes = 3;
    
    void Start() {
        maxHp = 1000;
        hp = maxHp;
        statsHandler = gameObject.GetComponent<StatsHandler>();
        controller = gameObject.GetComponent<CharacterController>();
        playerUpgrader = gameObject.AddComponent<PlayerUpgrader>();
    }

     void Update() {
        if (GameManager.manager.currentGameState != GameManager.gameState.go) {
            return;
        }
        if (playerVelocity.y < 0) {
            groundedPlayer = controller.isGrounded;
        }

        if (currentDashCooldown < dashCooldown ) {
            currentDashCooldown += Time.deltaTime;
        }
        if (dashCount != maxDashes) {
            currentDashRecharge += Time.deltaTime;
            if (currentDashRecharge >= dashRecharge) {
                dashCount++;
                currentDashRecharge = 0;
            }
        }
        if (groundedPlayer && playerVelocity.y < 1) {
            playerVelocity.y = 0f;
            jumped = false;
            jumpedTwo = false;
        }
        Vector3 move = transform.right * (Input.GetAxis("Horizontal")) + transform.forward * (Input.GetAxis("Vertical"));
        controller.Move(move * (Time.deltaTime * playerSpeed));
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashCount > 0 && dashCooldown <= currentDashCooldown) {
                //dash(move);
                StartCoroutine(DashCoroutine(move));
            }
        }
        if (Input.GetButtonDown("Jump") && jumpedTwo == false) {
            if (jumped) {
                jumpedTwo = true;
            }
            jumped = true;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * 3.0f * gravity);
        }
        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void GetHit(int damage) {
        if (armor > 0) {
            damage /= (1 + armor / 100);
        }
        hp -= damage;
        if (hp <= 0) {
            GameOver();
        }
        UpdateHpUI();
    }

    public void ChangeScore(int value) {
        score += (int) (value * scoreMultiplayer);
        statsHandler.scoreChange(score);
        if (statsHandler.xpChange((int)((value / 5) * xpMultiplayer))) {
            GameManager.manager.Upgrade();
        }
    }

    void GameOver() {
        GameManager.manager.Die(score);
        //TODO
    }
    IEnumerator DashCoroutine(Vector3 move) {
        currentDashCooldown = 0;
        dashCount--;
        float startTime = Time.time;
        while(Time.time < startTime + dashLength) {
            controller.Move(move * (Time.deltaTime * playerSpeed * 1.26f));
            yield return null;
        }
    }
    internal void UpdateHpUI() {
        statsHandler.hpChange(hp, maxHp); }
}