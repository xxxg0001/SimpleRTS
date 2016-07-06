using UnityEngine;

using System;

public class MeleeAttack : NormalAttack
{
    public void OnHit()
    {
        if (target == null)
        {
            return;
        }
        target.BroadcastMessage("OnHurt", damage);
        target = null;
    }

   
}
