using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class ScoreOnKill : BaseGameObject
{
    [field:SerializeField]
    public int Score { get; private set; }

    [field: SerializeField]
    public Killable Killable { get; private set; }

    private Score _score;

    protected override void OnAwake()
    {
        base.OnAwake();
        _score = _score.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Killable.OnDeath += Killable_OnDeath;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Killable.OnDeath -= Killable_OnDeath;
    }

    private void Killable_OnDeath()
    {
        _score.GiveScore(Score);
    }
}
