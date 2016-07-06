using UnityEngine;
using System.Collections;

public class skill_charge : MonoBehaviour {

    public int damage;
    private float curtime;

    public float range = 3;
    public Unit unit;
	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        unit.transform.Rotate(new Vector3(0, 3*360 * Time.deltaTime, 0));
        Collider[] collider = Physics.OverlapSphere(unit.transform.position, range);

        curtime += Time.deltaTime;
        if (curtime >= 1)
        {
            curtime = 0;
            foreach (Collider c in collider)
            {
                if ((c.CompareTag("Unit") || c.CompareTag("Building")) && c.GetComponent<Base>().team != unit.GetComponent<Base>().team)
                {
                    c.BroadcastMessage("OnHurt", damage);

                }
            }
        }
        //unit.transform.RotateAround(unit.transform.position, Vector3.up, 180);
    }
}
