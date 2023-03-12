using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateController {
    public IdleState(Enemy enemy, NavMeshAgent agent, Animator anim, Transform player) : base(enemy, agent, anim,player) {
        name = currentState.idle;
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
