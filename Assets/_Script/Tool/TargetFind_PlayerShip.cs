using UnityEngine;
using System.Collections;
using sunjiahaoz;

public class TargetFind_PlayerShip : TargetFindLogicBase {
    public override GameObject[] Find(TargetFindLogicParams data = null)
    {
        GameObject[] go = new GameObject[1];

        if (GamingData.Instance.gameBattleManager.playerShip == null)
        {
            return null;
        }
        go[0] = GamingData.Instance.gameBattleManager.playerShip.gameObject;
        return go;
    }
}
