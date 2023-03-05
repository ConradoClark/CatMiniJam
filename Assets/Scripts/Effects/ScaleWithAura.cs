using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class ScaleWithAura : BaseGameObject
{
    [field: SerializeField]
    public float InitialScale { get; private set; }
    [field:SerializeField]
    public float AuraProportion { get; private set; }

    private Aura _aura;
    protected override void OnAwake()
    {
        base.OnAwake();
        _aura = _aura.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        var scale = InitialScale + _aura.Radius * AuraProportion;
        transform.localScale = new Vector3(scale, scale, 0);
        _aura.OnAuraChanged += _aura_OnAuraChanged;
    }

    private void _aura_OnAuraChanged(float obj)
    {
        var scale = InitialScale + obj * AuraProportion;
        transform.localScale = new Vector3(scale, scale, 0);
    }
}
