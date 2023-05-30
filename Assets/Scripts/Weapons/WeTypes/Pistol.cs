using UnityEngine;

public class Pistol : Weapon {
    void Awake() {
        name = "Pistol";
        available = true;
        damage = 120;
        magSize = 12;
        cooldown = 0.3f;
        reloadCooldown = 3.2f;
        weaponSpread = 0.05f;
        muzzleFlash.transform.position = gunBarrel.position;
        base.Awake();
    }

    void Start() {
        playerInput = playerController.playerInput;
    }
    protected void Update() {
        base.update();
    }
}