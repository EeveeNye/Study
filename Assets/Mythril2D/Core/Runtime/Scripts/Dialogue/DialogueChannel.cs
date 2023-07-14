using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gyvr.Mythril2D
{
    /// <summary>
    /// 负责处理游戏中的对话系统。
    /// </summary>
    public class DialogueChannel : MonoBehaviour
    {
        // 公共的事件
        public UnityEvent<DialogueTree> dialogueStarted = new UnityEvent<DialogueTree>(); // 对话开始的事件
        public UnityEvent<DialogueTree> dialogueEnded = new UnityEvent<DialogueTree>(); // 对话结束的事件
        public UnityEvent<DialogueNode> dialogueNodeChanged = new UnityEvent<DialogueNode>(); // 对话节点改变的事件

        // 私有成员
        private DialogueTree m_dialogueTree = null; // 当前的对话树
        private DialogueNode m_currentNode = null; // 当前的对话节点
        private Queue<DialogueTree> m_dialogueQueue = new Queue<DialogueTree>(); // 对话队列

        // 将对话加入队列
        public void AddToQueue(DialogueTree dialogue)
        {
            m_dialogueQueue.Enqueue(dialogue);
        }

        // 清空对话队列
        public void ClearQueue()
        {
            m_dialogueQueue.Clear();
        }

        // 立即播放指定的对话
        public void PlayNow(string line, params object[] args)
        {
            AddToQueue(new DialogueTree(new DialogueNode(StringFormatter.Format(line, args))));
            PlayQueue();
        }

        // 立即播放指定的对话树
        public void PlayNow(DialogueTree dialogue)
        {
            AddToQueue(dialogue);
            PlayQueue();
        }

        // 播放对话队列
        public void PlayQueue()
        {
            if (m_dialogueTree == null)
            {
                if (m_dialogueQueue.Count > 0)
                {
                    Play(m_dialogueQueue.Dequeue());
                }
            }
        }

        // 尝试跳过当前的对话
        public bool TrySkipping()
        {
            if (m_currentNode != null && m_currentNode.optionCount < 2)
            {
                Next();
                return true;
            }

            return false;
        }

        // 转到下一个对话
        public void Next(int option = 0)
        {
            m_dialogueTree.OnNodeExecuted(m_currentNode, option);

            if (m_currentNode.toExecuteOnCompletion != null)
            {
                foreach (ActionHandler actionHandler in m_currentNode.toExecuteOnCompletion)
                {
                    actionHandler.Execute();
                }
            }

            SetCurrentNode(m_currentNode.GetNext(option));
        }

        // 是否正在播放对话
        public bool IsPlaying() => m_dialogueTree != null;

        // 播放指定的对话树
        private void Play(DialogueTree dialogue)
        {
            if (dialogue.root != null)
            {
                m_dialogueTree = dialogue;
                dialogueStarted.Invoke(m_dialogueTree);
                m_dialogueTree.dialogueStarted.Invoke();
                SetCurrentNode(dialogue.root);
            }
            else
            {
                Debug.LogError("Cannot start a dialogue with a null entry point node.");
            }
        }

        // 设置当前的对话节点
        private void SetCurrentNode(DialogueNode node)
        {
            m_currentNode = node;
            dialogueNodeChanged.Invoke(m_currentNode);

            if (m_currentNode == null)
            {
                OnLastNodeReached();
            }
        }

        // 当达到最后一个节点时的操作
        private void OnLastNodeReached()
        {
            DialogueTree tree = m_dialogueTree;

            if (tree != null)
            {
                dialogueEnded.Invoke(tree);
                tree.dialogueEnded.Invoke(m_dialogueTree.messages);
                m_dialogueTree = null;
                PlayQueue();
            }
        }
    }
}