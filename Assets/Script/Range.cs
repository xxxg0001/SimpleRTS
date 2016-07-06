using UnityEngine;
using System.Collections;

public class Range : MonoBehaviour
{
	public Unit unit;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider collider)
	{

		/*if ( collider.gameObject.CompareTag("Unit") || collider.gameObject.CompareTag("Building") ) {

			if (unit.GetComponent<Base>().team != collider.gameObject.GetComponent<Base>().team) {
				unit.Detected(collider.gameObject);
			}

		}*/
	}
}
