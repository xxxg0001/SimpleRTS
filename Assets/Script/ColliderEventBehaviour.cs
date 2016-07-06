using UnityEngine;
using System.Collections;
using UnityEngine.Events;
[System.Serializable]
public class ColliderEvent : UnityEvent<Collider, GameObject>
{

}

public class ColliderEventBehaviour : MonoBehaviour {

    public ColliderEvent onTriggreEnter;
    void OnTriggerEnter(Collider c)
    {
        onTriggreEnter.Invoke(c, gameObject);
    }
}
