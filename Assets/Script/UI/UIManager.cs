using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
        
	
	}
    public void quit()
    {
        Application.Quit();
    }
    public void again()
    {
        SceneManager.LoadScene("DEMO");
    }
}
