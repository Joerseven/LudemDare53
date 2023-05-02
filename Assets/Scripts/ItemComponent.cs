using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Serialization;
// ReSharper disable All

public class ItemComponent : MonoBehaviour
{
    public bool isHeld = false;
    // Start is called before the first frame update
    public string itemName;

    IItemAbility _ability;
    void Start()
    {
        _ability = itemName switch
        {
            "Unfreeze" => new Unfreeze(),
            "Weapon" => new Weapon(),
            "Blowpipe" => new Blowpipe(),
            _ => _ability
        };
    }

    public void GiveToAlly(AllyComponent ally)
    {
        _ability.OnAllyUse(ally);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
