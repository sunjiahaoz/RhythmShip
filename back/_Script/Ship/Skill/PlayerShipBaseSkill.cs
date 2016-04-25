using UnityEngine;
using System.Collections;
using sunjiahaoz;
public class PlayerShipBaseSkill : MonoBehaviour {
    PlayerShip _ship;
    public PlayerShip ship
    {
        get { return _ship; }
    }
    public virtual void InitSkill(PlayerShip ship)
    {
        _ship = ship;
    }
}
