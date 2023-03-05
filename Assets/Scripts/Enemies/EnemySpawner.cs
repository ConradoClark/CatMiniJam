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
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(TestSpawn());
    }

    private IEnumerable<IEnumerable<Action>> TestSpawn()
    {
        while (ComponentEnabled)
        {
            yield return TimeYields.WaitSeconds(GameTimer, Random.Range(0f, 3f));
            var spawn = EnemySpawns[Random.Range(0, EnemySpawns.Length)];
            var pool = _enemyPoolManager.GetEffect(spawn.Enemy);
            if (pool.TryGetFromPool(out var enemy))
            {
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
            }
        }
    }
}
