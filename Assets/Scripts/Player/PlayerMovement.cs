using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float movingSpeed;
        
        [Range(0, 1)]
        public float rotationSmoothness;
        public float velocitySmoothTime;

        private Animator _animator;
        private readonly int _isMoving = Animator.StringToHash("IsMoving");
        
        private Vector3 _acceleration = Vector3.zero;
        private Vector3 _velocity = Vector3.zero;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(horizontalInput, 0, verticalInput);
            var shouldMove = direction != Vector3.zero;
            
            // Set the proper animation
            _animator.SetBool(_isMoving, shouldMove);
            
            // Rotate
            if (shouldMove)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    1 - rotationSmoothness
                );
            }

            // Move
            _velocity = Vector3.SmoothDamp(_velocity, direction * movingSpeed, ref _acceleration, velocitySmoothTime);
            transform.position += _velocity * Time.deltaTime;
        }
    }
}
