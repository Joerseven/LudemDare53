using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public FrogUI frog;
    void Start()
    {
        frog = FindObjectOfType<FrogUI>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponentInParent<Collider2D>());
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.TryGetComponent<ItemComponent>(out var obj) && obj.isHeld == false)
        {
            obj.isHeld = true;
            
            GetComponentInParent<HolderComponent>().holding = obj;
            
            frog.ShowItem(obj.itemName);
            other.gameObject.transform.parent = gameObject.transform;
            other.gameObject.transform.localPosition = new Vector3(0,0,0);
            return;
        }
        
        var newObj = GetComponentInParent<HolderComponent>();
        if (newObj)
        {
            var holding = newObj.holding;
            if (other.gameObject.TryGetComponent<AllyComponent>(out var reciever) && holding)
            {
                holding.GiveToAlly(reciever);
                frog.RemoveItem();
                holding = null;
            }
        }
            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
