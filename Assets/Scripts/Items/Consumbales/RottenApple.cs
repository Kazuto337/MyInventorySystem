using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RottenApple : ConsumableItem
{
    public override void Use()
    {
        base.Use();
        GameManager.Instance.Player.OnDamageReceived.Invoke(value);
    }
}
