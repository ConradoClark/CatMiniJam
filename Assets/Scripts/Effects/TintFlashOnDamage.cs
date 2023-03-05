using Licht.Unity.Objects;
using UnityEngine;

public class TintFlashOnDamage : BaseGameObject
{
    [field: SerializeField]
    public TintFlash TintFlash { get; private set; }

    [field: SerializeField]
    public Damageable Damageable { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Damageable.OnDamage += Damageable_OnDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Damageable.OnDamage -= Damageable_OnDamage;
    }

    private void Damageable_OnDamage(int obj)
    {
        TintFlash.Flash();
    }
}
