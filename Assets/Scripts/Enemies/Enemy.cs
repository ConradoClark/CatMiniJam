using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using Licht.Unity.Pooling;
using UnityEngine;

public class Enemy : EffectPoolable
{
    public Vector2 Direction { get; set; }

    public float Speed { get; set; }

    [field:SerializeField]
    public float MinSpeed { get; private set; }

    [field: SerializeField]
    public float MaxSpeed { get; private set; }

    public bool Interrupted { get; private set; }

    public override void OnActivation()
    {
    }
}
