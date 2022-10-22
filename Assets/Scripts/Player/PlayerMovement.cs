using System;
using System.Collections;
using UnityEngine;

namespace TestGame.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Moving values")]
        [SerializeField] private float _playerMovespeed;
        [SerializeField] private Transform _targetPosition;
        [SerializeField] private float _knockBackDuration;
        private const float BoostSpeed = 13f;
        private const float BaseMoveSpeed = 6f;
        private Vector3 _velocity;

        [Header("Touch movement")]
        private Touch _touch;
        private Vector2 _oldTouchPosition;
        private Vector2 _newTouchPosition;

        [Header("Rotation values")]
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _deltaThreshhold;
        private const float RotationCoefficient = 1.5f;
        
        private Rigidbody _rigidbody;
        private PlayerCollision _playerCollision;
        
        public bool IsMoving { get; set; } = true;
        public bool IsSpeedUp { get; set; }
        
        public Func<Vector3, Vector3, bool> OnLevelEnd { get; set; }
        
        public float PlayerMovespeed { 
            get => _playerMovespeed;
        }

        private void Start()
        {
            _playerCollision = GetComponent<PlayerCollision>();
            _rigidbody = GetComponent<Rigidbody>();
            _playerCollision.OnCollisionDetected += HandleOnCollisionDetected;
        }
        
        private void Update()
        {
            EndLevel();
            Rotate();
            Move();
            SpeedUp();
        }

        private void FixedUpdate()
        {
            if (IsMoving)
            {
                MovePlayer(_velocity);
            }
        }

        private void HandleOnCollisionDetected()
        {
            StartCoroutine(KnockBackCo(_knockBackDuration));
            
            IEnumerator KnockBackCo(float knockBackDuration)
            {
                float knockBackTimer = 0;
                
                IsMoving = false;
                while (knockBackTimer < knockBackDuration)
                {
                    MovePlayer(-_velocity);

                    yield return new WaitForFixedUpdate();
                    knockBackTimer += Time.fixedDeltaTime;
                }
                IsMoving = true;
            }
        }

        private void Rotate()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);
                switch (_touch.phase)
                {
                    case TouchPhase.Began:
                        _oldTouchPosition = _touch.position;
                        _newTouchPosition = _touch.position;
                        break;
                    case TouchPhase.Moved:
                        _oldTouchPosition = _newTouchPosition;
                        _newTouchPosition = _touch.position;
                        break;
                }
                float delta = Mathf.Abs(_oldTouchPosition.x - _newTouchPosition.x);
                
                if (delta >= _deltaThreshhold)
                {
                    Vector2 rotationDirection = _oldTouchPosition - _newTouchPosition;
                    if (rotationDirection.x < 0)
                    {
                        RotateLeft();
                    }
                    else  
                    {
                        RotateRight();
                    }
                }
            }
        }

        private void SpeedUp()
        {
            if (Input.touchCount >= 2)
            {
                _playerMovespeed = BoostSpeed;
                IsSpeedUp = true;
            }
            else
            {
                _playerMovespeed = BaseMoveSpeed;
                IsSpeedUp = false;
            }
        }

        private void RotateRight()
        {
            Quaternion rightRotation = Quaternion.Euler(0f, RotationCoefficient * _rotationSpeed, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * rightRotation);
        }

        private void RotateLeft()
        {
            Quaternion leftRotation = Quaternion.Euler(0f, -RotationCoefficient * _rotationSpeed, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * leftRotation);
        }

        private void MovePlayer(Vector3 velocity)
        {
            _rigidbody.MovePosition(transform.position +(velocity * _playerMovespeed * Time.deltaTime));
        }

        private void Move()
        {
            _velocity = new Vector3(0, 0, 1f);
        }

        private void EndLevel()
        {
            OnLevelEnd?.Invoke(transform.position, _targetPosition.position);
        }
    }
}