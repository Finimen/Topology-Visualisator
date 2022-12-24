using Assets.Scripts;
using Assets.Scripts.InputSystem;
using Assets.Scripts.ReturnSystem;
using Assets.Scripts.SceneControls;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

public class Initializer : MonoBehaviour
{
    [SerializeField] private ObjectFactory objectFactory;

    [SerializeField] private FastActions fastAction;

    [Inject] private Camera cameraMain;

    [Inject] private InputServise inputServise;

    [Inject] private CanvasesData canvasesData;

    private void Awake()
    {
        objectFactory.Initialize(cameraMain);

        fastAction.Enable(inputServise, canvasesData.TopologyObjets);

    }
    private void OnDisable()
    {
        fastAction.Disable();
    }

    private void OnApplicationQuit()
    {
        Undo.ApplicationExit();
    }
}