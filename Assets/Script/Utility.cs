using UnityEngine;
using System.Collections;

public class Utility
{
    static public float GetSurfaceDistance(Transform trans1, Transform trans2)
    {
        
        var collider1 = trans1.GetComponent<Collider>();
        var collider2 = trans2.GetComponent<Collider>();
        var surfacePoint1 = trans1.position;
        var surfacePoint2 = trans2.position;

        if (collider1 != null)
        {
            surfacePoint1 = collider1.ClosestPointOnBounds(surfacePoint2);
        }
        if (collider2 != null)
        {
            surfacePoint2 = collider2.ClosestPointOnBounds(surfacePoint1);
        }

        float value = Vector3.Distance(surfacePoint1, surfacePoint2);
        return value;
    }
    static public float GetSurfaceDistance(Transform trans1, Vector3 point)
    {

        var collider1 = trans1.GetComponent<Collider>();
        var surfacePoint1 = trans1.position;
        if (collider1 != null)
        {
            surfacePoint1 = collider1.ClosestPointOnBounds(point);
        }
        float value = Vector3.Distance(surfacePoint1, point);
        return value;
    }
    [System.Serializable]
    public struct Cooldown
    {
        public float time;
        private float curtime;
        public void Update()
        {
            if (curtime > 0)
            {
                curtime -= Time.deltaTime;
            }
        }
        public bool IsCooling
        {
            get
            {
                return curtime > 0;
            }
        }
        public float CurrentPercent
        {
            get
            {
                return 1 - curtime / time;
            }
        }
        public void Start()
        {
            curtime = time;
        }
    }

   
}


