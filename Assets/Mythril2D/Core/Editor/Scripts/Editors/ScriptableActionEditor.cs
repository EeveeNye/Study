using System;
using UnityEditor;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    // 继承自Editor的自定义编辑器类，用于处理ScriptableAction类型的对象
    [CustomEditor(typeof(ScriptableAction), true)]
    public class ScriptableActionEditor : Editor
    {
        private int m_argIndex = 0;

        // 获取参数列表的字符串表示
        // argTypes是参数类型数组，argDescriptions是参数描述数组
        public string GetArgList(Type[] argTypes, string[] argDescriptions)
        {
            string list = string.Empty;

            // 如果argTypes不为空并且有元素
            if (argTypes != null && argTypes.Length > 0)
            {
                foreach (Type argType in argTypes)
                {
                    // 为每个参数添加一个带类型和描述的条目
                    list += $"[{m_argIndex}] > <b>{argType.Name}</b> <i>({argDescriptions[m_argIndex]})</i>\n";
                    ++m_argIndex;
                }
            }

            return list;
        }

        // 重写Inspector界面的GUI
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // 提供关于ScriptableAction的一般信息
            EditorGUILayout.HelpBox("ScriptableAction是可以执行多次的通用对象，每次执行可以提供不同的参数。所提供的参数必须匹配下面明确的格式。", MessageType.Info);

            m_argIndex = 0;

            ScriptableAction scriptableAction = target as ScriptableAction;

            // 获取必要参数和可选参数的列表
            string requiredArgList = GetArgList(scriptableAction.RequiredArgs, scriptableAction.ArgDescriptions);
            string optionalArgList = GetArgList(scriptableAction.OptionalArgs, scriptableAction.ArgDescriptions);

            // 创建用于显示列表标题和内容的GUI样式
            GUIStyle listTitleStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                richText = true,
                wordWrap = true
            };

            GUIStyle listContentStyle = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                wordWrap = true
            };

            // 如果存在必要参数，显示它们
            if (requiredArgList != string.Empty)
            {
                GUILayout.Label("必要参数", listTitleStyle);
                GUILayout.Label(requiredArgList, listContentStyle);
            }

            // 如果存在可选参数，显示它们
            if (optionalArgList != string.Empty)
            {
                GUILayout.Label("可选参数", listTitleStyle);
                GUILayout.Label(optionalArgList, listContentStyle);
            }
        }
    }
}