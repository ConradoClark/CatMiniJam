using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemyOnDeath : BaseGameObject
{
    [field: SerializeField]
    public Killable Killable { get; private set; }

    [field: SerializeField]
    public Enemy Self { get; private set; }

    [field: SerializeField]
    public Vector3 Offset { get; private set; }

    private EnemyPoolManager _poolManager;

    [field: SerializeField]
    public ScriptPrefab Enemy { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        _poolManager = _poolManager.FromScene();
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
        if (!_poolManager.GetEffect(Enemy).TryGetFromPool(out var enemy)) return;
        enemy.Direction = Self.Direction;
        enemy.transform.position = Self.transform.position + Offset;
        enemy.Speed = Random.Range(enemy.MinSpeed, enemy.MaxSpeed);
    }
}
