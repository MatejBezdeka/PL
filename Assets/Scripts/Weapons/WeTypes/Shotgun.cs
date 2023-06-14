public class Shotgun : Weapon {
    void Awake() {
        name = "Shotgun";
        damage = 120;
        magSize = 5;
        cooldown = 1.25f;
        reloadCooldown = 4.2f;
        weaponSpread = 0.15f;
        base.Awake();
    }

    public override void Attack() {
        if (currentCooldown > 0) return;
        for (int i = 0; i < 5; i++) {
            ShootBullet();
        }
        bulletsInMag--;
        currentCooldown = cooldown;
    }
}