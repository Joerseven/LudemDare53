using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Unfreeze : IItemAbility
    {
        public void OnAllyUse(AllyComponent receiver)
        {
            receiver.Unfreeze();
            receiver.Heal(5);
        }
    }
}
