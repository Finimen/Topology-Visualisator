using Assets.Scripts.Topology;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class FastObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject topologyObject;

        [SerializeField] private int spawnCount;

        [SerializeField] private Transform canvasTransform;

        private void Start()
        {
            for(int i = 0; i < spawnCount; i++)
            {
                Instantiate(topologyObject, canvasTransform);
            }
        }
    }
}