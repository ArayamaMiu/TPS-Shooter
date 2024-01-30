using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : AcitionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._rHandAim.weight = 1f;
        actions._lHandIK.weight = 1f;
    }

    public override void UpdateState(ActionStateManager actions)
    {
        actions._rHandAim.weight = Mathf.Lerp(actions._rHandAim.weight, 1, 10 * Time.deltaTime);
        actions._lHandIK.weight = Mathf.Lerp(actions._lHandIK.weight, 1, 10 * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.Reload);
        }
    }

    bool CanReload(ActionStateManager action)
    {
        if(action._ammo._currentAmmo == action._ammo._clipSize)return false;
        else if(action._ammo._extraAmmo == 0)return false;
        else return true;
    }
}
