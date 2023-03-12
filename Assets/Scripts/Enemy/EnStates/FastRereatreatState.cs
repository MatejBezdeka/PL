using UnityEngine;
using UnityEngine.AI;

public class FastRereatreatState : StateController {
    NavMeshHit hit;
    public FastRereatreatState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) : base(enemy, agent, anim,player) {
        name = currentState.fastRetreat;
    }

    protected override void enter() {
        //Debug.Log("fast");
        agent.speed = enemy.speed * 1.5f;
        base.enter();
    }

    protected override void update() {
        if (enemy.attackRange < enemy.DistaceBetweenEnemyAndPlayer()) {
            nextstate = new AttackState(enemy, agent, anim, player);
            stage = stateStage.exit;
        }
        else {
            if (agent.FindClosestEdge(out hit) && hit.distance < agent.radius) {
                AtEdge();
            }else {
                enemy.MoveFromPlayer();
                lookAtPlayerOrNot(false);
            }
        }
    }

    protected override void exit() {
        agent.speed = enemy.speed;
        base.exit();
    }
}