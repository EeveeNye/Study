using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CreateAssetMenu(menuName = AssetMenuIndexer.Mythril2D_Quests + nameof(Quest))]
    public class Quest : ScriptableObject
    {
        [Header("任务详情")] [SerializeField] [LabelText("标题")]
        private string m_title = string.Empty;

        [SerializeField] [LabelText("描述")] [TextArea]
        private string m_description = string.Empty;

        [SerializeField] [LabelText("推荐等级")] [Range(Stats.MinLevel, Stats.MaxLevel)]
        private int m_recommendedLevel = 1;

        [SerializeField] [LabelText("可重复")] private bool m_repeatable = false;
        [SerializeField] [LabelText("任务任务")] private QuestTask[] m_tasks = null;

        [Header("任务完成")] [SerializeField] [LabelText("完成后执行")]
        private ActionHandler[] m_toExecuteOnQuestCompletion = null;

        [Header("相关NPC")] [SerializeField] [LabelText("接取NPC")]
        private NPCSheet m_offeredBy = null;

        [SerializeField] [LabelText("上交NPC")] private NPCSheet m_reportTo = null;

        [Header("对话")] [SerializeField] [LabelText("任务接取对话")]
        private DialogueSequence m_questOfferDialogue = null;

        [SerializeField] [LabelText("任务提示对话")] private DialogueSequence m_questHintDialogue = null;
        [SerializeField] [LabelText("任务完成对话")] private DialogueSequence m_questCompletedDialogue = null;

        public QuestTask[] tasks => m_tasks;
        public string title => m_title;
        public string description => m_description;
        public int recommendedLevel => m_recommendedLevel;
        public bool repeatable => m_repeatable;
        public NPCSheet offeredBy => m_offeredBy;
        public NPCSheet reportTo => m_reportTo;
        public DialogueSequence questOfferDialogue => m_questOfferDialogue;
        public DialogueSequence questHintDialogue => m_questHintDialogue;
        public DialogueSequence questCompletedDialogue => m_questCompletedDialogue;
        public ActionHandler[] toExecuteOnQuestCompletion => m_toExecuteOnQuestCompletion;
    }
}