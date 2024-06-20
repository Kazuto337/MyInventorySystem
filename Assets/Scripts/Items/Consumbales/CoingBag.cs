using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoingBag : ConsumableItem
{
    public override void Use()
    {
        base.Use();
        GameManager.Instance.Player.OnCoinsAdded.Invoke(value);
    }
}
