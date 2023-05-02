using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float damageTimer = 3;
    [SerializeField] private int damageAmount = 1;

     private List<IDamageable> CurrentlyDamaging = new List<IDamageable>();
    private List<IDamageable> ReadyToKill = new List<IDamageable>();
    void Start()
    {
        
    }

    private IEnumerator DamageAfter(IDamageable thing)
    {
        if (CurrentlyDamaging.Contains(thing))
        {
            yield break;
        }
        
        CurrentlyDamaging.Add(thing);
        
        print("First");
        
        yield return new WaitForSeconds(damageTimer);
        
        print("Second");
        
        ReadyToKill.Add(thing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageable)) return;

        if (ReadyToKill.Contains(damageable))
        {
            damageable.Damage(damageAmount);
            CurrentlyDamaging.Remove(damageable);
            ReadyToKill.Remove(damageable);
            return;
        }
            
        if (CurrentlyDamaging.Contains(damageable))
        {
            return;
        }
            
        StartCoroutine(DamageAfter(damageable));
    }
}
