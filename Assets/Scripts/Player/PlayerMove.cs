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

        [Inject]
        public void Construct(IInputService inputService) =>
            _input = inputService;

        private void Start() => _rigidbody = GetComponent<Rigidbody2D>();


        private void Update()
        {
            Vector2 dircetion = _input.Axis;

            if (dircetion == Vector2.zero)
                return;

            Move(dircetion);
        }

        private void Move(Vector2 direction)
        {
            direction *= _speedMove * Time.deltaTime;

            Vector2 movementVector = _rigidbody.position + direction;

            _rigidbody.MovePosition(movementVector);
        }


        private void OnValidate() =>
            GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
