using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{
    public class Blowpipe: IItemAbility
    {
        public void OnAllyUse(AllyComponent receiver)
        {
            receiver.ReceiveItem();
            
            if (receiver.equipped is not RangedComponent) receiver.AddRangedAttack();
            
            if (receiver.frozen)
            {
                receiver.Unfreeze();
            }
        }
    }
}