using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : ItemController
{
    Player player;

    protected override void ItemGain()
    {
        base.ItemGain();

        player = base.player.GetComponent<Player>();

        if(player.Damage < 3)
        {
            player.Damage++;
        }
        if(player.Damage >=3)
        {
            UIManager.Instance.ScoreAdd(base.score);
        }

    }
}
