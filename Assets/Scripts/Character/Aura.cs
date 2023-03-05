using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class Aura : BaseGameObject
{
    [field:SerializeField]
    public float InitialRadius { get; private set; }

    private float _radius;
    public float Radius
    {
        get => _radius;
        private set
        {
            _radius = value;
            OnAuraChanged?.Invoke(value);
        }
    }

    public event Action<float> OnAuraChanged;

    protected override void OnAwake()
    {
        base.OnAwake();
        _radius = InitialRadius;
    }

    public void Increase(float aura)
    {
        Radius += aura;
    }

    public void Decrease(float aura)
    {
        Radius = Math.Clamp(Radius - aura, 0, Radius);
    }
}
