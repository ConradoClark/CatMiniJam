using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Mixins;
using Licht.Unity.Objects;
using Licht.Unity.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RetryAction : UIAction
{
    [field:SerializeField]
    public SpriteRenderer PerkIcon { get; private set; }

    [field: SerializeField]
    public InputActionReference MousePos { get; private set; }
    [field: SerializeField]
    public InputActionReference MouseClick { get; private set; }
    [field: SerializeField]
    public string Scene { get; private set; }

    private ClickableObjectMixin _clickable;

    protected override void OnAwake()
    {
        base.OnAwake();
        _clickable = new ClickableObjectMixinBuilder(this, MousePos, MouseClick)
            .WithCamera(SceneObject<UICamera>.Instance(true).Camera)
            .Build();
    }

    public override IEnumerable<IEnumerable<Action>> DoAction()
    {
        DefaultMachinery.FinalizeWith(() =>
        {
            GameTimer.Multiplier = 1;
            SceneManager.LoadScene(Scene, LoadSceneMode.Single);
        });
        yield break;
    }

    public override void OnInit()
    {
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();

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
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
    }
}
