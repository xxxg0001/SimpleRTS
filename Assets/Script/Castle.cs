using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Castle : MonoBehaviour {

    
    public Hero[] prefabHero;

    public Text Message;
    

    //[HideInInspector]
    public Hero hero;

    public float productTime;
    public float timeLeft;
    public bool running;

    public int heroIdx;
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
       
        if (Player.Inst.castle != this)
        {

            SummonHero(Random.Range(0, 2));
        }
       
        if (running)
        {
            
            timeLeft -= Time.deltaTime;
            if (Message != null)
            {
                Message.gameObject.SetActive(true);
                Message.text = "英雄召唤中..." + string.Format("{0:f2}", timeLeft);
            }
                
            
            if (timeLeft <= 0)
            {
                var pos = transform.FindChild("pos").position;
                hero = Instantiate(prefabHero[heroIdx]);
                hero.transform.position = pos;
                Team team = GetComponent<Base>().team;
                hero.GetComponent<Base>().team = team;
                hero.transform.LookAt(transform.FindChild("destination"));
                if (Player.Inst.castle == this)
                {
                    
                    Player.Inst.UpdateSkillsUI();
                    hero.gameObject.AddComponent<Controller>();
                }
                else
                {
                    var bt = hero.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
                    if (bt)
                    {
                        bt.SetVariableValue("destination", transform.FindChild("destination").position);
                        bt.SetVariableValue("bloodpos", GameManager.Inst.team[(int)team].recoverySpring.transform.position);
                    }
                }
                running = false;
                if (Message != null)
                {
                    Message.gameObject.SetActive(false);
                }
                
            }
            
        }
	}

    public void SummonHero(int idx)
    {
        if (hero != null || running)
        {
            return;
        }
        running = true;
        heroIdx = idx;
        timeLeft = productTime;

    }
    
    public void OnDie()
    {
        GameManager.Inst.gameover.SetActive(true);
    }
}
