using UnityEngine;

namespace Spawners
{
    public class MarkerSpawn : MonoBehaviour
    {
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private Color _color = Color.red;

        [SerializeField] private bool _isVisible = true;

        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            if (!_isVisible)
                return;

            Color startColot = Gizmos.color;

            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);

            Gizmos.color = startColot;
        }
    }
}
