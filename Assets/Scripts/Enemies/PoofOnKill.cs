using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class PoofOnKill : BaseGameObject
    {
        [field:SerializeField]
        public Killable Killable { get; private set; }

        [field: SerializeField]
        public ScriptPrefab Effect { get; private set; }

        [field: SerializeField]
        public Vector3 Offset { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            Killable.OnDeath += OnDeath;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Killable.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            Effect.TrySpawnEffect(transform.position + Offset, out _);
        }
    }
}
