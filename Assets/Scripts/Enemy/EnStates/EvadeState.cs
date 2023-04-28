using UnityEngine;
using UnityEngine.AI;

public class EvadeState : StateController {
    public EvadeState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player, ParticleSystem muzzleFlash) : base(enemy, agent, anim, player, muzzleFlash) {
        name = currentState.evade;
    }
    
    protected override void enter() {
        
        
        base.enter();
    }

    protected override void update(){
        
    }

    protected override void exit() {
        
        base.exit();
    }
}