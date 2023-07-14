using Sirenix.OdinInspector;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public abstract class QuestTask : ScriptableObject
    {
        [SerializeField] [LabelText("标题")] protected string m_title = string.Empty;

        [SerializeField] [LabelText("要求完成之前的任务")]
        protected bool m_requirePreviousTasksCompletion = false;

        public bool requirePreviousTaskCompletion => m_requirePreviousTasksCompletion;

        public abstract QuestTaskProgress CreateTaskProgress();
        public abstract string GetTitle();
        public abstract string GetTitle(QuestTaskProgress progress);
    }
}