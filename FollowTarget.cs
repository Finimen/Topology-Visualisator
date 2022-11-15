using UnityEngine;

namespace Assets.Scripts
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Vector3 offest;

        public Transform Target
        {
            set
            {
                if (value)
                {
                    target = value;
                }
            }
            get
            {
                return target;
            }
        }

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