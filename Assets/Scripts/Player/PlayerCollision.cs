using System;
using TestGame.Obstacles;
using TestGame.UI;
using UnityEngine;


namespace TestGame.Character
{
    public class PlayerCollision : MonoBehaviour
    {
        private LevelPoints _levelPoints;
        private const float BaseScoreMultiplier = 1f;

        public event Action OnCollisionDetected;

        private void Awake()
        {
            _levelPoints = FindObjectOfType<LevelPoints>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var cube = collision.rigidbody.GetComponent<Cube>();
            if (cube == null) return;
            collision.rigidbody.useGravity = true;
            _levelPoints.ScoreMultiplier = BaseScoreMultiplier;
            if (cube.IsInteractable)
            {
                OnCollisionDetected?.Invoke();
                cube.IsInteractable = false;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var scoreMultiplier = collision.rigidbody.GetComponent<ScoreMultiplier>();
            if (scoreMultiplier == null) return;
            _levelPoints.ScoreMultiplier += BaseScoreMultiplier;
            scoreMultiplier.IsInteractable = false;
            
        }
    }
}