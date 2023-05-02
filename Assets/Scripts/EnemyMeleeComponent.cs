using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerComponent>(out var player))
        {
            player.Kill();
        }
        
        if (other.TryGetComponent<AllyComponent>(out var ally))
        {
            ally.Damage(1);
        }
    }
}
