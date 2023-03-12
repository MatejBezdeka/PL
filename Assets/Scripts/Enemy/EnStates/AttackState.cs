using UnityEngine;
using UnityEngine.AI;
public class AttackState : StateController {
    public AttackState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) : base(enemy, agent, anim,player) {
        name = currentState.attack;
    }

    protected override void enter() {
        agent.isStopped = true;
        base.enter();
    }

    protected override void update(){
        lookAtPlayerOrNot(true);
        float distance = enemy.DistaceBetweenEnemyAndPlayer();
        if (enemy.attackRange + 3 < distance) {
            nextstate = new ChaseState(enemy, agent, anim, player);
            stage = stateStage.exit;
        }else if (enemy.attackRange/2 > distance) {
            if (Random.Range(0, 100) > enemy.courage)
                nextstate = new FastRereatreatState(enemy, agent, anim, player);
            else
                nextstate = new RetreatState(enemy, agent, anim, player);
            stage = stateStage.exit;
        }else {
            enemy.Attack();
        }
    }

    protected override void exit() {
        agent.isStopped = false;
        base.exit();
    }
}