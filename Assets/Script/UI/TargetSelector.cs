
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TargetSelector : MonoBehaviour
{
	public GameObject indicator;
	private GameObject targetIndicator;
	private GameObject rangeIndicator;

	public int idx;
	private float range;
	public Hero caster;




	// Use this for initialization
	void Start()
	{




	}
	void Update()
	{
       
	}
	// Update is called once per frame
	void FixedUpdate()
	{
        if (caster == null)
        {
            Destroy(rangeIndicator);
            Destroy(targetIndicator);
            GameManager.Inst.controlState = CONTROL_STATE.Idle;
            return;
        }
        if (GameManager.Inst.controlState != CONTROL_STATE.UsingSkill)
		{
			return;
		}

		if (rangeIndicator != null && targetIndicator != null)
		{
			var inputPosition = Vector2.zero;
			#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
			inputPosition = Input.mousePosition;
			#else
			if (Input.touchCount > 0)
			{
			inputPosition = Input.GetTouch(0).position;
			}
			#endif

			Ray ray = Camera.main.ScreenPointToRay(inputPosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
			{
				var orgpos = targetIndicator.transform.position;
				var center = new Vector3(orgpos.x, 0, orgpos.z);
				if (hit.collider.name == "Terrain")
				{
                    
					var pos = FixPos(new Vector3(hit.point.x, 0, hit.point.z));
					var posCenter = pos - center;
					if (posCenter.magnitude < range)
					{
						rangeIndicator.transform.position = pos;
					}
					else {
						rangeIndicator.transform.position = posCenter.normalized * range + center;
					}
				}
			}
		}
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;//为真，则点击在UI上
		}
		#else
		if (Input.touchCount <= 0)
		{
		return;
		}

		if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
		{
		return;//为真，则点击在UI上
		}
		#endif
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		if (Input.GetMouseButtonUp(0))
		#else
		if (Input.GetTouch(0).phase == TouchPhase.Ended)
		#endif
		{
			BaseSkill skill = caster.skills [idx];
            //            var orgpos = targetIndicator.transform.position;
            switch (skill.targetMethod)
            {
                case SkillTarget.Method.Position:
                    caster.CastSkill(rangeIndicator, idx);
                    break;
                case SkillTarget.Method.Object:
                    caster.CastSkill(FindTarget(), idx);
                    break;
                case SkillTarget.Method.Direction:
                    caster.CastSkill(rangeIndicator, idx);
                    break;
                default:
                    break;
            }
			Destroy (rangeIndicator);
			Destroy (targetIndicator);
			GameManager.Inst.controlState = CONTROL_STATE.Idle;
			//Destroy(gameObject);

		}

		/*else if (Input.GetMouseButtonUp(1))
        {
            toggle.isOn = false;
        }*/
	}

    Vector3 FixPos(Vector3 hit)
    {
        GameObject target = null;

        BaseSkill skill = caster.skills[idx];
        if (skill.targetMethod == SkillTarget.Method.Object)
        {
            float minDistance = -1;
            Collider[] collider = Physics.OverlapSphere(rangeIndicator.transform.position, 2);
            foreach (Collider c in collider)
            {
                if ((c.CompareTag("Unit") || c.CompareTag("Building")) && c.GetComponent<Base>().team != caster.GetComponent<Base>().team)
                {
                    float distance = Utility.GetSurfaceDistance(c.transform, rangeIndicator.transform.position);
                    if (minDistance < 0 || minDistance > distance)
                    {
                        minDistance = distance;
                        target = c.gameObject;
                    }
                }
            }
        }        
        if (target != null)
        {
            return target.transform.position;
        }
        else
        {
            return hit;
        }
    }
	GameObject FindTarget()
	{
		Collider[] collider = Physics.OverlapSphere(rangeIndicator.transform.position, 2);
		float minDistance = -1;
		GameObject target = null;
		foreach (Collider c in collider)
		{
			if ((c.CompareTag("Unit") || c.CompareTag("Building")) && c.GetComponent<Base>().team != caster.GetComponent<Base>().team)
			{
				float distance = Utility.GetSurfaceDistance(c.transform, rangeIndicator.transform.position);
				if (minDistance < 0 || minDistance > distance)
				{
					minDistance = distance;
					target = c.gameObject;
				}
			}
		}
		return target;
	}

	public void OnPointerDown()
	{


//		if (GameManager.Inst.controlState != CONTROL_STATE.UsingSkill)
//		{
//			return;
//		}
//
			BaseSkill skill = caster.skills [idx];


			rangeIndicator = Instantiate(indicator);
			switch(skill.effectType) {
			case BaseSkill.EffectType.Area:
				rangeIndicator.GetComponent<Projector>().orthographicSize = skill.GetEffectRange();
                break;
			default:
				rangeIndicator.GetComponent<Projector> ().orthographicSize = 2;	
				break;
			}
			rangeIndicator.GetComponent<RangeIndicator>().canMove = true;
			rangeIndicator.GetComponent<RangeIndicator>().limitRadius = 0;


			targetIndicator = Instantiate(indicator);

			range = skill.range;

			targetIndicator.GetComponent<Projector>().orthographicSize = skill.range;
			targetIndicator.transform.parent = caster.transform;
			targetIndicator.transform.localPosition = Vector3.zero;
			targetIndicator.transform.localScale = new Vector3(1, 1, 1);
			targetIndicator.transform.localRotation = new Quaternion(1, 0, 0, 1);
			GameManager.Inst.controlState = CONTROL_STATE.UsingSkill;

	}





}
