using UnityEngine;
using System.Collections;







public struct SkillTarget
{
    public enum Method
    {
        None,
        Position,
        Object,
        Direction,
    }
    public Method method;
    public GameObject obj;
    public Vector3 pos;

    public object Value
    {
        get
        {
            switch(method)
            {
                case Method.Object:
                    return obj;
                case Method.Position:
                case Method.Direction:
                    return pos;
                default:
                    return null;
            }
        }
    }

    public void SetValue(Vector3 _pos)
    {   
        pos = _pos;
    }
    public void SetValue(GameObject _obj)
    {
        obj = _obj;
    }
}

public interface IAreaEffect  {
	float GetEffectRange();
}
public abstract class SkillShape: MonoBehaviour
{
    protected Unit caster;
    public System.Collections.Generic.List<IEffect> effects;
    public abstract void Cast(Unit _caster, SkillTarget _target);
}




public abstract class NormalAttack: MonoBehaviour
{
    protected Unit caster;
    public int damage;
    public float hitSpeed;
    public float hitRange;
    
    private Utility.Cooldown timer;

    protected GameObject target;
    void Awake()
    {
        caster = GetComponent<Unit>();
    }
    // Use this for initialization
    void Start()
    {
        caster.animator.SetFloat("HitSpeed", 2 / hitSpeed);
        timer.time = hitSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        timer.Update();
    }
    public bool CanCast(GameObject target)
    {

        if (timer.IsCooling)
        {
            return false;
        }
        if (target == null)
        {
            
            return false;
        }

        if (Utility.GetSurfaceDistance(caster.transform, target.transform) > hitRange)
        {
            return false;
        }
        return true;
    }
   
    public void Cast(GameObject _target)
    {
        if (!CanCast(_target))
        {
            return;
        }
        target = _target;
        timer.Start();
        caster.transform.LookAt(target.transform.position);
        caster.animator.SetBool("walking", false);
        caster.animator.SetTrigger("Attack");
        caster.ResetPath();
    }
}