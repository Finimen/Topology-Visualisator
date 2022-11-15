using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Transform scene;

        [SerializeField] private SceneScaler sceneScaler;
        [SerializeField] private SceneMover sceneMover;

        private void Start()
        {
            sceneMover.Initialize(scene);
            sceneScaler.Initialize(scene);
        }

        private void Update()
        {
            sceneMover.Update();
            sceneScaler.Update();
        }
    }
}