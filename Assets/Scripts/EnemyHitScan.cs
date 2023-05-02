using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitScan : MonoBehaviour
{
    private Animator animator;

    private static readonly int Attack = Animator.StringToHash("Attack");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerComponent>(out var player))
        {
            animator.SetTrigger(Attack);
        } else if (other.TryGetComponent<AllyComponent>(out var ally))
        {
            if (ally.TryGetComponent<MeleeComponent>(out var melee))
            {
                if (melee.isSubmerged)
                    return;
            }
            animator.SetTrigger(Attack);
        }
    }
}
