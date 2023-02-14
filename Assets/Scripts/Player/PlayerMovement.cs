using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private int _maxJumpsCount;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpSpeed;

        private Rigidbody _rigidbody;
        private int _jumpsCount;
        private float _stepTimer;
        private bool _canJump;
        
        public bool OnGround { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.GetComponent<Ground>())
            {
                OnGround = true;
                
                _canJump = true;
                _jumpsCount = 0;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<Ground>())
                OnGround = false;
        }

        public void Move(float directionX, float directionZ)
        {
            Vector3 cameraRelativeMovement = transform.TransformDirection(directionX, 0, directionZ);
            
            Vector3 direction = new Vector3(_movementSpeed * cameraRelativeMovement.x, _rigidbody.velocity.y,
                _movementSpeed * cameraRelativeMovement.z);
            _rigidbody.velocity = direction;
        }

        public void Jump()
        {
            if (_canJump)
            {
                if(_jumpsCount++ < _maxJumpsCount - 1)
                {
                    Vector3 velocity = _rigidbody.velocity;
                    velocity = new Vector3(velocity.x, _jumpSpeed, velocity.z);
                    _rigidbody.velocity = velocity;
                }
                else
                {
                    _canJump = false;
                }
            }
        }

        public void IncreaseMovementSpeed(float value)
        {
            if(value > 0)
                _movementSpeed += value;
        }

        public void IncreaseJumpSpeed(float value)
        {
            if (value > 0)
                _jumpSpeed += value;
        }
    }
}