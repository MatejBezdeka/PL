using UnityEngine;
using UnityEngine.AI;
public class ChargeState : StateController {
    private float cooldown = 0.5f;
    private float currentCooldown;

    public ChargeState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) : base(enemy, agent, anim, player) {
        name = currentState.charge;
    }

    protected override void enter() {
        //Debug.Log("chaargee!");
        currentCooldown = cooldown;
        agent.acceleration *= 2;
        agent.speed = enemy.speed * 5;
        //agent.SetDestination(new Vector3(player.transform.position.x*Random.Range(-1,1), player.transform.position.y*Random.Range(-1,1)));
        agent.SetDestination(new Vector3(player.position.x + Random.Range(-0.9f, 0.9f), player.position.y + Random.Range(-0.9f, 0.9f)));
        base.enter();
    }

    protected override void update() {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0) {
            nextstate = new RetreatState(enemy, agent, anim, player);
            stage = stateStage.exit;
        }
    }

    protected override void exit() {
        agent.acceleration /= 2;
        agent.speed = enemy.speed;
        base.exit();
    }
}