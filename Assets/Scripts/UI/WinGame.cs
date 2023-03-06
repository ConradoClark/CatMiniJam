using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class WinGame : BaseGameObject
{   
    [field:SerializeField]
    public GameObject WinPanel { get; private set; }

    private Level _level;

    protected override void OnAwake()
    {
        base.OnAwake();
        _level = _level.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _level.LevelStat.OnChange += LevelStat_OnChange;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _level.LevelStat.OnChange -= LevelStat_OnChange;
    }

    private void LevelStat_OnChange(Licht.Unity.Objects.Stats.ScriptStat<int>.StatUpdate obj)
    {
        if (obj.NewValue != 16) return;

        DefaultMachinery.AddBasicMachine(Win());
    }

    private IEnumerable<IEnumerable<Action>> Win()
    {
        yield return TimeYields.WaitMilliseconds(GameTimer, 200);

        GameTimer.Multiplier = 0.0001f;
        WinPanel.SetActive(true);
    }
}
