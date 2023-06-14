public class AssaultRifle : Weapon {
    void Awake() {
        name = "Assault Rifle";
        damage = 65;
        magSize = 20;
        cooldown = 0.26f;
        reloadCooldown = 3.8f;
        weaponSpread = 0.08f;
        base.Awake();
    }
    
    protected void Update() {
        base.update();
    }
}
