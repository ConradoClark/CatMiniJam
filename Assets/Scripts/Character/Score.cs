using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using UnityEngine;

public class Score : BaseGameObject
{
    [field:SerializeField]
    public CounterStat ScoreStat { get; private set; }

    public void GiveScore(int score)
    {
        ScoreStat.Value += score;
    }
}
