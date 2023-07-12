using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Characters + nameof(HeroSheet))]
    public class HeroSheet : CharacterSheet
    {
        [Header("英雄")] [LabelText("基础属性")] public Stats baseStats;

        [LabelText("每级获得属性点")] public int pointsPerLevel = 5;

        [LabelText("经验")] public LevelScaledInteger experience = new LevelScaledInteger();

        public HeroSheet() : base(EAlignment.Good)
        {
        }
    }
}