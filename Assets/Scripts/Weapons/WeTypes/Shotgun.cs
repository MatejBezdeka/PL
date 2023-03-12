public class Shotgun : Weapon {
    void Awake() {
        name = "Shotgun";
        damage = 20;
        magSize = 10;
        cooldown = 0.7f;
        reloadCooldown = 2.2f;
        weaponSpread = 0.2f;
        base.Awake();
    }

    public override void Attack() {
        if (currentCooldown > 0) return;
        for (int i = 0; i < 8; i++) {
            shootBullet();
        }
        bulletsInMag--;
        currentCooldown = cooldown;
    }
}