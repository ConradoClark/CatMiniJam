using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSnowCrystalsOnJump : BaseGameObject
{
    [field: SerializeField]
    public Vector3 Offset { get; private set; }

    [field:SerializeField]
    public LichtPlatformerJumpController JumpController { get; private set; }

    [field: SerializeField]
    public ScriptPrefab CrystalPrefab { get; private set; }

    [field: SerializeField]
    public float AuraMultiplier { get; private set; }

    [field: SerializeField]
    public int InitialAmount { get; private set; }

    private Aura _aura;

    protected override void OnAwake()
    {
        base.OnAwake();
        _aura = _aura.FromScene();
    }

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
        if (obj.CustomParams == null || obj.CustomParams.Identifier == "Bounce") return;

        var amount = InitialAmount + Mathf.RoundToInt(_aura.Radius * AuraMultiplier);

        Vector2 objectDirection = Quaternion.Euler(0, 0, Random.Range(-90f, 90)) * Vector2.up;

        var angleBetweenObjects = Mathf.Abs(Mathf.Atan2(objectDirection.y, objectDirection.x) - Mathf.Atan2(Vector2.up.y, Vector2.up.x)) * Mathf.Rad2Deg;
        var angleOffset = -angleBetweenObjects / (amount - 1);

        var angleSwitch = Random.Range(0, 2) == 0 ? 1 : -1;

        var directions = Enumerable.Range(1, amount)
            .Select(i =>
                amount == 1
                    ? Vector3.up
                    : Quaternion.Euler(0, 0, angleOffset + i * angleBetweenObjects / (amount - 1)) * Vector2.up);

        foreach (var direction in directions)
        {
            if (!CrystalPrefab.TrySpawnEffect(transform.position + Offset, out var crystal)) continue;
            crystal.CustomProps["DirX"] = direction.x * angleSwitch;
            crystal.CustomProps["DirY"] = direction.y;
        }
    }
}
