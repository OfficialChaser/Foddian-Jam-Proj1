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

    private bool animationInProgress = false;
	
	//Sprite Management
	[SerializeField] private List<Sprite> sprites;

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !animationInProgress)
        {
            Debug.Log("Hit");
            StartCoroutine(GlassBreakAndRegenerateSequence());
        }
    }

    private IEnumerator GlassBreakAndRegenerateSequence()
    {
        animationInProgress = true;
        float elapsed = 0f;

        while (elapsed < lifespan)
        {
            elapsed += Time.deltaTime;

            if (elapsed > lifespan / 2f)
            {
                Debug.Log("3");
                spriteRenderer.sprite = sprites[3];
            }
            else if (elapsed > lifespan / 4f)
            {
                Debug.Log("2");
                spriteRenderer.sprite = sprites[2];
            }
            else
            {
                Debug.Log("1");
                spriteRenderer.sprite = sprites[1];
            }

            yield return null;
        }

        bCollider.enabled = false;
        spriteRenderer.enabled = false;
        Debug.Log("Broken");

        yield return new WaitForSeconds(regenerationTime);

        bCollider.enabled = true;
        spriteRenderer.sprite = sprites[0];
        spriteRenderer.enabled = true;
        animationInProgress = false;
    }
}
