using System;
using Licht.Unity.Objects;

public class Killable : BaseGameObject
{
    public bool Dead { get; private set; }
    public event Action OnDeath;
    public virtual bool CanBeKilled => true;

    protected override void OnEnable()
    {
        base.OnEnable();
        Dead = false;
    }

    public void Kill()
    {
        if (!CanBeKilled) return;
        Dead = true;
        OnDeath?.Invoke();
    }
}
