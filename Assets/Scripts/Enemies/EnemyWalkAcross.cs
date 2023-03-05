using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using UnityEngine;

public class EnemyWalkAcross : BaseGameObject
{
    [field: SerializeField]
    public Enemy Enemy { get; private set; }
    [field: SerializeField]
    public LichtPhysicsObject PhysicsObject { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(HandleWalk());
    }

    private IEnumerable<IEnumerable<Action>> HandleWalk()
    {
        while (ComponentEnabled)
        {
            PhysicsObject.ApplySpeed(Enemy.Direction * Enemy.Speed);
            yield return TimeYields.WaitOneFrameX;
        }
    }

}
