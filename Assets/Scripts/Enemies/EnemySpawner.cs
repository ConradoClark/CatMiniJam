using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : BaseGameObject
{
    private EnemyPoolManager _enemyPoolManager;
    public EnemyDef[] EnemySpawns;
    private EnemyDef[] _currentSpawns;
    private Level _level;

    [Serializable]
    public struct EnemyDef
    {
        public int MinLevel;
        public float Growth;
        public ScriptPrefab Enemy;
        public Vector3 LeftSpawn;
        public Vector3 RightSpawn;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        _enemyPoolManager = _enemyPoolManager.FromScene();
        _currentSpawns = EnemySpawns.Where(sp => sp.MinLevel <= 1).ToArray();
        _level = _level.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(TestSpawn());
        _level.LevelStat.OnChange += LevelStat_OnChange;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _level.LevelStat.OnChange -= LevelStat_OnChange;
    }

    private void LevelStat_OnChange(Licht.Unity.Objects.Stats.ScriptStat<int>.StatUpdate obj)
    {
        _currentSpawns = EnemySpawns.Where(sp => sp.MinLevel <= _level.LevelStat.Value).ToArray();
    }

    private float MaxWaitTime()
    {
        return Mathf.Clamp(3 - (_level.LevelStat.Value-1) * 0.2f, 0.1f, float.MaxValue);
    }

    private IEnumerable<IEnumerable<Action>> TestSpawn()
    {
        while (ComponentEnabled)
        {
            yield return TimeYields.WaitSeconds(GameTimer, Random.Range(0.1f, MaxWaitTime()));
            var spawn = _currentSpawns[Random.Range(0, _currentSpawns.Length)];
            var pool = _enemyPoolManager.GetEffect(spawn.Enemy);

            var growth = spawn.Growth * _level.LevelStat.Value;
            var amount = Math.Max(1, Mathf.RoundToInt(Random.Range(0, growth)));

            DefaultMachinery.AddBasicMachine(Spawn(amount, spawn, pool));
        }
    }

    private IEnumerable<IEnumerable<Action>> Spawn(int amount, EnemyDef spawn, EnemyPool pool)
    {
        for (var i = 0; i < amount; i++)
        {
            if (!pool.TryGetFromPool(out var enemy)) continue;
            if (Random.Range(0, 2) == 0)
            {
                enemy.Direction = Vector2.right;
                enemy.transform.position = spawn.LeftSpawn;
            }
            else
            {
                enemy.Direction = Vector2.left;
                enemy.transform.position = spawn.RightSpawn;
            }

            enemy.Speed = Random.Range(enemy.MinSpeed, enemy.MaxSpeed);

            yield return TimeYields.WaitMilliseconds(GameTimer, Random.Range(100, 1000));
        }
    }
}
