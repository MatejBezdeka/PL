using UnityEngine;
using UnityEngine.AI;
public class StateController {
    protected enum currentState {
        idle, chase, attack, retreat, fastRetreat, dead, charge, evade/*, jump*/
    };
    protected enum stateStage {
        enter, update, exit
    };

    protected currentState name;
    protected stateStage stage;
    protected StateController nextstate;
    protected Enemy enemy;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected Transform player;

    protected StateController(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) {
        this.enemy = enemy;
        this.agent = agent;
        this.anim = anim;
        this.player = player;
        stage = stateStage.enter;
    }

    protected StateController() { }

    protected virtual void enter() {
        stage = stateStage.update;
    }

    protected virtual void update() { }

    protected virtual void exit() {
        stage = stateStage.exit;
    }

    public StateController Process() {
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
                return nextstate;
        }
        return this;
    }

    protected void lookAtPlayerOrNot(bool towards) {
        int plusMinus;
        if (towards)
            plusMinus = 1;
        else
            plusMinus = -1;
        Vector3 direction = enemy.DirectionOfEnemyToPlayer();
        //rotace ruky s pistoli
        //enemy.arm.transform.rotation = Quaternion.LookRotation(lookDirection);
        direction *= plusMinus;
        direction.y = enemy.transform.position.y;
        enemy.transform.rotation = Quaternion.LookRotation(direction);
    }

    protected void AtEdge() {
        nextstate = new ChargeState(enemy, agent, anim, player);
        stage = stateStage.exit;
    }
}