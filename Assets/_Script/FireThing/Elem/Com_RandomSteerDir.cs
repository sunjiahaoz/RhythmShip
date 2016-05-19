using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;

public enum RandomSteerDirType
{
    Around360,
    BetweenVector,
    Direction,
    HDirection,
    VDirection,
}
public class Com_RandomSteerDir : MonoBehaviour {
    public RandomSteerDirType _RandomType = RandomSteerDirType.Around360;

    [Header("BetweenVector时使用")]
    public Vector2 _vecA;
    public Vector2 _vecB;

    SteeringDirLine _dir;
    void Awake()
    {
        _dir = GetComponent<SteeringDirLine>();        
    }

    public void SetRandomDir()
    {
        switch (_RandomType)
        {
            case RandomSteerDirType.Around360:
                _dir._vec2DirOff = RUL.RulVec.RandUnitVector2();
                break;
            case RandomSteerDirType.BetweenVector:
                _dir._vec2DirOff = RUL.RulVec.RandVector2Between(_vecA, _vecB);
                break;
            case RandomSteerDirType.Direction:
                _dir._vec2DirOff = RUL.RulVec.RandDirection2();
                break;
            case RandomSteerDirType.HDirection:
                _dir._vec2DirOff = RUL.Rul.RandElement(new Vector2(1, 0), new Vector2(-1, 0));
                break;
            case RandomSteerDirType.VDirection:
                _dir._vec2DirOff = RUL.Rul.RandElement(new Vector2(0, 1), new Vector2(0, -1));
                break;
            default:
                break;
        }
        
    }
}
