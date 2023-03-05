using System;
using Licht.Impl.Events;
using Licht.Interfaces.Events;
using Licht.Unity.Objects;

public class Killable : BaseGameObject
{
    public bool Dead { get; private set; }
    public event Action OnDeath;
    public virtual bool CanBeKilled => true;

    public enum KillableEvents
    {
        OnDeath
    }

    private IEventPublisher<KillableEvents, Killable> _eventPublisher;

    protected override void OnAwake()
    {
        base.OnAwake();
        _eventPublisher = this.RegisterAsEventPublisher<KillableEvents, Killable>();
    }

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
        _eventPublisher.PublishEvent(KillableEvents.OnDeath, this);
    }
}
