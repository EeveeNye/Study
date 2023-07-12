using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [System.Flags]
    public enum EOptionalCharacterStatistics
    {
        [LabelText("无")] None = 0,
        [LabelText("法力值")] Mana = 1 << 0,
        [LabelText("魔法攻击")] MagicalAttack = 1 << 1,
        [LabelText("魔法防御")] MagicalDefense = 1 << 2,
        [LabelText("敏捷度")] Agility = 1 << 3,
        [LabelText("幸运值")] Luck = 1 << 4,
    }

    public enum EGameTerm
    {
        [LabelText("货币")] Currency,
        [LabelText("等级")] Level,
        [LabelText("经验值")] Experience
    }

    [System.Serializable]
    public struct StatSettings
    {
        [LabelText("名称")] public string name;
        [LabelText("缩写")] public string shortened;
        [LabelText("描述")] public string description;
        [LabelText("图标")] public Sprite icon;
        [LabelText("隐藏")] public bool hide;
    }


    [System.Serializable]
    public struct TermDefinition
    {
        [LabelText("完整名称")] public string fullName;
        [LabelText("简称")] public string shortName;
        [LabelText("描述")] public string description;
        [LabelText("图标")] public Sprite icon;
    }

    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Game + nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        [Header("一般设置")] [LabelText("游戏场景")] public string gameplayScene = "Gameplay";
        [LabelText("主菜单场景")] public string mainMenuSceneName = "Main Menu";
        [LabelText("交互层")] public string interactionLayer = "Interaction";
        [LabelText("投射物层")] public string projectileLayer = "Projectile";
        [LabelText("碰撞框层")] public string hitboxLayer = "Hitbox";
        [LabelText("投射物忽略的层")] public string[] layersIgnoredByProjectiles;
        [LabelText("碰撞联系过滤器")] public ContactFilter2D collisionContactFilter;

        [Header("游戏性设置")] [LabelText("可以进行暴击")]
        public bool canCriticalHit = true;

        [LabelText("可以未命中")] public bool canMissHit = true;

        [Header("游戏术语")] [SerializeField] [LabelText("游戏术语定义")]
        private SerializableDictionary<string, TermDefinition> m_gameTerms =
            new SerializableDictionary<string, TermDefinition>();

        [SerializeField] [LabelText("状态术语绑定")]
        private SerializableDictionary<EStat, string> m_statTermsBinding = new SerializableDictionary<EStat, string>();

        private TermDefinition m_defaultTermDefinition = new TermDefinition
        {
            fullName = "[INVALID_FULLNAME]",
            shortName = "[INVALID_SHORTNAME]",
            description = "[INVALID_DESCRIPTION]",
            icon = null
        };

        public TermDefinition GetTermDefinition(string termID)
        {
            if (m_gameTerms.ContainsKey(termID))
            {
                return m_gameTerms[termID];
            }

            return m_defaultTermDefinition;
        }

        public TermDefinition GetTermDefinition(EStat stat)
        {
            if (m_statTermsBinding.ContainsKey(stat))
            {
                return GetTermDefinition(m_statTermsBinding[stat]);
            }

            return m_defaultTermDefinition;
        }
    }
}