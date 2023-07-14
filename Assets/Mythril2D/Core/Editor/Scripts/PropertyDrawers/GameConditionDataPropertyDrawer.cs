// 引入需要的库

using System;
using UnityEditor;
using UnityEngine;

// 定义命名空间
namespace Gyvr.Mythril2D
{
    // 为GameConditionData类型定义一个自定义的PropertyDrawer
    [CustomPropertyDrawer(typeof(GameConditionData))]
    public class GameConditionDataPropertyDrawer : PropertyDrawer
    {
        // 重写GetPropertyHeight方法，返回属性的高度
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        // 重写OnGUI方法，负责绘制属性
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 获取type和state属性
            SerializedProperty type = property.FindPropertyRelative("type");
            SerializedProperty state = property.FindPropertyRelative("state");

            // 开始绘制属性
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            // 获取conditionType的值
            EGameConditionType conditionType = (EGameConditionType)type.enumValueIndex;

            // 保存indent级别并设置为0
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // 如果conditionType不等于None，那么绘制state，type和value属性
            if (conditionType != EGameConditionType.None)
            {
                // 计算state，type和value的矩形区域
                var stateRect = new Rect(position.x, position.y, position.width * 0.15f,
                    EditorGUIUtility.singleLineHeight);
                var typeRect = new Rect(stateRect.xMax, position.y, position.width * 0.4f,
                    EditorGUIUtility.singleLineHeight);
                var valueRect = new Rect(typeRect.xMax, position.y, position.width * 0.45f,
                    EditorGUIUtility.singleLineHeight);

                // 绘制type属性
                type.enumValueIndex = Convert.ToInt32(EditorGUI.EnumPopup(typeRect, GUIContent.none, conditionType));

                // 绘制state属性
                EditorGUI.PropertyField(stateRect, state, GUIContent.none);

                // 根据conditionType的值，绘制不同的value属性
                switch (conditionType)
                {
                    default:
                        EditorGUI.LabelField(valueRect, "Select a type");
                        break;
                    case EGameConditionType.QuestAvailable:
                    case EGameConditionType.QuestFullfilled:
                    case EGameConditionType.QuestCompleted:
                    case EGameConditionType.QuestActive:
                        DrawProperty(property, valueRect, "quest");
                        break;
                    case EGameConditionType.TaskActive:
                        DrawProperty(property, valueRect, "task");
                        break;
                    case EGameConditionType.GameFlagSet:
                        DrawProperty(property, valueRect, "flagID");
                        break;
                }
            }
            else
            {
                // 如果conditionType等于None，那么只绘制type属性
                var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                type.enumValueIndex = Convert.ToInt32(EditorGUI.EnumPopup(typeRect, GUIContent.none, conditionType));
            }

            // 恢复之前的indent级别
            EditorGUI.indentLevel = indent;

            // 结束属性绘制
            EditorGUI.EndProperty();
        }

        // 根据propertyName绘制对应的属性
        private void DrawProperty(SerializedProperty property, Rect position, string propertyName)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative(propertyName), GUIContent.none);
        }
    }
}