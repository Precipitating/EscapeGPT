using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface HumanInterface
{
    int HP { get; set; }
    bool CanDamage();
    void OnHit(int dmg);
    void EnableSwordDamage(int enabled);



    void Die();



}