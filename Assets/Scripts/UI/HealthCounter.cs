using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class HealthCounter : BaseGameObject
{
    [field: SerializeField]
    public RuntimeAnimatorController FullHeart { get; private set; }
    [field: SerializeField]
    public RuntimeAnimatorController HalfHeart { get; private set; }
    [field: SerializeField]
    public RuntimeAnimatorController EmptyHeart { get; private set; }
    private Health _health;
    private Animator[] _hearts;

    protected override void OnAwake()
    {
        base.OnAwake();
        _health = _health.FromScene();
        _hearts = GetComponentsInChildren<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _health.HP.OnChange += HP_OnChange;
        AdjustSprites();
    }

    private void HP_OnChange(Licht.Unity.Objects.Stats.ScriptStat<int>.StatUpdate obj)
    {
        AdjustSprites();
    }

    private void AdjustSprites()
    {
        for (var i = 0; i < _hearts.Length; i++)
        {
            var heart = _hearts[i];
            heart.runtimeAnimatorController = _health.HP.Value >= (i + 1)*2 ? FullHeart :
                _health.HP.Value > i * 2 ? HalfHeart : EmptyHeart;
        }
    }
}
