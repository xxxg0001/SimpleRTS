using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("Custom")]
    public class HpConditional : Conditional
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
        public SharedGameObject target;
        public SharedFloat hpPercent;
        public Operation operation;
        

        public override TaskStatus OnUpdate()
        {
            if (target.IsNone || hpPercent.IsNone)
            {
                return TaskStatus.Failure;
            }
            var health =  target.Value.GetComponentInChildren<Health>();
            if (health == null)
            {
                return TaskStatus.Failure;
            }
            float curhp = health.HP * 100.0f / health.maxHp;

            switch (operation)
            {
                case Operation.LessThan:
                    return curhp < hpPercent.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.LessThanOrEqualTo:
                    return curhp <= hpPercent.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.EqualTo:
                    return curhp == hpPercent.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.NotEqualTo:
                    return curhp != hpPercent.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThanOrEqualTo:
                    return curhp >= hpPercent.Value ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThan:
                    return curhp > hpPercent.Value ? TaskStatus.Success : TaskStatus.Failure;
            }
            return TaskStatus.Failure;
        }
    }
}