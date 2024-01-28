using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : MovementBaseState
{
    public override void EnterState(MovementStateManeger movement)
    {
        movement._anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManeger movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
        else if (movement._dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }

        if (movement._vInput < 0)
        {
            movement._currentMoveSpeed = movement._walkBackspeed;
        }
        else
        {
            movement._currentMoveSpeed = movement._walkSpeed;
        }
    }

    void ExitState(MovementStateManeger movement, MovementBaseState state)
    {
        movement._anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
