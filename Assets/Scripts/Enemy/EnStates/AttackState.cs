using UnityEngine;
using UnityEngine.AI;
public class AttackState : StateController {
    static readonly int Attack = Animator.StringToHash("attack");
    static readonly int State = Animator.StringToHash("state");

    public AttackState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) : base(enemy, agent, anim,player) {
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
            nextstate = new ChaseState(enemy, agent, anim, player);
            stage = stateStage.exit;
        }else if (enemy.attackRange/2 > distance) {
            if (Random.Range(0, 100) > enemy.courage)
                nextstate = new FastRereatreatState(enemy, agent, anim, player);
            else 
                nextstate = new RetreatState(enemy, agent, anim, player);
            stage = stateStage.exit;
        }else {
            anim.Play("Shoot");
            enemy.Attack();
        }
    }

    protected override void exit() {
        agent.isStopped = false;
        anim.Play("Run");
        base.exit();
    }
}