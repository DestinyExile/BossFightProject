using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : CollectibleBase
{
    protected override void Collect(Player player)
    {
        player.IncreaseTreasure();
    }

}
