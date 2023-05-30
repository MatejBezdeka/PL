using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    CharacterController controller;
    public PlayerUpgrader playerUpgrader { get; private set; }
    Vector3 playerVelocity;
    public Vector3 move { get; private set; }
    bool groundedPlayer;
    bool jumped;
    bool jumpedTwo;
    public int maxHp { get; internal set; }
    public int hp { get; protected internal set; }
    public float xpMultiplayer { get; internal set; } = 1;
    public float scoreMultiplayer { get; internal set; }  = 1;
    float playerSpeed = 6.5f;
    float jumpHeight = 0.5f;
    float gravity = 10;
    int armor = 1;
    //float dmgMultiplayer = 1;
    public StatsHandler statsHandler;
    int score = 0;
    
    // dash
    float dashLength = 0.3f;
    float dashRecharge = 2;
    float currentDashRecharge = 0;
    float dashCooldown = 0.8f;
    float currentDashCooldown = 0;
    int dashCount = 3;
    int maxDashes = 3;

    public PlayerInput playerInput;
    
    void Awake() {
        maxHp = 1000;
        hp = maxHp;
        statsHandler = gameObject.GetComponent<StatsHandler>();
        controller = gameObject.GetComponent<CharacterController>();
        playerUpgrader = gameObject.AddComponent<PlayerUpgrader>();
        playerInput = gameObject.GetComponent<PlayerInput>();
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

        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        move = new Vector3(input.x, 0, input.y);
        move = move.x * transform.right + move.z * transform.forward;
        //move = transform.right * (Input.GetAxis("Horizontal")) + transform.forward * (Input.GetAxis("Vertical"));
        controller.Move(move * (Time.deltaTime * playerSpeed));
        if (/*Input.GetButtonDown("Dash")*/ playerInput.actions["Dash"].triggered) {
            //TODO
            if (dashCount > 0 && dashCooldown <= currentDashCooldown) {
                //dash(move);
                StartCoroutine(DashCoroutine(move));
            }
        }
        if (/*Input.GetButtonDown("Jump")*/ playerInput.actions["Jump"].triggered && jumpedTwo == false) {
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
            hp = 0;
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