using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour
{


	public GameObject prefabUnit;
	public float productTime;
	private float curtime;
	//private bool running = false;

	// Use this for initialization
	void Start ()
	{
		curtime = 0;        
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (running == true) {
			curtime += Time.deltaTime;
			if (curtime >= productTime) {
				curtime = 0;
				Spawn ();
				//running = false;
			}
		//}
        /*
		if (Input.GetMouseButtonDown (0)) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, Mathf.Infinity, LayerMask.GetMask ("Building"))) {


				if (hit.collider.gameObject == gameObject) {
					running = true;
				}
			}
		}*/
	}

	void Spawn ()
	{
		var unit = Instantiate (prefabUnit);

		unit.transform.position = transform.FindChild ("pos").position;
		Team team = GetComponent<Base> ().team;
		unit.GetComponent<Base> ().team = team;
        unit.transform.LookAt(transform.FindChild("destination"));
        unit.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>().SetVariableValue("destination", transform.FindChild("destination").position);
	}
}
