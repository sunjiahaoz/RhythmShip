using UnityEngine;
using System.Collections;

public class FrameSelWeapon : CommonBaseFrame
{
    public Elem_ShootSelect _shootSelect;
    public Elem_SkillSelect _skillSelect;

    public int[] _ShootIDs;
    public int[] _skillIDs;

    // TEST CODE
    void Start()
    {
        OnFrameLoad();
    }

    public override void OnFrameLoad()
    {
        base.OnFrameLoad();
        if (_ShootIDs != null
            && _ShootIDs.Length > 0)
        {
            _shootSelect.InitWithIds(_ShootIDs);
        }
        else
        {
            _shootSelect.Init();
        }

        if (_skillIDs != null
            && _skillIDs.Length > 0)
        {
            _skillSelect.InitWithIds(_skillIDs);
        }
        else
        {
            _skillSelect.Init();
        }
    }
}
