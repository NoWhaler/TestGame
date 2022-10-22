using System;
using UnityEngine;
using TestGame.Character;

namespace TestGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        private PlayerMovement _playerMovement;
        private Vector3 _velocity;

        private void Awake()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        private void Update()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            transform.position = Vector3.SmoothDamp(transform.position, _playerMovement.transform.position + _offset, ref _velocity,
                _playerMovement.PlayerMovespeed * Time.deltaTime);
        }
    }
}

