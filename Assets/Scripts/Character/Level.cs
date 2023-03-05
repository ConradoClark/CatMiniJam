using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using UnityEngine;

public class Level : BaseGameObject
{
    [field:SerializeField]
    public CounterStat LevelStat { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        LevelStat.Value = 1;
    }
}
