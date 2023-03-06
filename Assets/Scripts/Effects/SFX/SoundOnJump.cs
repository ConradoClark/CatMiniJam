using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Objects;
using UnityEngine;

public class SoundOnJump : BaseGameObject
{
    [field:SerializeField]
    public AudioSource AudioSource { get; private set; }

    [field: SerializeField]
    public LichtPlatformerJumpController JumpController { get; private set; }

    [field: SerializeField]
    public AudioClip JumpSound { get; private set; }
    [field: SerializeField]
    public AudioClip BounceSound { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        JumpController.OnJumpStart += JumpController_OnJumpStart;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        JumpController.OnJumpStart -= JumpController_OnJumpStart;
    }

    private void JumpController_OnJumpStart(LichtPlatformerJumpController.LichtPlatformerJumpEventArgs obj)
    {
        AudioSource.PlayOneShot(obj.CustomParams is { Identifier: "Bounce" } ? BounceSound : JumpSound);
    }
}
