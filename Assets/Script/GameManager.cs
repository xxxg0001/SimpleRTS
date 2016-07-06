using UnityEngine;



public enum CONTROL_STATE
{
    Idle,
    UsingSkill,
}

public class GameManager : MonoBehaviour {

    public CONTROL_STATE controlState;
    public GameObject gameover;
    [System.Serializable]
    public class Team
    {
        public Castle castle;
        public Barrack barrack_F;
        public Barrack barrack_A;
        public RecoverySpring recoverySpring;

    }

    public Team[] team;
    static private GameManager _inst;
    
    

    

    static public GameManager Inst
    {
        get
        {
            return _inst;
        }
    }

    void Awake()
    {
        _inst = this;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
