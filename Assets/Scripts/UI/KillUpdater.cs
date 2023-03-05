using System;
using Licht.Unity.Objects;
using TMPro;
using UnityEngine;
public class KillUpdater : BaseGameObject
{
    [field: SerializeField]
    public TMP_Text TextComponent { get; private set; }

    [field: SerializeField]
    public CounterStat KillStat { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        TextComponent.text = KillStat.InitialValue.ToString().PadLeft(3, '0');
        KillStat.OnChange += KillStat_OnChange;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        KillStat.OnChange -= KillStat_OnChange;
    }

    private void KillStat_OnChange(Licht.Unity.Objects.Stats.ScriptStat<int>.StatUpdate obj)
    {
        TextComponent.text = obj.NewValue.ToString().PadLeft(3, '0');
    }
}
