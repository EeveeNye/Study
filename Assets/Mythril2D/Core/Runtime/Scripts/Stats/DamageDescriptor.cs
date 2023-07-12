using System;
using Sirenix.OdinInspector;

namespace Gyvr.Mythril2D
{
    [LabelText("伤害来源")]
    public enum EDamageSource
    {
        [LabelText("角色")] Character,
        [LabelText("未知")] Unknown
    }

    [Flags]
    [LabelText("伤害标记")]
    public enum EDamageFlag
    {
        [LabelText("默认")] Default = None,
        [LabelText("无")] None = 0,
        [LabelText("重击")] Critical = 1 << 0,
        [LabelText("未命中")] Miss = 1 << 1,
    }

    [LabelText("伤害类型")]
    public enum EDamageType
    {
        [LabelText("无")] None,
        [LabelText("物理")] Physical,
        [LabelText("魔法")] Magical
    }

    [System.Serializable]
    public struct DamageDescriptor
    {
        [LabelText("数值伤害")] public int flatDamages;

        [LabelText("比例伤害")] public int scaledDamages;

        [LabelText("比例")] public float scale;

        [LabelText("伤害类型")] public EDamageType type;
    }


    /// <summary>
    /// 经过攻击/重击计算后的攻击者输出伤害
    /// </summary>
    public struct DamageOutputDescriptor
    {
        [LabelText("伤害来源")] public EDamageSource source;

        [LabelText("攻击者")] public object attacker;

        [LabelText("伤害")] public int damage;

        [LabelText("伤害类型")] public EDamageType type;

        [LabelText("伤害标记")] public EDamageFlag flags;
    }

    /// <summary>
    /// 目标经过减免（防御/未命中）后接收的伤害
    /// </summary>
    public struct DamageInputDescriptor
    {
        [LabelText("伤害")] public int damage;

        [LabelText("伤害标记")] public EDamageFlag flags;
    }
}