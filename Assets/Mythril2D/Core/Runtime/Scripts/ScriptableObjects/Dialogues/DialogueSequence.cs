using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [System.Serializable]
    public struct DialogueSequenceOption
    {
        [LabelText("选项名称")] public string name; // 选项名称
        [LabelText("对话序列")] public DialogueSequence sequence; // 关联的对话序列
        [LabelText("对话消息")] public DialogueMessage message; // 关联的对话消息
    }

    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Dialogues + nameof(DialogueSequence))]
    public class DialogueSequence : ScriptableObject
    {
        [Header("对话列表")] [LabelText("对话行")] public string[] lines = null; // 对话行数组
        [Header("对话设置")] [LabelText("对话选项")] public DialogueSequenceOption[] options = null; // 对话选项数组

        [Header("完成后操作")] [LabelText("对话完成后要执行的操作")] 
        public ActionHandler[] toExecuteOnCompletion = null; // 对话完成后要执行的操作数组

        // 将对话序列转换为对话树
        public DialogueTree ToDialogueTree(string speaker, params string[] args)
        {
            return DialogueUtils.CreateDialogueTree(this, speaker, args);
        }
    }
}