using UnityEngine;
using UnityEngine.AI;
public class ChaseState : StateController {
    static readonly int State = Animator.StringToHash("state");

    public ChaseState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player, ParticleSystem muzzleFlash) : base(enemy, agent, anim, player, muzzleFlash) {
        name = currentState.chase;
    }

    protected override void enter() {
        anim.SetInteger(State, 0);
        base.enter();
    }

    protected override void update(){
        enemy.Move();
        lookAtPlayerOrNot(true);
        if (enemy.attackRange > enemy.DistaceBetweenEnemyAndPlayer()) {
            nextstate = new AttackState(enemy, agent, anim, player, muzzleFlash);
            stage = stateStage.exit;
        }
    }

    protected override void exit() {
        anim.SetInteger(State, 999);
        base.exit();
    }
}
