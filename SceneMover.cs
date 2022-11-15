using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class SceneMover : MonoBehaviour
    {
        [SerializeField] private Transform scene;

        [SerializeField] private float coeff;

        private Vector3 newPosition;

        private void Start()
        {
            newPosition = scene.position;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                newPosition = new Vector3(newPosition.x - coeff, newPosition.y, newPosition.z);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                newPosition = new Vector3(newPosition.x + coeff, newPosition.y, newPosition.z);
            }

            if (Input.GetKey(KeyCode.S))
            {
                newPosition = new Vector3(newPosition.x, newPosition.y + coeff, newPosition.z);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                newPosition = new Vector3(newPosition.x, newPosition.y - coeff, newPosition.z);
            }

            scene.position = Vector3.Lerp(scene.position, newPosition, .02f);
        }
    }
}