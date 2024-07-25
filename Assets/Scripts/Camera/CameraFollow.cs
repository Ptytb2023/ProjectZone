using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target; 
        [SerializeField] private float _smoothSpeed = 50f; 
        [SerializeField] private Vector3 _offset; 

        private void LateUpdate()
        {
            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}
