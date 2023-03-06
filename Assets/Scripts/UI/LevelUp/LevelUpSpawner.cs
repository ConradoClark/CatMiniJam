using System;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUpSpawner : BaseGameObject
{
    [field:SerializeField]
    public GameObject LevelUp { get; private set; }

    [field: SerializeField]
    public PerkMenuAction Perk1 { get; private set; }
    [field: SerializeField]
    public PerkMenuAction Perk2 { get; private set; }
    [field: SerializeField]
    public PerkMenuAction Perk3 { get; private set; }

    private Perk[] _perks;

    protected override void OnAwake()
    {
        base.OnAwake();
        _perks = FindObjectsOfType<Perk>(true);
    }

    public IEnumerable<IEnumerable<Action>> Spawn()
    {
        if (!SelectPerk()) yield break; // handle this later

        yield return TimeYields.WaitMilliseconds(GameTimer, 200);

        GameTimer.Multiplier = 0.0001f;
        LevelUp.SetActive(true);

        yield return TimeYields.WaitOneFrameX;

        while (!Perk1.Chosen && !Perk2.Chosen && !Perk3.Chosen)
        {
            yield return TimeYields.WaitOneFrameX;
        }

        GameTimer.Multiplier = 1f;
        LevelUp.SetActive(false);
    }

    public bool SelectPerk()
    {
        var spawnablePerks = _perks
            .Where(p => !p.Activated && !p.Blocked && p.Requires.All(req => _perks.Any(perk => perk == req && perk.Activated)))
            .ToArray();

        if (spawnablePerks.Length == 0) return false;

        var perk1 = spawnablePerks[Random.Range(0, spawnablePerks.Length)];
        var perk2 = spawnablePerks[Random.Range(0, spawnablePerks.Length)];
        var perk3 = spawnablePerks[Random.Range(0, spawnablePerks.Length)];

        Perk1.Perk = perk1;
        Perk2.Perk = perk2;
        Perk3.Perk = perk3;
        return true;
    }
}
