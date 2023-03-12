using UnityEngine;

public class WeaponState : MonoBehaviour {
    public enum currentState {
        normal, shooting, reloading, switchingGun 
    };
    protected enum stateStage {
        enter, update, exit
    };
    public currentState name {get; protected set;}
    protected stateStage stage;
    protected WeaponState nextState;
    protected Weapon weapon;
    protected PlayerController player;

    public WeaponState(Weapon weapon, PlayerController player) {
        this.weapon = weapon;
        this.player = player; 
    }
   
    protected virtual void enter() {
        stage = stateStage.update; 
    }

    protected virtual void update() { }
    
    protected virtual void exit() {
        stage = stateStage.exit;
    }
    public WeaponState Process() {
        switch (stage) {
            case stateStage.enter:
                enter();
                break;
            case stateStage.update:
                update();
                break;
            case stateStage.exit:
            default:
                exit();
                return nextState;
        }
        return this;
    }
}