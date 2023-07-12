using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public enum EItemLocation
    {
        Bag,
        Equipment
    }

    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Items + nameof(Item))]
    public class Item : ScriptableObject, INameable
    {
        [Header("基础属性")] [SerializeField] [LabelText("图标")]
        private Sprite m_icon = null;

        [SerializeField] [LabelText("显示名")] private string m_displayName = string.Empty;
        [SerializeField] [LabelText("描述")] private string m_description = string.Empty;
        [SerializeField] [LabelText("价格")] private int m_price = 50;

        public virtual void Use(CharacterBase target, EItemLocation location)
        {
            GameManager.DialogueSystem.Main.PlayNow("This item has no effect");
        }

        public Sprite icon => m_icon;
        public string displayName => DisplayNameUtils.GetNameOrDefault(this, m_displayName);
        public string description => StringFormatter.Format(m_description);
        public int price => m_price;
    }
}