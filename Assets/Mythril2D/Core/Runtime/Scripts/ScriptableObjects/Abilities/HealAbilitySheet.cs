using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Abilities + nameof(HealAbilitySheet))]
    public class HealAbilitySheet : AbilitySheet
    {
        [Header("治疗能力设置")] [LabelText("治疗量")] [SerializeField] [Min(0)]
        private int m_healAmount = 1;

        public int healAmount => m_healAmount;

        public override void GenerateAdditionalDescriptionLines(List<AbilityDescriptionLine> lines)
        {
            lines.Add(new AbilityDescriptionLine
            {
                header = "治疗",
                content = m_healAmount.ToString()
            });
        }
    }
}