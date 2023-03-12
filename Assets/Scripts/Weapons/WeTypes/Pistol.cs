public class Pistol : Weapon {
    void Awake() {
        name = "Pistol";
        available = true;
        damage = 2000;
        magSize = 12;
        cooldown = 0.3f;
        reloadCooldown = 3.2f;
        weaponSpread = 0;
        base.Awake();
    }
    protected void Update() {
        base.update();
    }
}