using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    
    [TaskCategory("Custom")]
    public class SurfaceDistance : Conditional
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
        
        public SharedVariable target2;

        public Operation operation;
        public SharedFloat distance;

        public override TaskStatus OnUpdate()
        {
            
            if (target1.IsNone || target2.IsNone || distance.IsNone)
            {
                return TaskStatus.Failure;
            }
            float value = 0;
            switch (target2.GetValue().GetType().ToString())
            {
                case "UnityEngine.GameObject":
                    value = Utility.GetSurfaceDistance(target1.Value.transform, (target2.GetValue() as GameObject).transform);
                    break;

                case "UnityEngine.Vector3":
                    value = Utility.GetSurfaceDistance(target1.Value.transform, (Vector3)target2.GetValue());
                    break;
                default:
                    return TaskStatus.Failure;
            }
            
            switch (operation)
            {
                case Operation.LessThan:
                    return value < distance.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.LessThanOrEqualTo:
                    return value <= distance.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.EqualTo:
                    return value == distance.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.NotEqualTo:
                    return value != distance.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThanOrEqualTo:
                    return value >= distance.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThan:
                    return value > distance.Value ? TaskStatus.Success : TaskStatus.Failure;
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