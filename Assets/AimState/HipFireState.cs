using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState :AimBaseState
{
    public override void EnterState(AimStateManeger aim)
    {
        aim._anim.SetBool("Aiming", false);
        aim._currentFov = aim._hipFov;
    }

    public override void UpdateState(AimStateManeger aim)
    {
        if (Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
