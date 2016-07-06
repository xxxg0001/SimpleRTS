using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
    [TaskCategory("Basic/SharedVariable")]
    [TaskDescription("Sets the SharedGameObject variable to NULL. Returns Success.")]
    public class SetSharedGameObjectToNull : Action
    {
        [RequiredField]
        [Tooltip("The SharedGameObject to set")]
        public SharedGameObject targetVariable;

        public override TaskStatus OnUpdate()
        {
            targetVariable.Value = null;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetVariable = null;
        }
    }
}