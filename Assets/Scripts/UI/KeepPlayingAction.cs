using System;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Unity.Mixins;
using Licht.Unity.Objects;
using Licht.Unity.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeepPlayingAction : UIAction
{
    [field:SerializeField]
    public SpriteRenderer PerkIcon { get; private set; }

    [field: SerializeField]
    public InputActionReference MousePos { get; private set; }
    [field: SerializeField]
    public InputActionReference MouseClick { get; private set; }
    [field: SerializeField]
    public GameObject GameWinPanel { get; private set; }

    [field: SerializeField]
    public AudioSource GameMusic { get; private set; }
    [field: SerializeField]
    public AudioClip NewSong { get; private set; }

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
        GameTimer.Multiplier = 1;
        GameWinPanel.SetActive(false);

        GameMusic.Stop();
        GameMusic.clip = NewSong;
        GameMusic.time = 0f;
        GameMusic.Play();

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
