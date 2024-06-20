using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Brownie : ConsumableItem
{
    public override void Use()
    {
        base.Use();
        GameManager.Instance.Player.OnHealingReceived.Invoke(value);
    }
}
