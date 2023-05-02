using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
   public interface IItemAbility
   {
      public abstract void OnAllyUse(AllyComponent receiver);
   }
}