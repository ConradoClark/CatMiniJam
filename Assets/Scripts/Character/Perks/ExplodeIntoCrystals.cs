using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplodeIntoCrystals : BaseGameObject
{
    [field: SerializeField]
    public SnowCrystal Crystal { get; private set; }

    [field: SerializeField]
    public ScriptPrefab SmallerCrystals { get; private set; }

    [field: SerializeField]
    public ScriptPrefab BreakEffect { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Crystal.OnMaxHeight += Crystal_OnMaxHeight;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Crystal.OnMaxHeight -= Crystal_OnMaxHeight;
    }

    private void Crystal_OnMaxHeight()
    {
        Crystal.EndEffect();

        BreakEffect.TrySpawnEffect(transform.position, out _);

        var amount = 2;

        Vector2 objectDirection = Quaternion.Euler(0, 0, Random.Range(-90f, 90)) * Vector2.up;

        var angleBetweenObjects = Mathf.Abs(Mathf.Atan2(objectDirection.y, objectDirection.x) - Mathf.Atan2(Vector2.up.y, Vector2.up.x)) * Mathf.Rad2Deg;
        var angleOffset = -angleBetweenObjects / (amount - 1);

        var angleSwitch = Random.Range(0, 2) == 0 ? 1 : -1;

        var directions = Enumerable.Range(1, amount)
            .Select(i =>
                amount == 1
                    ? Vector3.up
                    : Quaternion.Euler(0, 0, angleOffset + i * angleBetweenObjects / (amount - 1)) * Vector2.up);

        foreach (var direction in directions)
        {
            if (!SmallerCrystals.TrySpawnEffect(transform.position, out var crystal)) continue;
            crystal.CustomProps["DirX"] = direction.x * angleSwitch;
            crystal.CustomProps["DirY"] = direction.y;
        }
    }
}
