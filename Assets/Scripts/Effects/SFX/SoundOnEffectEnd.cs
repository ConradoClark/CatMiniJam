using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Pooling;
using UnityEngine;
public class SoundOnEffectEnd : BaseGameObject
{
    [field: SerializeField]
    public EffectPoolable EffectPoolable { get; private set; }

    [field: SerializeField]
    public AudioClip AudioClip{ get; private set; }

    private SFXAudioSource _audioSource;
    protected override void OnAwake()
    {
        base.OnAwake();
        _audioSource = _audioSource.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EffectPoolable.OnEffectOver += Effect_OnEffectOver;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EffectPoolable.OnEffectOver -= Effect_OnEffectOver;
    }

    private void Effect_OnEffectOver()
    {
        _audioSource.AudioSource.PlayOneShot(AudioClip);
    }
}
