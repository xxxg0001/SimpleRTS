using UnityEngine;
using System;

public class FireBall : SkillShape, IAreaEffect
{
    
    public float needTime;
    private float curtime;
    public float damageRange;

    private Vector3 targetPos;
    
    
    private ParticleSystem ps;
    

    // Use this for initialization
    void Awake()
    {
    
        ps = GetComponent<ParticleSystem>();
        ps.playOnAwake = false;
        
        ps.startLifetime = needTime;
        
    }

   
    // Update is called once per frame
    void Update()
    {
        if (ps.isPlaying) {
            curtime += Time.deltaTime;
            if (curtime >= needTime)
            {
                curtime = 0;
                Collider[] collider = Physics.OverlapSphere(targetPos, damageRange);                
                foreach (Collider c in collider)
                {
                    if ((c.CompareTag("Unit") || c.CompareTag("Building")) && caster.GetComponent<Base>().team != c.GetComponent<Base>().team)
                    {
                        foreach (IEffect effect in effects)
                        {
                            effect.cast(c.gameObject);
                        }
                    }
                }
                Destroy(gameObject);
            }
        }
    }    

    public override void Cast(Unit _caster, SkillTarget targetMethod)
    {
        caster = _caster;
        targetPos = targetMethod.pos;
        transform.position = caster.transform.position;
        var distance = Vector3.Distance(targetPos, transform.position);

        var vel = ps.velocityOverLifetime;
        vel.y = new ParticleSystem.MinMaxCurve(ps.gravityModifier * -Physics.gravity.y * ps.startLifetime / 2.0f);
        vel.z = new ParticleSystem.MinMaxCurve(distance / ps.startLifetime);
        transform.LookAt(targetPos);
        ps.Play();
        
    }

    public float GetEffectRange()
    {
        return damageRange;
    }
}
