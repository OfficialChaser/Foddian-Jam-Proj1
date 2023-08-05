using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBlock : MonoBehaviour
{
    // Self References
    private BoxCollider2D bCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float lifespan;
    [SerializeField] private float regenerationTime;
    private float currentRegenerationTime;

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().isGrounded)
            {
                StartCoroutine(GlassBreakAndRegenerateSequence());
            }
        }
    }

    private IEnumerator GlassBreakAndRegenerateSequence()
    {
        float elapsed = 0f;

        while (elapsed < lifespan)
        {
            elapsed += Time.deltaTime;

            if (elapsed > 0.75f)
            {
                // Change Sprite
            } 
            else if (elapsed > 0.5f)
            {
                // Change Sprite
            }
            else if (elapsed > 0.25f)
            {
                // Change Sprite
            }

            yield return null;
        }

        bCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(regenerationTime);

        bCollider.enabled = true;
        spriteRenderer.enabled = true;
    }
}
