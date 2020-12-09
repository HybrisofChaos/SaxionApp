using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
   void ApplyDamage(int damage, GameObject source);

   void RegisterToUIEvents();

   void Die();
}
