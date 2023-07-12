using System;
using Sirenix.OdinInspector;

namespace Gyvr.Mythril2D
{
    [Serializable]
    public struct Loot
    {
        [LabelText("物品")] public Item item;
        [LabelText("数量")] public int quantity;
        [LabelText("掉落率")] public int dropRate;
        [LabelText("最低怪物等级")] public int minimumMonsterLevel;
        [LabelText("最低玩家等级")] public int minimumPlayerLevel;

        public bool ResolveDrop() => UnityEngine.Random.Range(1, 101) <= dropRate;
    }
}