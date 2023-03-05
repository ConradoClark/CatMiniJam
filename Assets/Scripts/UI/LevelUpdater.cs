using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using TMPro;
using UnityEngine;

public class LevelUpdater : BaseGameObject
{
    [field:SerializeField]
    public TMP_Text TextComponent { get; private set; }

    private Level _level;
    protected override void OnAwake()
    {
        base.OnAwake();
        _level = _level.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _level.LevelStat.OnChange += Stat_OnChange;
    }

    private void Stat_OnChange(Licht.Unity.Objects.Stats.ScriptStat<int>.StatUpdate obj)
    {
        TextComponent.text = $"Level {obj.NewValue}";
    }
}
