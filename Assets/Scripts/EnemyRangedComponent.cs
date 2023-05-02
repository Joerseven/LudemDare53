using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedComponent : MonoBehaviour
{
    private LevelManager levelManager;

    private PlayerComponent player;
    // Start is called before the first frame update
    [SerializeField] private float attackTimer;
    [SerializeField] private GameObject rangedAttackPrefab;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float attackDelay;
    private Animator animator;
    public float attackTime;
    private static readonly int Ranged = Animator.StringToHash("Ranged");

    void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();
        player = FindObjectOfType<PlayerComponent>();
        animator = GetComponent<Animator>();
        attackTimer = attackTime;
    }
    
    int GetAllyCount()
    {
        return levelManager.allies.Count;
    }

    AllyComponent GetNearestAlly()
    {
        if (GetAllyCount() == 0)
        {
            return null;
        }
        
        var nearestAlly = levelManager.allies[0];
        var nearestDistance = Vector2.Distance(transform.position, nearestAlly.transform.position);
        foreach (var ally in levelManager.allies)
        {
            var distance = Vector2.Distance(transform.position, ally.transform.position);
            if (distance < nearestDistance && ally.frozen == false)
            {
                nearestAlly = ally;
                nearestDistance = distance;
            }
        }

        return nearestAlly;
    }

    Transform CheckValidTarget()
    {
        if (GetAllyCount() <= 0)
            if ((player.transform.position - transform.position).sqrMagnitude < attackRange * attackRange)
            {
                return player.transform;
            }

        Transform nearest;
        
        var ally = GetNearestAlly();
        
        var allyDistance = (ally.transform.position - transform.position).sqrMagnitude;
        var playerDistance = (player.transform.position - transform.position).sqrMagnitude;
        
        var near = allyDistance < playerDistance ? allyDistance : playerDistance;

        if (ally.frozen)
        {
            return playerDistance > attackRange * attackRange ? null : player.transform;
        }

        if (playerDistance < allyDistance)
        {
            if (playerDistance > attackRange * attackRange)
            {
                return null;
            }
            
            return player.transform;
        }

        if (allyDistance > attackRange * attackRange)
        {
            return null;
        }
            
        return ally.aimfor.transform;

    }
    
    
    Vector3 GetClosestAlliedUnitPosition()
    {
        var nearestAlly = GetNearestAlly();
        if ((nearestAlly.transform.position - transform.position).sqrMagnitude < (player.transform.position - transform.position).sqrMagnitude)
        {
            return player.transform.position;
        } 
        return nearestAlly.transform.position;
    }

    void RangedAttackTimer()
    {
        if (attackTimer >= attackTime)
        {
            var target = CheckValidTarget();
            
            if (target)
            {
                targetTransform = target;
                attackTimer = 0;
                animator.SetTrigger(Ranged);
            }
            
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackDelay);
        if (targetTransform)
        {
            rangedAttackPrefab.SetActive(true);
            rangedAttackPrefab.transform.position = targetTransform.position;
        }
        targetTransform = null;
    }

    void SpawnRangedAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        RangedAttackTimer();
    }
    
}
