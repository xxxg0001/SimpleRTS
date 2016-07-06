using UnityEngine;
using System.Collections;


public class BaseSkill : MonoBehaviour
{

    public enum EffectType
    {
        Single,
        Area,
    }

    public SkillShape shape;

    public string skillName;
    
    public EffectType effectType = EffectType.Single;
    public float range;
    public Utility.Cooldown CDTime;
    public SkillTarget.Method targetMethod;

    // Update is called once per frame
    void Update()
    {
        CDTime.Update();
    }
    public bool CanCast(Unit caster, SkillTarget target)
    {
        if (CDTime.IsCooling)
        {
            return false;
        }
        switch (targetMethod)
        {
            case SkillTarget.Method.Object:
                if (target.obj == null)
                {
                    return false;
                }
                if (Utility.GetSurfaceDistance(caster.transform, target.obj.transform) > range)
                {
                    return false;
                }
                break;
            case SkillTarget.Method.Position:
                if (target.pos == Vector3.zero)
                {
                    return false;
                }
                if (Utility.GetSurfaceDistance(caster.transform, target.pos) > range)
                {
                    return false;
                }
                break;
            default:
                break;
        }

        return true;
    }

    public void Cast(Unit caster, SkillTarget target)
    {
        if (!CanCast(caster, target))
        {
            return;
        }
        var obj = Instantiate(shape);
        obj.effects = GetComponent<EffectContainer>().effects;
        obj.Cast(caster, target);
        CDTime.Start();
    }

    public float GetEffectRange()
    {
        if (effectType == EffectType.Area)
        {
            return (shape as IAreaEffect).GetEffectRange();
        }
        return 0;
    }
    //public abstract void Cast(SkillTarget _target);
}