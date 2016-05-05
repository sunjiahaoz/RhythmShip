using UnityEngine;
using System.Collections;
using sunjiahaoz.SteerTrack;

public enum RandomSteerDirType
{
    Around360,
    BetweenVector,
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
            default:
                break;
        }
        
    }
}
