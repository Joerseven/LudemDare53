using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderComponent : MonoBehaviour
{

    public ItemComponent holding;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        // if (collider.gameObject.name == "TongueLash")
        // {
        //     return;
        // }
        //
        // print("Collided with " + collider.gameObject.name);
        //
        // if (collider.gameObject.TryGetComponent<ItemComponent>(out var obj) && obj.isHeld == false)
        // {
        //     obj.isHeld = true;
        //     holding = obj;
        //     collider.gameObject.transform.parent = gameObject.transform;
        //     return;
        // }
    }
    
    private void OnCollisionEnter2D(Collision2D collider)
    {
        // if (collider.gameObject.TryGetComponent<AllyComponent>(out var reciever) && holding)
        // {
        //     holding.GiveToAlly(reciever);
        //     
        //     holding = null;
        // }
    }
}
