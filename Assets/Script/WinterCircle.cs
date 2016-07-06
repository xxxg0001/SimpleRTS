using UnityEngine;
using System.Collections;
using System;

public class WinterCircle : MonoBehaviour 
{
    private GameObject pp;
    void Start ()
    {
        GameObject m_circle = new GameObject();
        m_circle.name = "mCircle";
        pp = GameObject.Find("pp");
        MeshFilter filter = m_circle.AddComponent<MeshFilter>();
        m_circle.AddComponent<MeshRenderer>();
        DrawCircle(m_circle, filter, 20, 150);
	}
    /// <summary>
    /// 生成扇形函数
    /// </summary>
    /// <param name="canvas">圆心的GameObject</param>
    /// <param name="filter"></param>
    /// <param name="raidus">半径</param>
    /// <param name="angle">圆心角</param>
    private void DrawCircle(GameObject canvas, MeshFilter filter, float raidus, float angle)
    {
        int ANGLE_STEP = 15;
        var mesh = new Mesh();
        int len = (int)Math.Floor(angle / ANGLE_STEP);
        len = len + 2;
        Vector3[] vs = new Vector3[len];
        //第一个为圆心
        
        Matrix4x4 m = pp.transform.localToWorldMatrix;
        Debug.Log(m.ToString());

        /*
         * 1 0 0 1
         * 0 1 0 2
         * 0 0 1 3
         * 0 0 0 0
         */

        Vector3 v0 = new Vector3(m.m03, m.m13, m.m23);
        vs[0] = v0;
        for (int i = 1; i < len; i++)
        {
            canvas.transform.position = v0;
            canvas.transform.rotation = pp.transform.rotation;
            if (i != len - 1)
            {//非最后一个点
                canvas.transform.Rotate(new Vector3(0, ANGLE_STEP * (i - 1), 0));
                var v = canvas.transform.position + canvas.transform.forward * raidus;
                vs[i] = v;
            }
            else
            {//最后一个顶点
                canvas.transform.Rotate(new Vector3(0, angle, 0));
                var v = canvas.transform.position + canvas.transform.forward * raidus;
                vs[i] = v;
            }
        }
        //三角形数
        int tc = (int)Math.Floor(angle / ANGLE_STEP);
        int[] triangles = new int[tc * 3];
        for (int j = 0; j < tc; j++)
        {
            triangles[j * 3] = 0;
            triangles[j * 3 + 1] = j + 1;
            triangles[j * 3 + 2] = j + 2;
        }
        canvas.transform.position = v0;
        canvas.transform.rotation = pp.transform.rotation;
        mesh.vertices = vs;   //顶点Vertex数组
        mesh.triangles = triangles;  //三角形数组
        filter.mesh = mesh;
    }
}
