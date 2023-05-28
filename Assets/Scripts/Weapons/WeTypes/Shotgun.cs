public class Shotgun : Weapon {
    void Awake() {
        name = "Shotgun";
        damage = 80;
        magSize = 10;
        cooldown = 0.7f;
        reloadCooldown = 2.2f;
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