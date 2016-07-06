using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour {

    private Image image;
	public int idx = 0;
    private BaseSkill skill;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        skill = Player.Hero.skills[idx];
    }

    // Update is called once per frame
    void Update() {
        if (Player.Hero == null)
        {
           gameObject.SetActive(false);
        }
        if (skill.CDTime.CurrentPercent > 0)
        {
            image.fillAmount = skill.CDTime.CurrentPercent;
        }
        else
        {
            image.fillAmount = 1;
        }
	}

	public void OnClick()
	{
        if ( image.fillAmount < 1)
        {
            return;
        }
		Player.Inst.CastSkill (idx);
	}
}
