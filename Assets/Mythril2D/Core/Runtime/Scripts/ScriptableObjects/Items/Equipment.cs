using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public enum EEquipmentType
    {
        [LabelText("武器")] Weapon,
        [LabelText("头部")] Head,
        [LabelText("躯干")] Torso,
        [LabelText("手部")] Hands,
        [LabelText("脚部")] Feet
    }

    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Items + nameof(Equipment))]
    public class Equipment : Item
    {
        [Header("音频")] [SerializeField] [LabelText("装备音频")]
        private AudioClipResolver m_equipAudio;

        [SerializeField] [LabelText("移除装备音频")] private AudioClipResolver m_unequipAudio;

        [Header("装备")] [SerializeField] [LabelText("装备位置")]
        private EEquipmentType m_type;

        [SerializeField] [LabelText("装备属性")] private Stats m_bonusStats;

        public EEquipmentType type => m_type;
        public Stats bonusStats => m_bonusStats;

        public override void Use(CharacterBase user, EItemLocation location)
        {
            if (location == EItemLocation.Bag)
            {
                GameManager.InventorySystem.Equip(this);
                GameManager.NotificationSystem.audioPlaybackRequested.Invoke(m_equipAudio);
            }
            else
            {
                GameManager.InventorySystem.UnEquip(type);
                GameManager.NotificationSystem.audioPlaybackRequested.Invoke(m_unequipAudio);
            }
        }
    }
}