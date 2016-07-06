using UnityEngine;
using System.Collections;

public class RecoverySpring : MonoBehaviour {
    public float range;
    public Utility.Cooldown interval;
    public int hp;
    private Team team;
    private Castle castle;

	// Use this for initialization
	void Start () {
        var projector = GetComponentInChildren<Projector>();
        if (projector)
        {
            projector.orthographicSize = range;
        }
        
        team = GetComponent<Base>().team;
        castle = GameManager.Inst.team[(int)team].castle;
	}

    // Update is called once per frame
    void Update() {
        interval.Update();
        if (interval.IsCooling)
        {
            return;
        }
        if (castle.hero == null)
        {
            return;
        }
        float dis = Vector3.Distance(transform.position, castle.hero.transform.position);
        if (dis < range)
        {
            castle.hero.GetComponentInChildren<Health>().HP += hp;
            interval.Start();
        }
    }
   
   
}
