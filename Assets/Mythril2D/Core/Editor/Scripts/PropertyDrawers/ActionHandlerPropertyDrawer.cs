// 引入需要的库

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

// 定义命名空间
namespace Gyvr.Mythril2D
{
    // 为ActionHandler类型定义一个自定义的PropertyDrawer
    [CustomPropertyDrawer(typeof(ActionHandler))]
    public class ActionHandlerPropertyDrawer : PropertyDrawer
    {
        // 定义属性之间的间距
        private const int fieldOffset = 20;

        // 重写GetPropertyHeight方法，返回属性的高度
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // 获取args属性
            SerializedProperty args = property.FindPropertyRelative("args");

            // 如果args展开，则返回对应的高度，否则返回单行高度
            if (args.isExpanded)
            {
                int lineCount = math.max(args.arraySize, 1) + 3;
                return (EditorGUIUtility.singleLineHeight + 2) * lineCount;
            }
            else
            {
                return EditorGUIUtility.singleLineHeight;
            }
        }

        // 创建一个对象选择弹窗
        public bool CreateObjectSelectionPopup<T>(Rect rect, SerializedProperty property) where T : UnityEngine.Object
        {
            // 获取所有T类型的对象
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            T[] instances = new T[guids.Length];
            List<string> names = new List<string>(guids.Length);
            GUIContent[] options = new GUIContent[guids.Length];

            // 对于每个对象，获取其路径，加载它，获取它的名字，然后添加到names和options列表中
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
                string name = instances[i].name;
                names.Add(name);
                options[i] = new GUIContent(name);
            }

            // 获取当前选中的对象，并显示一个弹窗让用户选择一个对象
            int previousSelected = property.objectReferenceValue
                ? names.IndexOf((property.objectReferenceValue as ScriptableAction).name.ToString())
                : -1;
            int currentSelected = EditorGUI.Popup(rect, previousSelected, options);

            // 如果用户选择了一个新的对象，那么更新property的objectReferenceValue，并返回true
            if (previousSelected != currentSelected)
            {
                property.objectReferenceValue = instances[currentSelected];
                return true;
            }

            // 如果用户没有选择新的对象，那么返回false
            return false;
        }

        // 将Type类型转换为EActionArgType枚举类型
        private EActionArgType TypeToActionArgType(Type type)
        {
            if (type == typeof(int)) return EActionArgType.Int;
            if (type == typeof(bool)) return EActionArgType.Bool;
            if (type == typeof(float)) return EActionArgType.Float;
            if (type == typeof(string)) return EActionArgType.String;
            if (type.IsSubclassOf(typeof(UnityEngine.Object))) return EActionArgType.Object;

            return EActionArgType.Null;
        }

        // 重写OnGUI方法，负责绘制属性
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 获取action和args属性
            SerializedProperty action = property.FindPropertyRelative("action");
            SerializedProperty args = property.FindPropertyRelative("args");

            // 定义action和args属性的绘制区域
            Rect actionRect = new Rect(position.x, position.y, position.width / 3, EditorGUIUtility.singleLineHeight);
            Rect argsRect = new Rect(actionRect.xMax + fieldOffset, position.y, (position.width / 3) * 2 - fieldOffset,
                EditorGUIUtility.singleLineHeight);

            // 开始绘制属性
            EditorGUI.BeginProperty(position, label, property);

            // 保存indent级别并设置为0
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // 绘制action属性
            CreateObjectSelectionPopup<ScriptableAction>(actionRect, action);
            ScriptableAction actionInstance = (action.objectReferenceValue as ScriptableAction);

            // 如果actionInstance存在，那么绘制args属性，并根据actionInstance的要求调整args的大小和类型
            if (actionInstance)
            {
                EditorGUI.PropertyField(argsRect, args);

                args.arraySize = math.clamp(args.arraySize, actionInstance.RequiredArgs.Length,
                    actionInstance.RequiredArgs.Length + actionInstance.OptionalArgs.Length);

                // Forcing args types based on the provided ScriptableAction Type requirements
                for (int i = 0; i < args.arraySize; i++)
                {
                    SerializedProperty elementProperty = args.GetArrayElementAtIndex(i);
                    SerializedProperty typeProperty = elementProperty.FindPropertyRelative("type");
                    Type expectedArgType = actionInstance.GetArgTypeAtIndex(i);
                    typeProperty.enumValueIndex = (int)TypeToActionArgType(expectedArgType);
                }
            }

            // 恢复之前的indent级别
            EditorGUI.indentLevel = indent;

            // 结束属性绘制
            EditorGUI.EndProperty();
        }
    }
}