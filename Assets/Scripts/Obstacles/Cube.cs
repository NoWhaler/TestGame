using UnityEngine;

namespace TestGame.Obstacles
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Cube : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; } = true;
        
    }
}