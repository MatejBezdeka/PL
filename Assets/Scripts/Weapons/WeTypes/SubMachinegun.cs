public class SubMachinegun : Weapon
{
    private int burstCount = 3;
    void Awake() {
        name = "SMG";
        damage = 200;
        magSize = 25;
        cooldown = 1.6f;
        reloadCooldown = 1.2f;
        weaponSpread = 0.08f;
        base.Awake();
    }

    void Update() {
        base.update();
    }

    public override void Attack() {
        int burst;
        if (currentCooldown > 0) return;
        if (burstCount > bulletsInMag) {
            burst = bulletsInMag;
        }
        else {
            burst = burstCount;
        }
        for (int i = 0; i < burst; i++) {
            Invoke(nameof(ShootBullet), 0.1f * i); 
            bulletsInMag--;    
        }
        currentCooldown = cooldown;
    }
}