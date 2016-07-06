using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    
    [TaskCategory("Custom")]
    public class GameObjectDistance : Conditional
    {

        public enum Operation
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            NotEqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }
        
        public SharedGameObject target1;
        public SharedGameObject target2;
        public Operation operation;
        public SharedFloat distance;
        




        public override TaskStatus OnUpdate()
        {
            if (target1.IsNone || target2.IsNone || distance.IsNone)
            {
                return TaskStatus.Failure;
            }

            float value = Vector3.Distance(target1.Value.transform.position, target2.Value.transform.position);

            switch (operation)
            {
                case Operation.LessThan:
                    return distance.Value < value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.LessThanOrEqualTo:
                    return distance.Value <= value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.EqualTo:
                    return distance.Value == value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.NotEqualTo:
                    return distance.Value != value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThanOrEqualTo:
                    return distance.Value >= value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThan:
                    return distance.Value > value ? TaskStatus.Success : TaskStatus.Failure;
            }
            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            operation = Operation.LessThan;
            target1 = null;
            target2 = null;
        }
    }
}