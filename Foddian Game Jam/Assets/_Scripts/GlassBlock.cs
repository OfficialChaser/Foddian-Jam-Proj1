using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBlock : MonoBehaviour
{
    // Self References
    private BoxCollider2D bCollider;
    public SpriteRenderer spriteRenderer;

    [SerializeField] private float lifespan;
    [SerializeField] private float regenerationTime;
    private float currentRegenerationTime;
	
	//Sprite Management
	[SerializeField] private List<Sprite> sprites;

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GlassBreakAndRegenerateSequence());
        }
    }

    private IEnumerator GlassBreakAndRegenerateSequence()
    {
        float elapsed = 0f;

        while (elapsed < lifespan)
        {
            elapsed += Time.deltaTime;

            if (elapsed > lifespan / 4f)
            {
                spriteRenderer.sprite = sprites[0];
            } 
            else if (elapsed > lifespan / 2f)
            {
                spriteRenderer.sprite = sprites[1];
            }
            else if (elapsed > lifespan / 0.5f)
            {
                spriteRenderer.sprite = sprites[2];
            }
			else
			{
				spriteRenderer.sprite = sprites[3];
			}

            yield return null;
        }

        bCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(regenerationTime);

        bCollider.enabled = true;
        spriteRenderer.sprite = sprites[0];
        spriteRenderer.enabled = true;
    }
}
