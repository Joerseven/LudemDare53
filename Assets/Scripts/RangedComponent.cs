using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedComponent : MonoBehaviour
{
    private LevelManager levelManager;
    private List<EnemyComponent> enemies;
    [SerializeField] private float attackRange = 4;
    [SerializeField] private float attackSpeed = 5;
    private Animator animator;

    private float attackTimer = 0;
    private EnemyComponent target;

    private AllyComponent allyComponent;
    private static readonly int RangedAttack = Animator.StringToHash("RangedAttack");

    // Start is called before the first frame update
    void Start()
    {
        allyComponent = GetComponent<AllyComponent>();
        levelManager = GetComponentInParent<LevelManager>();
        animator = GetComponent<Animator>();
        enemies = levelManager.enemies;
        
        animator.SetTrigger("SwitchToRanged");
    }
    
    

    bool MakeRangedAttack()
    {
        target = allyComponent.GetClosestEnemy();

        if (!((target.transform.position - transform.position).sqrMagnitude < attackRange * attackRange))
        {
            if (levelManager.allies.Count == 1)
            {
                levelManager.GameOver();
            }

            return false;
        }

        animator.SetTrigger(RangedAttack);
        
        return true;

    }

    public bool CheckHasTargets()
    {
        return (target.transform.position - transform.position).sqrMagnitude < attackRange * attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer < attackSpeed)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            if (MakeRangedAttack()) attackTimer = 0;
        }
    }

    public void HitClosestEnemy()
    {
        target.Damage(1);
    }
}
