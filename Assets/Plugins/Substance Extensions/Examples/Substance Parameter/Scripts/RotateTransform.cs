using UnityEngine;

namespace SubstanceExtensions.Examples
{
    public class RotateTransform : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Rotations per second.")]
        private float rotationSpeed = 0.1f;

        [HideInInspector]
        private Transform _transform;

        new public Transform transform
        {
            get { return _transform ? _transform : _transform = base.transform; }
        }


        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * 360f * Time.deltaTime);
        }
    }
}