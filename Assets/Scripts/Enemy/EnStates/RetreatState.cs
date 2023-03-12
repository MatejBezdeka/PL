using UnityEngine;
using UnityEngine.AI;

public class RetreatState : StateController {
    Transform startTransform;
    NavMeshHit hit;
    public RetreatState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) : base(enemy, agent, anim,player) {
        name = currentState.retreat;
    }

    protected override void enter() {
        agent.speed = (enemy.speed/4) * 3;
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
                enemy.Attack();
                enemy.MoveFromPlayer();
                lookAtPlayerOrNot(true);
            }
        }
    }

    protected override void exit() {
        agent.speed = enemy.speed;
        base.exit();
    }
}