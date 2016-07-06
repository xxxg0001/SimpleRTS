using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class AnimationEvent : MonoBehaviour
{

    public UnityEvent onAttack;

    // Update is called once per frame
    void OnAttack()
    {
        onAttack.Invoke();
    }
    void OnSkill()
    {
    }
}
