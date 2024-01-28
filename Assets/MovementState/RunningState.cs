using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MovementBaseState
{
    public override void EnterState(MovementStateManeger movement)
    {
        movement._anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManeger movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Walk);
        }
        else if (movement._dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
            ;
        }
        if (movement._vInput < 0)
        {
            movement._currentMoveSpeed = movement._runBackspeed;
        }
        else
        {
            movement._currentMoveSpeed = movement._runSpeed;
        }
    }

    void ExitState(MovementStateManeger movement, MovementBaseState state)
    {
        movement._anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
