using UnityEngine;

public class Pistol : Weapon {
    void Awake() {
        name = "Pistol";
        available = true;
        damage = 65;
        magSize = 9;
        cooldown = 0.85f;
        reloadCooldown = 2.2f;
        weaponSpread = 0.03f;
        muzzleFlash.transform.position = gunBarrel.position;
        base.Awake();
    }

    void Start() {
        base.Start();
    }
    protected void Update() {
        base.update();
    }
}