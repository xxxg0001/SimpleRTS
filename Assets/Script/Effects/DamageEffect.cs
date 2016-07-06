using UnityEngine;
using System.Collections;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class DamageEffect : IEffect
{
    public int damage;

    public void cast(GameObject target)
    {
        
        target.BroadcastMessage("OnHurt", damage);
    }
    public void reverse()
    {
        
    }
    public DamageEffect(EffectConfig config)
    {
        damage = config.param;
    }
}
