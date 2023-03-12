using UnityEngine;
public class SwitchingGunState : WeaponState {
    float switchingProgress;
    Weapon nextWeapon;
    Weapon lastWeapon;
    public SwitchingGunState(Weapon lastWeapon, Weapon nextWeapon, PlayerController player) : base(lastWeapon, player) {
        name = currentState.switchingGun;
        this.nextWeapon = nextWeapon;
    }

    protected override void enter() {
        // weapon switchcooldown pro 1. i 2. zbraň!!!
        switchingProgress = 0.5f/*weapon.reloadCooldown + nextWeapon.reloadCooldown*/;
        lastWeapon = weapon;
        weapon = nextWeapon;
        base.enter();
    }

    protected override void update() {
        switchingProgress -= Time.deltaTime;
        if (switchingProgress < 0) {
            player.statsHandler.ammoChangeAmmoInMag(weapon.bulletsInMag, weapon.magSize);
            nextState = new NormalState(weapon, player);
            stage = stateStage.exit;
        }
    }

    protected override void exit() {
        Debug.Log("switch Over");
        lastWeapon.disableAndAbleMesh(nextWeapon);
        base.exit();
    }
}