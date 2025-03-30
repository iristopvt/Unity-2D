using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : ItemController
{
    Player player;

    protected override void ItemGain()
    {
        player = base.player.GetComponent<Player>();
        if(player.Boom < 4)
        {
            player.Boom++;
            UIManager.Instance.BoomCheck(player.Boom);
        }
    }

}
