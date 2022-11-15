using Assets.Scripts;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<ObjectFactory>().Initialize();
    }
}
