using UnityEditor;
using UnityEngine;

// 定义命名空间
namespace Gyvr.Mythril2D
{
    // 为ActionArg类型定义一个自定义的PropertyDrawer
    [CustomPropertyDrawer(typeof(ActionArg))]
    public class ActionArgPropertyDrawer : PropertyDrawer
    {
        // 定义属性之间的间距
        private const int fieldOffset = 3;

        // 重写GetPropertyHeight方法，返回属性的高度
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight; // 获取属性高度
        }

        // 重写OnGUI方法，负责绘制属性
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 获取type属性
            SerializedProperty type = property.FindPropertyRelative("type");

            // 划分type和value的矩形区域
            var typeRect = new Rect(position.x, position.y, position.width / 3, EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(typeRect.xMax + fieldOffset, position.y, (position.width / 3) * 2 - fieldOffset,
                EditorGUIUtility.singleLineHeight);

            // 开始绘制属性
            EditorGUI.BeginProperty(position, label, property);

            // 保存indent级别并设置为0
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // 绘制type属性
            EditorGUI.LabelField(typeRect, ((EActionArgType)type.enumValueIndex).ToString());

            // 根据type枚举值绘制不同的value属性
            switch ((EActionArgType)type.enumValueIndex)
            {
                case EActionArgType.Int:
                    DrawProperty(property, valueRect, "int");
                    break;

                case EActionArgType.Bool:
                    DrawProperty(property, valueRect, "bool");
                    break;

                case EActionArgType.Float:
                    DrawProperty(property, valueRect, "float");
                    break;

                case EActionArgType.String:
                    DrawProperty(property, valueRect, "string");
                    break;

                case EActionArgType.Object:
                    DrawProperty(property, valueRect, "object");
                    break;

                default:
                    EditorGUI.LabelField(valueRect, "Null");
                    break;
            }

            // 恢复之前的indent级别
            EditorGUI.indentLevel = indent;

            // 结束属性绘制
            EditorGUI.EndProperty();
        }

        // 根据类型绘制对应的value属性
        private void DrawProperty(SerializedProperty property, Rect position, string type)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative(string.Format("{0}_value", type)),
                GUIContent.none);
        }
    }
}