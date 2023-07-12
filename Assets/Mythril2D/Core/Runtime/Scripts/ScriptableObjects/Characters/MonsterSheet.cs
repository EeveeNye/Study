using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Characters + nameof(MonsterSheet))]
    public class MonsterSheet : CharacterSheet
    {
        [Header("怪物")] [LabelText("状态")] public LevelScaledStats stats = new LevelScaledStats();

        [Header("奖励")] [LabelText("经验值")] public LevelScaledInteger experience = new LevelScaledInteger();
        [LabelText("金币")] public LevelScaledInteger money = new LevelScaledInteger();
        [LabelText("战利品")] public Loot[] potentialLoot;

        [Header("行为")] [LabelText("死亡事件")] public ActionHandler[] executeOnDeath;

        public MonsterSheet() : base(EAlignment.Evil)
        {
        }
    }
}