using Assets.Scripts;
using UnityEngine;
using Zenject;

public class Initializer : MonoBehaviour
{
    [SerializeField] private ObjectFactory objectFactory;

    [Inject] private Camera cameraMain;

    private void Awake()
    {
        objectFactory.Initialize(cameraMain);
    }
}