using UnityEngine;
public class ReloadingState : WeaponState {
    float reloadProgress;
    public ReloadingState(Weapon weapon, PlayerController player) : base(weapon, player) {
        name = currentState.reloading;
    }

    protected override void enter() {
        Debug.Log("Reload Enter");
        reloadProgress = 0;
        player.statsHandler.ammoChangeReaload();
        base.enter();
    }

    protected override void update() {
        reloadProgress += Time.deltaTime;
        if (reloadProgress > weapon.reloadCooldown) {
            nextState = new NormalState(weapon, player);
            stage = stateStage.exit;
        }
    }

    protected override void exit() {
        Debug.Log("Reload Exit");
        weapon.Reload();
        player.statsHandler.ammoChangeAmmoInMag(weapon.bulletsInMag, weapon.magSize);
        base.exit();
    }
}