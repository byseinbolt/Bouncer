using UnityEngine;

namespace Resources.Scripts
{
    public class PlatformBorderTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider otherCollider)
        {
            var player = otherCollider.gameObject.GetComponent<PlayerMovementController>();
            
            if (player)
            {
                player.MoveToStartPoint();
            }
        }
    }
}