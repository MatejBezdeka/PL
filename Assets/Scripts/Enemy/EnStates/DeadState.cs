using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadState : StateController {
    public DeadState() {
        name = currentState.dead;
    }

    protected override void enter() {
        
    }
}
