using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumCollectible : MonoBehaviour
{
    [SerializeField] private float changeAmt;

    // Self References
    private BoxCollider2D bCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float regenerationTime;

    public enum Type 
    {
        Move,
        Jump
    }

    public Type type;

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Collect()
    {
        switch (type)
        {
            case Type.Move:
                MomentumManager.Instance.ModifyHorizontalMomentum(changeAmt);
                break;
            case Type.Jump:
                MomentumManager.Instance.ModifyJumpForce(changeAmt);
                break;
        }

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
