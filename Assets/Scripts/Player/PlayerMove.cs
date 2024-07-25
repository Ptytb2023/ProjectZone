using Services.Input;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField][Min(0.0f)] private float _speedMove;

        private Rigidbody2D _rigidbody;
        private IInputService _input;

        public Vector2 Direction { get; private set; }

        [Inject]
        public void Construct(IInputService inputService) =>
            _input = inputService;

        private void Start() => _rigidbody = GetComponent<Rigidbody2D>();


        private void FixedUpdate()
        {
            Direction = _input.Axis;

            if (Direction == Vector2.zero)
                return;

            Move(Direction);
        }

        private void Move(Vector2 direction)
        {
            direction *= _speedMove * Time.fixedDeltaTime;

            Vector2 movementVector = _rigidbody.position + direction;

            _rigidbody.MovePosition(movementVector);
        }

        private void OnValidate() =>
            GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
