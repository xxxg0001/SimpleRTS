using UnityEngine;
using System.Collections;
using System;

public class ChargeBuff : SkillShape
{
   
    private SkillTarget target;
    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = new Vector3();
        switch (target.method)
        {
            case SkillTarget.Method.Object:
                if (target.obj == null)
                {
                    Destroy(gameObject);
                    caster.animator.SetBool("IsStab", false);
                    return;
                }
                pos = target.obj.transform.position;
                break;
            case SkillTarget.Method.Position:
                pos = target.pos;
                break;
        }

        if (Vector3.Distance(pos, caster.transform.position) > 1.0f)
        {
            caster.transform.LookAt(pos);
            caster.transform.position += caster.transform.forward * Time.deltaTime * 20;
        }else
        {
            if (target.method == SkillTarget.Method.Object)
            {
                foreach (IEffect effect in effects)
                {
                    effect.cast(target.obj);
                }
            }            
            caster.animator.SetBool("IsStab", false);
            Destroy(gameObject);
        }
	}

    public override void Cast(Unit _caster, SkillTarget _target)
    {
        caster = _caster;

        target = _target;
        transform.parent = caster.transform;
        caster.animator.SetBool("IsStab", true);
    }
}
