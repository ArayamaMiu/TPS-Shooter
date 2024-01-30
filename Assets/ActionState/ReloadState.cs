using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : AcitionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._rHandAim.weight = 0f;
        actions._lHandIK.weight = 0f;
        actions._anim.SetTrigger("Reload");
    }

    public override void UpdateState(ActionStateManager actions)
    {
     
    }
}
