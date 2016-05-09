using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using sunjiahaoz;

public enum AnimIndex
{
    DestroyEffect,
    FireShootEffect,
    AppearEffect,
    Stage1,
}
[System.Serializable]
public class AnimData
{
    public AnimData(AnimIndex index, string strPath, string strName)
    {
        _index = index;
        _strFolderPath = strPath;
        _strPrefabName = strName;
    }

    public AnimIndex _index = AnimIndex.DestroyEffect;  // 标记
    public string _strFolderPath = string.Empty;            // 所在文件夹，相对于Resources
    public string _strPrefabName = string.Empty;            // prefab名称
}

[System.Serializable]
public class EffectParam
{
    public AnimIndex _animType = AnimIndex.FireShootEffect;
    public string _strName;
    public Vector3 _pos = Vector3.zero;
    public Vector3 _scale = Vector3.one;
    public Vector3 _rotate = Vector3.zero;
    public Transform _trBind = null;
    public Color _color = Color.white;
    public string _strAudioId;

    public ComGetColor _comGetColor;
}

public class ShotEffect
{
    #region _Instance_
    public static ShotEffect Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new ShotEffect();
            }
            return _Instance;
        }
    }
    private static ShotEffect _Instance;    
    #endregion

    Dictionary<AnimIndex, AnimData> _dictData = new Dictionary<AnimIndex, AnimData>();

    public void Init()
    {
        _dictData.Add(AnimIndex.DestroyEffect, new AnimData(AnimIndex.DestroyEffect, "Anim", "EffectExplosion"));
        _dictData.Add(AnimIndex.FireShootEffect, new AnimData(AnimIndex.FireShootEffect, "Anim", "EffectShoot"));
        _dictData.Add(AnimIndex.AppearEffect, new AnimData(AnimIndex.AppearEffect, "Anim", "EffectAppear"));
        _dictData.Add(AnimIndex.Stage1, new AnimData(AnimIndex.Stage1, "Anim", "Stage1"));
    }

    public void ShotDestroy(EffectParam param, System.Action actionCompete = null)
    {        
        ShotNormal(AnimIndex.DestroyEffect, param, actionCompete);
    }

    public void ShotShootEffect(EffectParam param, System.Action actionCompete = null)
    {
        ShotNormal(AnimIndex.FireShootEffect, param, actionCompete);        
    }

    public void ShotAppearEffect(EffectParam param, System.Action actionCompete = null)
    {
        ShotNormal(AnimIndex.AppearEffect, param, actionCompete);
    }

    public void Shot(EffectParam param, System.Action actionComplete = null)
    {
        ShotNormal(param._animType, param, actionComplete);
    }

    void ShotNormal(AnimIndex index,
        EffectParam param,
        System.Action actionCompete = null)
    {
        if (param._comGetColor != null)
        {
            param._color = param._comGetColor.GetColor();
        }

        AnimData data = _dictData[index];
        OneShotEffectMgr._Instance.OneShotParticleEffect(ToolsUseful.Hash(
            "name", data._strPrefabName,
            "folder", data._strFolderPath,
            "scale", param._scale,
            "rotation", param._rotate,
            "bindPoint", param._trBind,
            "position", param._pos), (go) => 
            {                
                //TagLog.Log(LogIndex.Effect, "Complete");
                if (actionCompete != null)
                {
                    actionCompete();
                }
            }, (go) =>
            {
                tk2dSpriteAnimator tor = go.GetComponent<tk2dSpriteAnimator>();
                if (tor == null)
                {
                    TagLog.LogError(LogIndex.Effect, "找不到tk2dSpriteAnimator！！！");
                    return;
                }
                tk2dSpriteAnimationClip clip = tor.GetClipByName(param._strName);
                if (clip == null)
                {
                    TagLog.LogError(LogIndex.Effect, "没有找到动画：" + param._strName, tor);
                    return;
                }

                tor.Sprite.color = param._color;
                tor.StopAndResetFrame();
                
                AutoDestroy ad = go.GetComponent<AutoDestroy>();
                if (ad == null)
                {
                    TagLog.LogError(LogIndex.Effect, "没有找到AutoDestroy组件！", tor);
                    return;
                }                
                ad.m_period = (clip.frames.Length / clip.fps);
                
                tor.Play(param._strName);
                
                if (param._strAudioId.Length > 0)
                {
                    AudioController.Play(param._strAudioId);
                }
            });
    }
}
