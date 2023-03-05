using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using TMPro;
using UnityEngine;

public class TextUpdater : BaseGameObject
{
    [field:SerializeField]
    public TMP_Text TextComponent { get; private set; }

    public void UpdateText(string text)
    {
        TextComponent.text = text;
    }
}
