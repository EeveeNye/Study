using System;
using UnityEditor;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    // TODO: Remove, not necessary anymore
    // TODO: 移除，不再需要
    [CustomEditor(typeof(AbilitySheet), true)]
    public class AbilitySheetEditor : Editor
    {
        // 显示消息框
        private void ShowMessage(MessageType type, string message, params object[] args)
        {
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(StringFormatter.Format(message, args), type);
        }

        private void ShowError(string message, params object[] args) => ShowMessage(MessageType.Error, message, args);

        private void ShowWarning(string message, params object[] args) =>
            ShowMessage(MessageType.Warning, message, args);

        // 查找泛型基类模板参数的类型
        static Type FindGenericBaseTemplateParameter(Type type, Type genericBase)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericBase.GetGenericTypeDefinition())
            {
                return type.GetGenericArguments()[0];
            }
            else if (type.BaseType != null)
            {
                return FindGenericBaseTemplateParameter(type.BaseType, genericBase);
            }
            else
            {
                return null;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AbilitySheet abilitySheet = target as AbilitySheet;
            if (abilitySheet.prefab)
            {
                var abilityBase = abilitySheet.prefab.GetComponent<AbilityBase>();

                if (abilityBase == null)
                {
                    // 显示错误消息，要求预制体的根节点上必须附加一个名为"AbilityBase"的组件
                    ShowError("提供的能力预制体必须附加一个名为 {0} 的组件到其根节点上！", nameof(AbilityBase));
                }
                else
                {
                    Type abilitySheetType = abilitySheet.GetType();
                    Type abilityExpectedSheetType =
                        FindGenericBaseTemplateParameter(abilityBase.GetType(), typeof(Ability<>));

                    if (abilitySheetType != abilityExpectedSheetType)
                    {
                        if (abilitySheetType.IsSubclassOf(abilityExpectedSheetType))
                        {
                            // 显示警告消息，表明AbilitySheet对于该预制体来说过于冗余，建议使用另一个类型的数据表
                            ShowWarning("{0} 对于 \"{1}\" 来说过于冗余。该数据表中的某些设置可能不会被使用。考虑使用类型为 {2} 的数据表。",
                                abilitySheetType.Name,
                                abilitySheet.prefab.name,
                                abilityExpectedSheetType.Name);
                        }
                        else
                        {
                            // 显示错误消息，指示AbilitySheet与预制体不兼容，并提供适当的类型建议
                            ShowError("\"{0}\" 与 {1} 不兼容。需要一个类型为 {2} 的 ScriptableObject 与该预制体一起工作。",
                                abilitySheet.prefab.name,
                                abilitySheetType.Name,
                                abilityExpectedSheetType.Name);
                        }
                    }
                }
            }
        }
    }
}