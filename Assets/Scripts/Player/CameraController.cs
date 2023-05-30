using System;
using RetroAesthetics;
using Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    float sensitivity = 1f;
    Camera camera;
    Vector2 vector;
    [SerializeField] CharacterController player;
    [SerializeField] Transform leftArm;
    [SerializeField] GameObject hit;
    RetroCameraEffect retroEffect;
    cPrecision colorEffect;
    PlayerInput playerInput;
    void Start() {
        colorEffect = gameObject.GetComponent<cPrecision>();
        retroEffect = gameObject.GetComponent<RetroCameraEffect>();
        camera = Camera.main;
        Settings.applySettings += ChangeSettings;
        playerInput = GetComponentInParent<PlayerInput>();
        ChangeSettings();
    }

    void ChangeSettings() {
            sensitivity = PlayerPrefs.GetInt(OptionsObject.type.sensitivity.ToString()) / 100f;
            camera.fieldOfView = PlayerPrefs.GetInt(OptionsObject.type.fov.ToString());
            if (PlayerPrefs.GetInt(OptionsObject.type.filters.ToString()) == 1) {
                retroEffect.enabled = false;
                colorEffect.enabled = false;
            }
            else {
                retroEffect.enabled = true;
                colorEffect.enabled = true;
            }
    }
    void Update() {
        if (GameManager.manager.currentGameState != GameManager.gameState.go) {
            return;
        }

        Vector2 input = playerInput.actions["Look"].ReadValue<Vector2>();
        vector.x += input.x * (sensitivity/10);
        vector.y -= input.y * (sensitivity/10);
        vector.y = Mathf.Clamp(vector.y, -90, 90);
        camera.transform.rotation = Quaternion.Euler(vector.y, vector.x, 0);
        player.transform.rotation = Quaternion.Euler(0, vector.x, 0);
        leftArm.rotation = Quaternion.Euler(-vector.y-60,vector.x - 195,0);
    }
}