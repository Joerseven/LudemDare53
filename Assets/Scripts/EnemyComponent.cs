using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour, IDamageable
{
    public string enemyType;
    private LevelManager levelManager;
    private SpriteRenderer sprite;

    [SerializeField] private int health;
    void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        sprite.sortingOrder = (int)(transform.position.y*-100);
    }

    public Vector3 GetEngagePoint()
    {
        foreach (Transform trans in transform)
        {
            if (trans.CompareTag("EngagePoint"))
            {
                return trans.transform.position;
            }
        }

        throw new Exception();
    }
    
    public void Damage(int damage)
    {
        health -= damage;
        
        GetComponent<DamageFlashComponent>().DamageFlash();
        
        if (health <= 0)
        {
            levelManager.RemoveEnemy(gameObject);
        }
    }
}
