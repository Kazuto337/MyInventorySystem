using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pretzel : ConsumableItem
{
    public override void Use()
    {
        base.Use();
        GameManager.Instance.Player.OnHealingReceived.Invoke(value);
    }
}
