using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<ObjectFactory>().Initialize();
    }
}
