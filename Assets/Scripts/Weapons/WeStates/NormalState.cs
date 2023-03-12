using UnityEngine;
public class NormalState : WeaponState {

    public NormalState(Weapon weapon, PlayerController player) : base(weapon, player) {
        name = currentState.normal;
    }

    protected override void enter() {
        if (weapon.bulletsInMag == 0) {
            nextState = new ReloadingState(weapon, player);
            stage = stateStage.exit;
        }
        else {
            base.enter();
        }
    }

    protected override void update() {
        weapon.update();
        if (Input.GetMouseButtonDown(0)) {
            nextState = new ShootState(weapon, player);
            stage = stateStage.exit;
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            if (weapon.reloading == false && weapon.bulletsInMag != weapon.magSize) {
                nextState = new ReloadingState(weapon, player);
                stage = stateStage.exit;
                return;
            }
        }
    }

    protected override void exit() {
        base.exit();
    }
}
