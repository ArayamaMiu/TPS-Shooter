using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManeger aim)
    {
        aim._anim.SetBool("Aiming", true);
        aim._currentFov = aim._adsFov;
    }

    public override void UpdateState(AimStateManeger aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchState(aim.Hip);
    }
}
