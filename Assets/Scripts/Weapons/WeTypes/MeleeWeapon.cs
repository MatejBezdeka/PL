public class MeleeWeapon : Weapon { 
    void Start() {
        damage = 200;
        magSize = 120;
        cooldown = 0.1f;
        reloadCooldown = 3.2f;
        weaponSpread = 0;
        base.Awake();
    }
    protected void Update() {
        base.update();
    }
    public override void Attack() {
        throw new System.NotImplementedException();
    }
}
