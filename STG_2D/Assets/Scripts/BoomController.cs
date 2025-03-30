using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BoomController : ItemController
{
    Player player;

    protected override void ItemGain()
    {
        base.ItemGain();

        player = base.player.GetComponent<Player>();
        if(player.Boom < 3)
        {
            player.Boom++;
            UIManager.Instance.BoomCheck(player.Boom);
        }
        if(player.Boom >=3)
        {
            UIManager.Instance.ScoreAdd(base.score);
        }
    }

}
