using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BehaviorDesigner.Runtime.BehaviorTree))]
public class Scanner : MonoBehaviour {

    public float range;

    private BehaviorDesigner.Runtime.BehaviorTree behaviorTree;
    // Use this for initialization
    void Start () {
        behaviorTree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        behaviorTree.SetVariableValue("range", range);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Scan()
    {

        Collider[] collider = Physics.OverlapSphere(gameObject.transform.position, range);
        float minDistance = -1;
        GameObject target = null;
        foreach (Collider c in collider)
        {
            if ((c.CompareTag("Unit") || c.CompareTag("Building")) && c.GetComponent<Base>().team != GetComponent<Base>().team)
            {
                float distance = Utility.GetSurfaceDistance(c.transform, transform);
                if (minDistance < 0 || minDistance > distance)
                {
                    minDistance = distance;
                    target = c.gameObject;
                }
            }
        }
        if (behaviorTree.isActiveAndEnabled)
        {
            behaviorTree.SetVariableValue("target", target);
        }
    }
}
