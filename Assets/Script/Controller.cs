using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;//为真，则点击在UI上
        }
#else
        if (Input.touchCount <= 0)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return;//为真，则点击在UI上
        }
#endif
        if (GameManager.Inst.controlState != CONTROL_STATE.Idle)
        {
            return;
        }
		if (Input.GetMouseButtonDown (0)) {
            
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, Mathf.Infinity, LayerMask.GetMask ("Terrain", "Unit", "Building"))) {

                var obj = hit.collider.gameObject;
                switch ( LayerMask.LayerToName(obj.layer))
                {
                    case "Terrain":
                        GetComponent<BehaviorDesigner.Runtime.BehaviorTree>().SetVariableValue("destination", new Vector3(hit.point.x, 0, hit.point.z));
                        break;
                    case "Unit":
                    case "Building":
                        GetComponent<BehaviorDesigner.Runtime.BehaviorTree>().SetVariableValue("destination", Vector3.zero);
                        GetComponent<BehaviorDesigner.Runtime.BehaviorTree>().SetVariableValue("target", obj);
                        break;
                }
				

			}

		} else if (Input.GetMouseButtonDown (1)) {


		}
	}
}
