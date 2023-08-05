using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumCollectible : MonoBehaviour
{
    [SerializeField] private float increaseAmt;
    [SerializeField] private float decreaseAmt;

    // Self References
    private BoxCollider2D bCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float regenerationTime;

    public enum Type 
    {
        Increase,
        Decrease
    }

    public Type type;

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnBecameInvisible()
    {
        switch (type)
        {
            case Type.Increase:
                MomentumManager.Instance.ModifyMomentum(increaseAmt);
                break;
            case Type.Decrease:
                MomentumManager.Instance.ModifyMomentum(-decreaseAmt);
                break;
        }
    }

    public void Collect()
    {
        StartCoroutine(RegenerateCollectible());
    }

    private IEnumerator RegenerateCollectible()
    {
        bCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(regenerationTime);

        bCollider.enabled = true;
        spriteRenderer.enabled = true;
    }

}
