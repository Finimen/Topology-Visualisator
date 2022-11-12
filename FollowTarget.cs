using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform Target;

        private Vector3 offest;

        private void Start()
        {
            offest = transform.position - Target.position;
        }

        private void Update()
        {
            transform.position = Target.position + offest;
        }
    }
}