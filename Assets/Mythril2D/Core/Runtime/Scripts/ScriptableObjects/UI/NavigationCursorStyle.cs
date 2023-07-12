using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_UI + nameof(NavigationCursorStyle))]
    public class NavigationCursorStyle : ScriptableObject
    {
        [LabelText("精灵")] public Sprite sprite = null;
        [LabelText("颜色")] public Color color = Color.white;
        [LabelText("位置偏移")] public Vector2 positionOffset = Vector2.zero;
        [LabelText("尺寸偏移")] public Vector2 sizeOffset = Vector2.zero;
    }
}