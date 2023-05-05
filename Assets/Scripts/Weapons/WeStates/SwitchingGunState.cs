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
        // weapon switchcooldown pro 1. i 2. zbraň?
        switchingProgress = 0.5f/*weapon.reloadCooldown + nextWeapon.reloadCooldown*/;
        player.statsHandler.ammoChangText("Switching");
        GameManager.manager.PlayAudioCLip(weapon.switchSoundStart);
        lastWeapon = weapon;
        weapon = nextWeapon;
        base.enter();
    }

    protected override void update() {
        switchingProgress -= Time.deltaTime;
        if (switchingProgress < 0) {
            nextState = new NormalState(weapon, player);
            stage = stateStage.exit;
        }
    }

    protected override void exit() {
        //Debug.Log("switch Over");
        GameManager.manager.PlayAudioCLip(weapon.switchSoundEnd);
        player.statsHandler.ammoChangText(weapon.bulletsInMag + "/" + weapon.magSize);
        player.statsHandler.ChangeGunIcon(nextWeapon.iconOfTheWeapon);
        lastWeapon.DisableAndAbleMesh(nextWeapon);
        base.exit();
    }
}