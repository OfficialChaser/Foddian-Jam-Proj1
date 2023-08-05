using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.GetComponent<MomentumCollectible>().Collect();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Finish Platform"))
            {
                if (player.isGrounded)
                {
                    player.rb.velocity = Vector2.zero;
                    GameManager.Instance.StartEndSequence();
                }
            }
    }
}
