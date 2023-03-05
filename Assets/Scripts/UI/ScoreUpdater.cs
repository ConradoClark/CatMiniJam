using System;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Unity.Builders;
using Licht.Unity.Objects;
using TMPro;
using UnityEngine;

public class ScoreUpdater : BaseGameObject
{
    [field: SerializeField]
    public TMP_Text TextComponent { get; private set; }

    [field: SerializeField]
    public CounterStat ScoreStat { get; private set; }

    [field: SerializeField]
    public int NumberOfDigits { get; private set; }

    private int _internalValue;

    protected override void OnEnable()
    {
        base.OnEnable();
        ScoreStat.OnChange += ScoreStat_OnChange;
        _internalValue = ScoreStat.Value;
        UpdateText();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ScoreStat.OnChange -= ScoreStat_OnChange;
    }

    private void ScoreStat_OnChange(Licht.Unity.Objects.Stats.ScriptStat<int>.StatUpdate obj)
    {
        DefaultMachinery.AddUniqueMachine("UpdateScore", UniqueMachine.UniqueMachineBehaviour.Replace,
            UpdateScore(obj.NewValue));
    }

    private IEnumerable<IEnumerable<Action>> UpdateScore(int newValue)
    {

        yield return new LerpBuilder(f => _internalValue = Mathf.RoundToInt(f), () => _internalValue)
            .SetTarget(newValue)
            .Over(0.5f)
            .Easing(EasingYields.EasingFunction.QuadraticEaseOut)
            .OnEachStep(_ =>
            {
                UpdateText();
            })
            .UsingTimer(UITimer)
            .Build();

        UpdateText();
    }

    private void UpdateText()
    {
        TextComponent.text = _internalValue.ToString().PadLeft(NumberOfDigits, '0');
    }
}
