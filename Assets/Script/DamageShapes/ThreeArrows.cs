using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ThreeArrows : SkillShape, IAreaEffect
{

    public float speed = 10f;
    public Utility.Cooldown lifeTime;
    public GameObject[] arrows ;


    // Use this for initialization
    void Start()
    {
        
      
    }

    

    // Update is called once per frame
    void Update()
    {
        lifeTime.Update();
        
        if (!lifeTime.IsCooling)
        {
            Destroy(gameObject);
            return;
        }
        //transform.LookAt(target.transform);
        int count = arrows.Length;
        foreach (GameObject arrow in arrows)
        {
            if (arrow)
            {
                arrow.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                count--;
            }
        }
        if (count <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void OnHit(Collider c, GameObject arrow)
    {
        var target = c.gameObject;
        var layer = target.layer;
        if (target == null)
        {
            Destroy(arrow);
            return;
        }
        if (layer == LayerMask.NameToLayer("Unit") || layer == LayerMask.NameToLayer("Building"))
        {
            print(c);
            print(target);
            print(target.name);

            if (target.GetComponent<Base>().team != caster.GetComponent<Base>().team)
            {
                foreach (IEffect effect in effects)
                {
                    effect.cast(target);
                }
                Destroy(arrow);
            }
        }
    }

    public override void Cast(Unit _caster, SkillTarget _target)
    {
        caster = _caster;
        transform.position = caster.transform.position;
        
        lifeTime.Start();
        transform.LookAt(_target.pos);
    }

    public float GetEffectRange()
    {
        return 3;
    }
}
