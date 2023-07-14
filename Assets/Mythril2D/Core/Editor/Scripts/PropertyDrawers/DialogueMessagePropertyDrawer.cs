using System;
using UnityEditor;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CustomPropertyDrawer(typeof(DialogueMessage))]
    public class DialogueMessagePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty type = property.FindPropertyRelative("type");

            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EDialogueMessageType selectedType = (EDialogueMessageType)type.enumValueIndex;

            Rect typeRect;
            //设置对话设置窗口的不同选项下的界面
            if (selectedType == EDialogueMessageType.Custom)
            {
                // 如果类型是Custom,则将类型选择框只占一半宽度
                typeRect = new Rect(position.x, position.y, position.width / 2, EditorGUIUtility.singleLineHeight);
                type.enumValueIndex = Convert.ToInt32(EditorGUI.EnumPopup(typeRect, GUIContent.none,
                    (EDialogueMessageType)type.enumValueIndex));

                // 自定义消息占另一半宽度
                SerializedProperty customMessage = property.FindPropertyRelative("customMessage");
                var customMessageRect = new Rect(typeRect.xMax, position.y, position.width / 2,
                    EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(customMessageRect, customMessage, GUIContent.none);
            }
            else
            {
                // 否则类型选择框占满整行宽度
                typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                type.enumValueIndex = Convert.ToInt32(EditorGUI.EnumPopup(typeRect, GUIContent.none,
                    (EDialogueMessageType)type.enumValueIndex));
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}