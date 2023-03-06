using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using UnityEngine;

public class DisableOnEnable : BaseGameObject
{
    [field: SerializeField]
    public MonoBehaviour Target { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.enabled = false;
    }
}
