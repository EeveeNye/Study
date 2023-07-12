using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Save + nameof(SaveFile))]
    public class SaveFile : ScriptableObject
    {
        [SerializeField] [LabelText("保存文件数据")] private SaveFileData m_content;

        public SaveFileData content => m_content;
    }
}