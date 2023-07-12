using UnityEditor;
using Sirenix.OdinInspector.Editor;

namespace Gyvr.Mythril2D
{
    [CustomEditor(typeof(Quest))]
    public class QuestEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Quest quest = target as Quest;

            if (!IsQuestOfferDialogueValid(quest.questOfferDialogue))
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(
                    string.Format("Quest Offer DialogueSequence没有发送预期[{0}]消息的选项。这项任务可能无法被玩家接收。",
                        EDialogueMessageType.Accept), MessageType.Warning);
            }
        }

        private bool IsQuestOfferDialogueValid(DialogueSequence sequence)
        {
            return HasQuestOfferMessage(sequence);
        }

        private bool HasQuestOfferMessage(DialogueSequence sequence)
        {
            if (sequence)
            {
                foreach (DialogueSequenceOption option in sequence.options)
                {
                    if (option.message.Equals(EDialogueMessageType.Accept))
                    {
                        return true;
                    }
                    else if (option.sequence)
                    {
                        return HasQuestOfferMessage(option.sequence);
                    }
                }
            }

            return false;
        }
    }
}