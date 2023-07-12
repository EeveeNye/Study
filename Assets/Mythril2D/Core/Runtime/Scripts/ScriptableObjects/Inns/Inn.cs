using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Inns + nameof(Inn))]
    public class Inn : ScriptableObject
    {
        [LabelText("价格")] public int price;
        [LabelText("治疗量")] public int healAmount;
        [LabelText("法力回复量")] public int manaRecoveredAmount;
        [LabelText("治疗音效")] public AudioClipResolver healingSound;
    }
}