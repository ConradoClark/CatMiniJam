using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Mixins;
using Licht.Unity.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerkMenuAction : UIAction
{
    public Perk Perk { get; set; }
    [field:SerializeField]
    public SpriteRenderer PerkIcon { get; private set; }

    [field: SerializeField]
    public InputActionReference MousePos { get; private set; }
    [field: SerializeField]
    public InputActionReference MouseClick { get; private set; }

    private PerkNameUpdater _nameUpdater;
    private PerkDescriptionUpdater _descriptionUpdater;
    private PerkFlavorTextUpdater _flavorTextUpdater;
    private ClickableObjectMixin _clickable;

    public bool Chosen { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        _nameUpdater = _nameUpdater.FromScene();
        _descriptionUpdater = _descriptionUpdater.FromScene();
        _flavorTextUpdater = _flavorTextUpdater.FromScene();
        _clickable = new ClickableObjectMixinBuilder(this, MousePos, MouseClick).Build();
    }

    public override IEnumerable<IEnumerable<Action>> DoAction()
    {
        Perk.Activate();
        Chosen = true;
        yield break;
    }

    public override void OnInit()
    {
        
    }

    protected override void OnEnable()
    {
        Chosen = false;
        base.OnEnable();
        PerkIcon.sprite = Perk == null ? null : Perk.IconSprite;

        DefaultMachinery.AddBasicMachine(_clickable.HandleHover(() => MenuContext.Select(this), () => { }));
        _clickable.HandleClick(()=>MenuContext.Click(this));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnSelect(bool manual)
    {
        base.OnSelect(manual);
        _nameUpdater.UpdateText(Perk.Name);
        _descriptionUpdater.UpdateText(Perk.Description);
        _flavorTextUpdater.UpdateText(Perk.FlavorText);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
    }
}
