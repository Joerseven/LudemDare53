using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Weapon: IItemAbility
    {
        public void OnAllyUse(AllyComponent receiver)
        {
            
            receiver.ReceiveItem();
            if (receiver.equipped is not MeleeComponent) receiver.AddMeleeAttack();

            if (!receiver.frozen)
            {
                return;
            }
            
            receiver.Unfreeze();
            
        }
    }
}
