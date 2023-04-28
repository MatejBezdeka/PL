using UnityEngine;
using UnityEngine.AI;

public class RetreatState : StateController {
    Transform startTransform;
    NavMeshHit hit;
    static readonly int State = Animator.StringToHash("state");

    public RetreatState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player, ParticleSystem muzzleFlash) : base(enemy, agent, anim, player, muzzleFlash) {
        name = currentState.retreat;
    }

    protected override void enter() {
        agent.speed = (enemy.speed/4) * 3;
        anim.SetInteger(State, 0);
        base.enter();
    }

    protected override void update() {
        if (enemy.attackRange < enemy.DistaceBetweenEnemyAndPlayer()) {
            nextstate = new AttackState(enemy, agent, anim, player, muzzleFlash);
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
        anim.SetInteger(State, 999);
        base.exit();
    }
}