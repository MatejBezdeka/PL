using UnityEngine;
public class ShootState : WeaponState {
    public ShootState(Weapon weapon, PlayerController player) : base(weapon, player) {
        name = currentState.shooting;
    }
    protected override void enter() {
        nextState = new NormalState(weapon, player);
        //Debug.Log("Attack Enter");
        weapon.Attack();
        player.statsHandler.ammoChangeAmmoInMag(weapon.magSize, weapon.magSize);
        if (weapon.bulletsInMag == 0) {
            nextState = new ReloadingState(weapon, player);
            stage = stateStage.exit;
        }
        player.statsHandler.ammoChangeAmmoInMag(weapon.bulletsInMag, weapon.magSize);
        if (Input.GetMouseButtonUp(0)) {
            stage = stateStage.exit;   
        }
    }

    protected override void exit() {
        base.exit();
    }
}