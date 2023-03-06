using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class RandomizeHeight : BaseGameObject
    {
        public float MinHeight;
        public float MaxHeight;

        protected override void OnEnable()
        {
            base.OnEnable();

            DefaultMachinery.AddBasicMachine(SetHeight());
        }

        private IEnumerable<IEnumerable<Action>> SetHeight()
        {
            yield return TimeYields.WaitOneFrameX;
            transform.position += new Vector3(0, Random.Range(MinHeight, MaxHeight), 0);
        }
    }
}
