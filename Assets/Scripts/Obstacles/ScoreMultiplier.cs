using UnityEngine;

namespace TestGame.Obstacles
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class ScoreMultiplier : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; } = true;
    }
}