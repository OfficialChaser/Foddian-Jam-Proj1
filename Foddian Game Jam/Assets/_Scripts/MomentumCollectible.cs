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

    [SerializeField] private float lifespan;
    [SerializeField] private float regenerationTime;
    private float currentRegenerationTime;

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

    }


}
