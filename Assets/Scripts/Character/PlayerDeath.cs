using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class PlayerDeath : BaseGameObject
    {
        [field:SerializeField]
        public GameObject DeathScreen { get; private set; }

        private Health _health;

        protected override void OnAwake()
        {
            base.OnAwake();
            _health = _health.FromScene();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _health.HP.OnChange += HP_OnChange;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _health.HP.OnChange -= HP_OnChange;
        }

        private void HP_OnChange(Licht.Unity.Objects.Stats.ScriptStat<int>.StatUpdate obj)
        {
            if (obj.NewValue > 0) return;

            DefaultMachinery.AddBasicMachine(Die());
        }

        private IEnumerable<IEnumerable<Action>> Die()
        {
            yield return TimeYields.WaitMilliseconds(GameTimer, 200);
            GameTimer.Multiplier = 0.001f;
            DeathScreen.SetActive(true);
            _health.HP.OnChange -= HP_OnChange;
        }
    }
}
