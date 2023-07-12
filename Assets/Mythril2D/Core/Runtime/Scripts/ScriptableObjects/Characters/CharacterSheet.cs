using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public abstract class CharacterSheet : ScriptableObject, INameable
    {
        [Header("常规")] [LabelText("道德倾向")] [SerializeField]
        private EAlignment m_alignment = EAlignment.Default;

        [LabelText("显示名")] [SerializeField] private string m_displayName = string.Empty;
        [LabelText("技能列表")] [SerializeField] private SerializableDictionary<AbilitySheet, int> m_abilitiesPerLevel;

        [Header("音频")] [LabelText("命中音效")] [SerializeField]
        private AudioClipResolver m_hitAudio;

        [LabelText("死亡音效")] [SerializeField] private AudioClipResolver m_deathAudio;

        public EAlignment alignment => m_alignment;
        public string displayName => DisplayNameUtils.GetNameOrDefault(this, m_displayName);
        public AudioClipResolver hitAudio => m_hitAudio;
        public AudioClipResolver deathAudio => m_deathAudio;

        public CharacterSheet(EAlignment alignment)
        {
            m_alignment = alignment;
        }

        public IEnumerable<AbilitySheet> GetAvailableAbilitiesAtLevel(int level)
        {
            return m_abilitiesPerLevel.Where((keyValuePair) => keyValuePair.Value <= level)
                .Select((keyValuePair) => keyValuePair.Key);
        }

        public IEnumerable<AbilitySheet> GetAbilitiesUnlockedAtLevel(int level)
        {
            return m_abilitiesPerLevel.Where((keyValuePair) => keyValuePair.Value == level)
                .Select((keyValuePair) => keyValuePair.Key);
        }
    }
}