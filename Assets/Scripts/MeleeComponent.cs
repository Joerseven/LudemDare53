using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComponent : MonoBehaviour, IWeaponComponent
{

    public List<EnemyComponent> enemies;
    private LevelManager levelManager;

    private EnemyComponent target;
    private AllyComponent allySelf;
    private Animator animator;

    public bool isSubmerged = false;

    private Vector3 direction;
    private Rigidbody2D rb;
    private float speed = 0.8f;

    private static readonly int SwitchToMelee = Animator.StringToHash("SwitchToMelee");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allySelf = GetComponent<AllyComponent>();
        levelManager = GetComponentInParent<LevelManager>();
        animator = GetComponent<Animator>();

        enemies = levelManager.enemies;
        
        animator.SetTrigger(SwitchToMelee);

    }

    bool CheckEnemies()
    {
        return enemies.Count != 0;
    }

    EnemyComponent GetClosestEnemy()
    {
        int closest = 0;
        for (int i=0;i<enemies.Count; i++)
        {
            var e = enemies[i];
            if ((e.transform.position - transform.position).sqrMagnitude < (enemies[closest].transform.position - transform.position).sqrMagnitude)
            {
                closest = i;
            }
        }

        return enemies[closest];
    }

    // Update is called once per frame
    void Update()
    {

        target = GetClosestEnemy();
            
        var position = target.GetEngagePoint();
        var position1 = transform.position;
            
        direction = (position - position1).normalized;
        
        if (Vector3.Distance(position,position1) == 0)
        {
            SwitchToAttack();
        }
        else
        {
            SwitchToMove();
        }

    }

    void SwitchToAttack()
    {
        animator.SetBool("ShouldAttack", true);
        animator.SetBool("ShouldMove", false);
    }
    
    public void HitClosestEnemy()
    {
        target.Damage(1);
    }

    void SwitchToIdle()
    {
        
    }

    void SwitchToMove()
    { 
        animator.SetBool("ShouldMove", true);
        animator.SetBool("ShouldAttack", false);
    }

    public void SetSubmerged(bool movement)
    {
        isSubmerged = movement;
    }

    private void FixedUpdate()
    {
        if (!isSubmerged) return;
        
        var movement = (Vector2)direction * (Time.deltaTime * speed);
        if (movement.magnitude > Vector3.Distance(transform.position, target.GetEngagePoint()))
        {
            rb.MovePosition(target.GetEngagePoint());
        }
        else
        {
            rb.MovePosition((Vector2)transform.position + movement);
        }
    }
    
}
