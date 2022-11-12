using Assets.Scripts.Topology;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectFactory : MonoBehaviour
    {
        [SerializeField] private Class classPrefab;
        [SerializeField] private Interface interfacePrefab;
        [SerializeField] private Transform sceneCanvas;

        private ObjectsLibary objectsLibary;

        private Camera cameraMain;

        public void Initialize()
        {
            objectsLibary = new ObjectsLibary();

            objectsLibary.Initialize(100);

            cameraMain = FindObjectOfType<Camera>();
        }

        public void CreateClass()
        {
            Class newClass = Instantiate(classPrefab, sceneCanvas.transform);

            newClass.transform.position = new Vector3(cameraMain.transform.position.x + Random.Range(-5,5),cameraMain.transform.position.y + Random.Range(-5, 5), 10);

            objectsLibary.Add(newClass);
        }

        public void CreateInterface()
        {

        }
    }
}