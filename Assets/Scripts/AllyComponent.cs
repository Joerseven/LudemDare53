using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public interface IWeaponComponent {}

public interface IDamageable
{
    void Damage(int damage);
}

public class AllyComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 3;
    // Start is called before the first frame update
    public bool frozen = true;
    // private bool rooted = false;
    public MonoBehaviour equipped;

    private LevelManager levelManager;
    private List<EnemyComponent> enemies;
    public Transform aimfor;
    private SpriteRenderer sprite;

    void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();
        enemies = levelManager.enemies;
        sprite = GetComponent<SpriteRenderer>();
        Freeze();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.sortingOrder = (int)(transform.position.y*-100);
    }
    
    public EnemyComponent GetClosestEnemy()
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
    
    public void Unfreeze()
    {
        frozen = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Animator>().speed = 1;
    }

    public void Freeze()
    {
        frozen = true;
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<Animator>().speed = 0;
    }

    public void AddMeleeAttack()
    {
        if (equipped) Destroy(equipped);
        
        var melee = gameObject.AddComponent<MeleeComponent>();
        melee.enemies = levelManager.enemies;
        equipped = melee;
    }

    public void AddRangedAttack()
    {
        if (equipped) Destroy(equipped);
        var ranged = gameObject.AddComponent<RangedComponent>();
        equipped = ranged;
    }
    
    public void Damage(int amount)
    {
        if (frozen) return;
        
        if (TryGetComponent<MeleeComponent>(out var melee))
        {
            if (melee.isSubmerged && amount < 99) return;
        }
        
        health -= amount;
        
        GetComponent<DamageFlashComponent>().DamageFlash();
        
        if (health <= 0)
        {
            levelManager.RemoveAlly(this.gameObject);
        }
    }

    public void SetMoving()
    {
        if (TryGetComponent<MeleeComponent>(out var melee))
        {
            melee.SetSubmerged(true);
        }
    }

    public Transform GetRangedAim()
    {
        return aimfor;
    }

    public void Heal(int amount)
    {
        health += amount;
        print("Healed!");
    }
    
    public void SetNotMoving()
    {
        if (TryGetComponent<MeleeComponent>(out var melee))
        {
            melee.SetSubmerged(false);
        }
    }

    public void DamageClosestEnemy()
    {
        if (TryGetComponent<MeleeComponent>(out var melee))
        {
            melee.HitClosestEnemy();
        }

        if (TryGetComponent<RangedComponent>(out var ranged))
        {
            ranged.HitClosestEnemy();
        }
    }

    public void ReceiveItem()
    {
        Debug.Log("Received item!");
    }
    
}
