public class SubMachinegun : Weapon
{
    private int burstCount = 3;
    void Awake() {
        name = "SMG";
        damage = 80;
        magSize = 20;
        cooldown = 1.6f;
        reloadCooldown = 3.5f;
        weaponSpread = 0.03f;
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