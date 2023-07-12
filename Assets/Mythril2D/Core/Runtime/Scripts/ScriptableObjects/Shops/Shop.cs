using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public enum ETransactionType
    {
        Buy,
        Sell
    }

    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Shops + nameof(Shop))]
    public class Shop : ScriptableObject
    {
        [LabelText("物品")] public Item[] items = null;
        [LabelText("销售价格乘数")] public float sellingPriceMultiplier = 0.5f;
        [LabelText("买入价格乘数")] public float buyingPriceMultiplier = 1.0f;

        public int GetPrice(Item item, ETransactionType transaction)
        {
            float floatPrice = 0.0f;

            switch (transaction)
            {
                case ETransactionType.Buy:
                    floatPrice = item.price * buyingPriceMultiplier;
                    break;

                case ETransactionType.Sell:
                    floatPrice = item.price * sellingPriceMultiplier;
                    break;
            }

            return (int)math.ceil(floatPrice);
        }
    }
}