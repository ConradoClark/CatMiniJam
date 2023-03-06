using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using Licht.Unity.Pooling;
using UnityEngine;

public class DespawnOffscreen : BaseGameObject
{
    [field: SerializeField]
    public EffectPoolable EffectPoolable { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(HandleDistance());
    }

    private IEnumerable<IEnumerable<Action>> HandleDistance()
    {
        while (ComponentEnabled)
        {
            if (Vector2.Distance(transform.position, Camera.main.transform.position) > 15)
            {
                EffectPoolable.EndEffect();
                yield break;
            }
            yield return TimeYields.WaitOneFrameX;
        }
    }
}

