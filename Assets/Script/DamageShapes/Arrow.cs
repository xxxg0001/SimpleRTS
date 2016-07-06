using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Arrow : MonoBehaviour
{
	public float speed = 10f;

    private int damage;
    private GameObject target;

    // Use this for initialization
    void Start ()
	{
	
	}

    public void Cast(int _damage, GameObject _target)
    {
        damage = _damage;
        target = _target;
    }

    // Update is called once per frame
    void Update ()
	{
		if (target == null) {
			Destroy (gameObject);
			return;
		}
        
		transform.LookAt (target.transform);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		float dist = Vector3.Distance (target.transform.position, transform.position);
		if (dist < 1) {
            target.BroadcastMessage("OnHurt", damage);
            Destroy (gameObject);
		}
	}
}
