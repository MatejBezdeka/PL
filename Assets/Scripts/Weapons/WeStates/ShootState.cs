using UnityEngine;
using UnityEngine.InputSystem;

public class ShootState : WeaponState {
    public ShootState(Weapon weapon, PlayerController player) : base(weapon, player) {
        name = currentState.shooting;
    }
    protected override void enter() {
        //Debug.Log("Attack Enter");
        weapon.Attack();
        player.statsHandler.ammoChangText(weapon.magSize + "/" + weapon.magSize);
        if (weapon.bulletsInMag == 0) {
            nextState = new ReloadingState(weapon, player);
            stage = stateStage.exit;
        }
        player.statsHandler.ammoChangText(weapon.bulletsInMag + "/" +  weapon.magSize);
        if (/*Input.GetMouseButtonUp(0)*/ !weapon.playerInput.actions["Fire"].IsPressed()) {
            nextState = new NormalState(weapon, player);
            stage = stateStage.exit;   
        }
    }

    protected override void exit() {
        base.exit();
    }
}