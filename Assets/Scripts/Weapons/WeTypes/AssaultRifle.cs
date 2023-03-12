public class AssaultRifle : Weapon {
    void Awake() {
        name = "Assault Rifle";
        damage = 200;
        magSize = 60;
        cooldown = 0.1f;
        reloadCooldown = 3.2f;
        weaponSpread = 0;
        base.Awake();
    }
    
    protected void Update() {
        base.update();
    }
}
