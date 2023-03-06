using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : BaseGameObject
{

    [field:SerializeField]
    public InputActionReference[] Inputs { get; private set; }

    [field: SerializeField]
    public string SceneName { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(HandleInput());
    }

    private IEnumerable<IEnumerable<Action>> HandleInput()
    {
        while (ComponentEnabled)
        {
            if (Inputs.Any(i=>i.action.WasPerformedThisFrame()))
            {
                DefaultMachinery.FinalizeWith(() =>
                {
                    SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                });
                yield break;
            }
            yield return TimeYields.WaitOneFrameX;
        }
    }
}
