using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : MonoBehaviour {

    
    public Castle castle;
    static private Player _inst;
	public RectTransform skillsUI;	
    public Text Message;
    

    void Awake()
    {
        _inst = this;
    }

    static public Player Inst
    {
        get {
            return _inst;
        }
    }


    static public Hero Hero
    {
        get
        {
			return Inst.castle.hero;
        }
    }

    // Use this for initialization
    void Start () {
		
		

	}
	
    public void UpdateSkillsUI()
    {
        var button = skillsUI.GetChild(0);

        for (var i = 1; i < skillsUI.childCount; i++)
        {
            Destroy(skillsUI.GetChild(i).gameObject);
        }
        for (var i = 0; i < castle.hero.skills.Length; i++)
        {
            button.gameObject.SetActive(true);
            Transform tmp;
            if (i == 0)
            {
                tmp = button;
            }
            else {
                tmp = Instantiate(button);
                tmp.SetParent(skillsUI, false);
                var rect = tmp.GetComponent<RectTransform>();
                rect.localPosition = button.localPosition - new Vector3(-i* 70, 0, 0);
            }
            tmp.GetComponent<SkillButton>().idx = i;
            tmp.name = "Skill" + i;
            tmp.GetComponentInChildren<Text>().text = castle.hero.skills[i].skillName;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}


	public void CastSkill(int idx)
	{
        BaseSkill skill = castle.hero.skills[idx];
        switch (skill.targetMethod)
        {
            case SkillTarget.Method.None:
                castle.hero.CastSkill(null, idx);
                break;
            default:
                GameManager.Inst.controlState = CONTROL_STATE.UsingSkill;
                var targetSelector = GameManager.Inst.GetComponent<TargetSelector>();
                targetSelector.caster = castle.hero;
                targetSelector.idx = idx;
                targetSelector.OnPointerDown();
                break;
        }
		
	}
   
}
