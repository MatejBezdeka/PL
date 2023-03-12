using UnityEngine;
using UnityEngine.AI;
public class ChaseState : StateController {
    public ChaseState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) : base(enemy, agent, anim,player) {
        name = currentState.chase;
    }

    protected override void enter() {
        base.enter();
    }

    protected override void update(){
        enemy.Move();
        if (enemy.attackRange > enemy.DistaceBetweenEnemyAndPlayer()) {
            nextstate = new AttackState(enemy, agent, anim, player);
            stage = stateStage.exit;
        }
    }

    protected override void exit() {
        base.exit();
    }
}
