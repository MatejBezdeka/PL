using UnityEngine;
using UnityEngine.AI;

public class FastRereatreatState : StateController {
    NavMeshHit hit;
    static readonly int State = Animator.StringToHash("state");

    public FastRereatreatState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player, ParticleSystem muzzleFlash) : base(enemy, agent, anim, player, muzzleFlash) {
        name = currentState.fastRetreat;
    }

    protected override void enter() {
        //Debug.Log("fast");
        agent.speed = enemy.speed * 1.5f;
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
                enemy.MoveFromPlayer();
                lookAtPlayerOrNot(false);
            }
        }
    }

    protected override void exit() {
        agent.speed = enemy.speed;
        anim.SetInteger(State, 999);
        base.exit();
    }
}