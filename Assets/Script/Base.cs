using UnityEngine;
using System.Collections;

public enum Team
{
	Team1,
	Team2
}

public class Base : MonoBehaviour
{

	public Team team;

	// Use this for initialization
	void Start ()
	{
		if (GetComponent<Renderer> () != null) {
			if (team == Team.Team1) {
				GetComponent<Renderer> ().material.color = Color.yellow;
			} else if (team == Team.Team2) {
				GetComponent<Renderer> ().material.color = Color.blue;
			}
		}

		Renderer[] renderers;

        var body = transform.FindChild("Body");
        if (body == null)
        {
            return;
        }

		renderers = body.GetComponentsInChildren<Renderer> ();
		foreach (Renderer renderer in renderers) {
			if (team == Team.Team1) {
				renderer.material.color = Color.yellow;
			} else if (team == Team.Team2) {
				renderer.material.color = Color.blue;
			}
		}


	}

	// Update is called once per frame
	void Update ()
	{

	}

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
