using UnityEngine;
using System;

public class RotationBuff : SkillShape
{
    
    public int range;
    
    
    public float duration;
    private Utility.Cooldown interval;
    private Utility.Cooldown timer;
    // Use this for initialization
    void Start()
    {
        interval.time = 0.3f;
        

    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();
        interval.Update();
        if (!timer.IsCooling)
        {
            Destroy(gameObject);
            caster.animator.SetBool("IsStab", false);
            return;
        }
        caster.transform.FindChild("Body").transform.Rotate(new Vector3(0, 3 * 360 * Time.deltaTime, 0));
        if (interval.IsCooling)
        {
            return;
        }
        interval.Start();   
        Collider[] collider = Physics.OverlapSphere(caster.transform.position, range);
        foreach (Collider c in collider)
        {
            if ((c.CompareTag("Unit") || c.CompareTag("Building")) && c.GetComponent<Base>().team != caster.GetComponent<Base>().team)
            {

                foreach (IEffect effect in effects)
                {
                    effect.cast(c.gameObject);
                }

            }
        }
    }

    public override void Cast(Unit _caster, SkillTarget _target)
    {
        caster = _caster;

        transform.parent = caster.transform;
        caster.animator.SetBool("IsStab", true);
        timer.time = duration;
        timer.Start();
    }
}
