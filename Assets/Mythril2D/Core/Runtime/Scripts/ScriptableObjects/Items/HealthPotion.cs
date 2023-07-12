using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Items + nameof(HealthPotion))]
    public class HealthPotion : Item
    {
        [Header("影响")] [SerializeField] [LabelText("要恢复的数值")]
        private int m_healthToRestore = 1;

        [Header("音频")] [SerializeField] [LabelText("饮用音频")]
        private AudioClipResolver m_drinkAudio;

        public override void Use(CharacterBase target, EItemLocation location)
        {
            if (target.currentStats[EStat.Health] < target.stats[EStat.Health])
            {
                int previousHealth = target.currentStats[EStat.Health];
                target.Heal(m_healthToRestore);
                int currentHealth = target.currentStats[EStat.Health];
                int diff = currentHealth - previousHealth;

                GameManager.DialogueSystem.Main.PlayNow("You recover {0} <health>", diff);
                GameManager.NotificationSystem.audioPlaybackRequested.Invoke(m_drinkAudio);
                GameManager.InventorySystem.RemoveFromBag(this);
            }
            else
            {
                base.Use(target, location);
            }
        }
    }
}