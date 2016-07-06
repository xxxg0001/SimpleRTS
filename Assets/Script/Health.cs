using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	
	//红色血条贴图
	private Texture2D bloodBar;

	public int HP;
	public int maxHp;

  

	void Start ()
	{
		bloodBar = new Texture2D (500, 1);
		bloodBar.wrapMode = TextureWrapMode.Clamp;
	}

	void Update ()
	{
		transform.rotation = Camera.main.transform.rotation;
		transform.Rotate (new Vector3 (0, 180, 0));
		UpDateBloodBar ();
        if (HP > maxHp)
        {
            HP = maxHp;
        }
	}

	public void UpDateBloodBar ()
	{
		float cunrrentRed = HP * bloodBar.width / maxHp;  //直接除，会因为两个整型得到0，所以先乘以后除
		for (var x = 0; x < bloodBar.width; x++) { // 对每个坐标点
			for (var y = 0; y < bloodBar.height; y++) {//循环执行y轴从0开始，y轴小于血条的宽的话执行下面，否则+
				if (x < cunrrentRed)
					bloodBar.SetPixel (x, y, Color.red);   //x小于血条长度的范围涂成红色，
                else
					bloodBar.SetPixel (x, y, Color.gray);         //其他部位涂成黑色
			}
		}
		bloodBar.Apply (); // 应用该图
		GetComponent<MeshRenderer> ().material.mainTexture = bloodBar;// 将修改后的贴图给血条Cube


	}

	public void OnHurt (int damage)
	{
		HP -= damage;
		if (HP <= 0) {
            transform.parent.BroadcastMessage("OnDie");
			
		}
	}

}


