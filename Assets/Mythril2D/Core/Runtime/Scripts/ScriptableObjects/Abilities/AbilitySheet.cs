using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Abilities + nameof(AbilitySheet))]
    public class AbilitySheet : ScriptableObject, INameable
    {
        public struct AbilityDescriptionLine
        {
            public string header;
            public string content;
        }

        [Header("UI设置")] [SerializeField, LabelText("图标")]
        private Sprite m_icon = null;

        [SerializeField, LabelText("显示名称")] private string m_displayName = string.Empty;
        [SerializeField, LabelText("描述")] private string m_description = string.Empty;

        [Header("关联")] [SerializeField, LabelText("预制物")]
        private GameObject m_prefab = null;

        [Header("常用能力设置")] [SerializeField, LabelText("魔法消耗")]
        private int m_manaCost = 0;

        [SerializeField, LabelText("是否可以中断")] private bool m_canInterupt = false;

        [SerializeField, LabelText("施法期间禁止的动作")]
        private EActionFlags m_disabledActionsWhileCasting = EActionFlags.All;

        [Header("音频")] [SerializeField, LabelText("发动攻击音频")]
        private AudioClipResolver m_fireAudio;

        public Sprite icon => m_icon;
        public string description => m_description;
        public string displayName => DisplayNameUtils.GetNameOrDefault(this, m_displayName);
        public GameObject prefab => m_prefab;
        public bool canInterupt => m_canInterupt;
        public int manaCost => m_manaCost;
        public EActionFlags disabledActionsWhileCasting => m_disabledActionsWhileCasting;
        public AudioClipResolver fireAudio => m_fireAudio;

        public virtual void GenerateAdditionalDescriptionLines(List<AbilityDescriptionLine> lines)
        {
            lines.Add(new AbilityDescriptionLine
            {
                header = "Mana Cost",
                content = m_manaCost.ToString()
            });
        }
    }
}