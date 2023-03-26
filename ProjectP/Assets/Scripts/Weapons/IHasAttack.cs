using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasAttack
{
    public void Attack();

    public bool AttackButtonPressed();

    public void EnableAttack();

    public void DisableAttack();
}
