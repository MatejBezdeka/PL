using UnityEngine;

public class Pistol : Weapon {
    void Awake() {
        name = "Pistol";
        available = true;
        damage = 2000;
        magSize = 12;
        cooldown = 0.3f;
        reloadCooldown = 3.2f;
        weaponSpread = 0;
        muzzleFlash.transform.position = gunBarrel.position;
        base.Awake();
    }
    protected void Update() {
        base.update();
    }
}