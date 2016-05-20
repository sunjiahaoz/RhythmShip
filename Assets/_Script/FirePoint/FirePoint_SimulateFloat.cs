/*
FirePoint_SimulateFloat
By: @sunjiahaoz, 2016-5-20

模拟漂浮路径（其实就是折线）的敌人的生成器，主要是提供了计算折线需要的起点与目标点
*/
using UnityEngine;
using System.Collections;

public class FirePoint_SimulateFloat : FirePointRhythm {
    [Header("FirePoint_SimulateFloat")]

    public Transform _trSimulateStartPos;
    public Transform _trSimulateGoalPos;

    protected override void CreateObject(Vector3 pos, System.Action<BaseFireThing> afterCreate = null)
    {
        base.CreateObject(pos, (thing) => 
        {
            EnemySimulateFloat enemy = thing.GetComponent<EnemySimulateFloat>();
            if (enemy != null)
            {
                enemy._trStartPos = _trSimulateStartPos;
                enemy._trGoalPos = _trSimulateGoalPos;
                enemy.GeneratePath();
            }
            if (afterCreate != null)
            {
                afterCreate(thing);
            }
        });
    }
    
}
