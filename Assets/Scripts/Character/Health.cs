using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using UnityEngine;

public class Health : BaseGameObject
{
    [field:SerializeField]
    public CounterStat HP { get; private set; }
}
