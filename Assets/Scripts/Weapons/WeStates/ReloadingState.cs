using UnityEngine;
public class ReloadingState : WeaponState {
    float reloadProgress;
    public ReloadingState(Weapon weapon, PlayerController player) : base(weapon, player) {
        name = currentState.reloading;
    }

    protected override void enter() {
        reloadProgress = 0;
        player.statsHandler.ammoChangText("Reloading");
        base.enter();
        GameManager.manager.PlayAudioCLip(weapon.reloadSoundStart);
    }

    protected override void update() {
        reloadProgress += Time.deltaTime;
        if (reloadProgress > weapon.reloadCooldown) {
            nextState = new NormalState(weapon, player);
            stage = stateStage.exit;
        }
    }

    protected override void exit() {
        GameManager.manager.PlayAudioCLip(weapon.reloadSoundEnd);
        weapon.Reload();
        player.statsHandler.ammoChangText(weapon.bulletsInMag + " / " + weapon.magSize);
        base.exit();
    }
}