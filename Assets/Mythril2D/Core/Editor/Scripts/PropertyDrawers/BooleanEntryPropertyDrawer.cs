using UnityEditor;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    [CustomPropertyDrawer(typeof(BooleanEntry))] //自定义BooleanEntry属性的绘制器
    public class BooleanEntryPropertyDrawer : PropertyDrawer
    {
        private const int fieldOffset = 3; //属性控件之间的间距
        private const int checkboxSize = 20; //复选框控件的大小

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight; //返回单行高度
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty id = property.FindPropertyRelative("id"); //获取id属性
            SerializedProperty value = property.FindPropertyRelative("value"); //获取value属性

            //计算id和value的矩形区域
            var idRect = new Rect(position.x, position.y, position.width - checkboxSize,
                EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(idRect.xMax + fieldOffset, position.y, checkboxSize,
                EditorGUIUtility.singleLineHeight);

            //开始绘制属性
            EditorGUI.BeginProperty(position, label, property);

            //保存indent级别并设置为0
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //绘制id和value属性
            EditorGUI.PropertyField(idRect, id, GUIContent.none);
            EditorGUI.PropertyField(valueRect, value, GUIContent.none);

            //恢复之前的indent级别
            EditorGUI.indentLevel = indent;

            //结束属性绘制
            EditorGUI.EndProperty();
        }
    }
}