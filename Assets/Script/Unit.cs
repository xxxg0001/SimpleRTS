using UnityEngine;
using System.Collections;


public class Unit : MonoBehaviour
{    

    private NormalAttack normalAttack;
	
	private NavMeshAgent nav;

    [HideInInspector]
    public Animator animator;
	// Use this for initialization
	void Awake ()
	{

        animator = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent> ();
        normalAttack = GetComponent<NormalAttack>();

	}

    void Start()
    {
        //nav.speed = speed;
		var bt = GetComponent<BehaviorDesigner.Runtime.BehaviorTree> ();
		if (bt) {
			bt.SetVariableValue ("self", gameObject);
			bt.SetVariableValue ("hitRange", normalAttack.hitRange);
            
		}
    }
	

   
    // Update is called once per frame
    void Update ()
	{
     
    }
   

    public bool IsCurAnimName(string name)
    {
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);

        return animatorState.IsName("Base Layer." + name) || animator.IsInTransition(0);
    }

    public void Attack(GameObject target)
	{
        
		if (!normalAttack.CanCast (target)) {
			return;
		}
		normalAttack.Cast (target);
	}

    public void Idle ()
	{
        
		nav.ResetPath ();
		animator.SetBool("walking", false);
	}

	public void ResetPath ()
	{
		nav.ResetPath ();
	}

	public bool IsMoving ()
	{
		return nav.hasPath;
	}

	public void MoveTo (Vector3 destination)
	{
        nav.SetDestination (destination);
        
        animator.SetBool("walking", true);
    }

	public void MoveTo (GameObject target)
	{
		if (target == null) {
			return;
		}
		nav.SetDestination (target.transform.position);
        
        animator.SetBool("walking", true);
  
	}
   
}
