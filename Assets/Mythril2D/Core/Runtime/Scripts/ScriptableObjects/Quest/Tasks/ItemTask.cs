using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public class ItemTaskProgress : QuestTaskProgress
    {
        public int currentQuantity { get; private set; } = 0;

        private ItemTask m_task => (ItemTask)task;

        public ItemTaskProgress(ItemTask task) : base(task)
        {
        }

        public override void OnProgressTrackingStarted()
        {
            GameManager.NotificationSystem.itemAdded.AddListener(OnItemAdded);
        }

        public override void OnProgressTrackingStopped()
        {
            GameManager.NotificationSystem.itemAdded.RemoveListener(OnItemAdded);
        }

        public override bool IsCompleted()
        {
            return currentQuantity >= m_task.amountToCollect;
        }

        private void UpdateAmount()
        {
            int quantityInInventory = GameManager.InventorySystem.GetItemCount(m_task.item);

            if (quantityInInventory != currentQuantity)
            {
                currentQuantity = GameManager.InventorySystem.GetItemCount(m_task.item);
                UpdateProgression();
            }
        }

        private void OnItemAdded(Item item, int quantity)
        {
            UpdateAmount();
        }
    }

    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Quests_Tasks + nameof(ItemTask))]
    public class ItemTask : QuestTask
    {
        [LabelText("物品")] public Item item = null;
        [LabelText("要收取的数量")] public int amountToCollect = 1;

        public ItemTask()
        {
            m_title = "Acquire {0} ({1}/{2})";
        }

        public override QuestTaskProgress CreateTaskProgress() => new ItemTaskProgress(this);

        public override string GetTitle()
        {
            return StringFormatter.Format(m_title, item.displayName, amountToCollect, amountToCollect);
        }

        public override string GetTitle(QuestTaskProgress progress)
        {
            return StringFormatter.Format(m_title, item.displayName, ((ItemTaskProgress)progress).currentQuantity,
                amountToCollect);
        }
    }
}