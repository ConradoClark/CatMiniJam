using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using UnityEngine;

public class Perk : BaseGameObject
{
    [field:SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public string FlavorText { get; private set; }

    [field: SerializeField]
    public Sprite IconSprite { get; private set; }

    [field: SerializeField]
    public GameObject ActivatedObject { get; private set; }

    [field: SerializeField]
    public Perk[] Requires { get; private set; }

    [field: SerializeField]
    public Perk[] Blocks { get; private set; }
    public bool Activated { get; set; }
    public bool Blocked { get; set; }

    public void Activate()
    {
        ActivatedObject.SetActive(true);
        Activated = true;
        foreach (var perk in Blocks)
        {
            perk.Blocked = true;
        }
    }


}
