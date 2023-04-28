using UnityEngine;
using UnityEngine.AI;
public class AttackState : StateController {
    static readonly int Attack = Animator.StringToHash("attack");
    static readonly int State = Animator.StringToHash("state");

    public AttackState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player, ParticleSystem muzzleFlash) : base(enemy, agent, anim, player, muzzleFlash) {
        name = currentState.attack;
    }

    protected override void enter() {
        agent.isStopped = true;
        base.enter();
        anim.SetInteger(State, 1);
    }

    protected override void update(){
        lookAtPlayerOrNot(true);
        float distance = enemy.DistaceBetweenEnemyAndPlayer();
        if (enemy.attackRange + 3 < distance) {
            nextstate = new ChaseState(enemy, agent, anim, player, muzzleFlash);
            stage = stateStage.exit;
        }else if (enemy.attackRange/2 > distance) {
            if (Random.Range(0, 100) > enemy.courage)
                nextstate = new FastRereatreatState(enemy, agent, anim, player, muzzleFlash);
            else 
                nextstate = new RetreatState(enemy, agent, anim, player, muzzleFlash);
            stage = stateStage.exit;
        }else {
            
            enemy.Attack();
        }
    }

    protected override void exit() {
        agent.isStopped = false;
        anim.Play("Run");
        base.exit();
    }
}