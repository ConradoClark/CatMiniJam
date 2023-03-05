using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Impl.Orchestration;
using Licht.Unity.Builders;
using Licht.Unity.Objects;
using UnityEngine;

public class GameFX : BaseGameObject
{
    [field:SerializeField]
    public Color DarkColor { get; private set; }
    [field: SerializeField]
    public Shader Shader { get; private set; }
    [field: SerializeField]
    public Transform FollowTarget { get; private set; }

    [field: SerializeField]
    public float AuraRadius{ get; private set; }

    public void SetRadius(float auraRadius)
    {
        DefaultMachinery.AddUniqueMachine("UpdateAuraRadius", UniqueMachine.UniqueMachineBehaviour.Replace,
            UpdateRadius(auraRadius));
    }

    private IEnumerable<IEnumerable<Action>> UpdateRadius(float auraRadius)
    {
        yield return new LerpBuilder(f => AuraRadius = f, () => AuraRadius)
            .SetTarget(auraRadius)
            .Over(0.5f)
            .Easing(EasingYields.EasingFunction.SineEaseOut)
            .UsingTimer(GameTimer)
            .Build();
    }

    // Start is called before the first frame update
    void Start()
    {
        Shader.SetGlobalColor("_FX_DarkColor", DarkColor);
        Shader.SetGlobalFloat("_FX_Radius", AuraRadius);
        Shader.SetGlobalInteger("_FX_Enabled", 1);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_FX_Center", (Vector2) FollowTarget.position);
        Shader.SetGlobalFloat("_FX_Radius", AuraRadius);
    }
}