using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private Unit unit;
    public BaseSkill[] skills;

    void Awake()
    {
        unit = GetComponent<Unit>();
        var skillsTrans = transform.FindChild("skills");
        skills = new BaseSkill[skillsTrans.childCount];
        for (var i=0; i < skillsTrans.childCount; i ++)
        {
            skills[i] = skillsTrans.GetChild(i).GetComponent<BaseSkill>();
        }
    }
	// Use this for initialization
	void Start () {
        if (Player.Hero == this)
        {
            var bt = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
            if (bt)
            {
                bt.SetVariableValue("isPlayer", true);
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CastSkill(GameObject target, int idx)
	{

		var skill = skills[idx];
		SkillTarget skillTarget = new SkillTarget();
        skillTarget.method = skill.targetMethod;
        switch (skill.targetMethod)
        {
            case SkillTarget.Method.Object:
                skillTarget.SetValue(target);
                break;
            case SkillTarget.Method.Position:
                skillTarget.SetValue(target.transform.position);
                break;
            case SkillTarget.Method.None:
                skillTarget.method = SkillTarget.Method.None;
                break;
            case SkillTarget.Method.Direction:
                skillTarget.SetValue(target.transform.position);
                break;
            default:
                return;
        }

		if (!skills[idx].CanCast(unit, skillTarget))
		{
			return;
		}
		skills[idx].Cast(unit, skillTarget);
	}
}
