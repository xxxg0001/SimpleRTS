using UnityEngine;
using System;

public class ArrowAttack : NormalAttack
{
    
    public GameObject prefabArrow;



    public void OnHit()
    {
        if (target == null)
        {
            return;
        }
        var arrow = Instantiate(prefabArrow);
        arrow.transform.position = caster.transform.position;
        arrow.GetComponent<Arrow>().Cast(damage, target);
        target = null;

    }
}
