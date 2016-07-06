using UnityEngine;
using System.Collections.Generic;
using System;

public enum SKillTargetType
{
    Target,
    Position,
    Direction
}

[System.Serializable]
public enum EffectType
{
    None,
    Damage,
}

[System.Serializable]
public struct EffectConfig
{

    public EffectType effectType;
    public int param;
}

public interface IEffect
{
    void cast(GameObject target);
    void reverse();

}

public interface IEffectContainer
{
    List<IEffect> effects { get; set; }
}

