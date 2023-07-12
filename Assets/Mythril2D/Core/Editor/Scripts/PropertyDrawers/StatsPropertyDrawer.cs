using UnityEditor;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    // 自定义属性抽屉，用于显示Stats类型的字段
    [CustomPropertyDrawer(typeof(Stats))]
    public class StatsPropertyDrawer : PropertyDrawer
    {
        // 获取属性的高度
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            // 如果属性展开，则为每个统计值添加一行
            if (property.isExpanded)
            {
                height += Stats.StatCount *
                          (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            }

            return height;
        }

        // 自定义的属性绘制函数
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 查找对应的数组属性
            SerializedProperty values = property.FindPropertyRelative("m_values");

            // 如果数组的大小不正确，则重置数组
            if (values.arraySize != Stats.StatCount)
            {
                values.ClearArray();
                values.arraySize = Stats.StatCount;
            }

            // 开始绘制属性
            EditorGUI.BeginProperty(position, label, property);

            // 保存原始缩进等级
            var indent = EditorGUI.indentLevel;

            position.height = EditorGUIUtility.singleLineHeight;

            // 绘制属性的可折叠标签
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

            // 如果属性已展开，则绘制每个统计值
            if (property.isExpanded)
            {
                ++EditorGUI.indentLevel;
                Rect statRect = new Rect(position.x,
                    position.y + position.height + EditorGUIUtility.standardVerticalSpacing, position.width,
                    EditorGUIUtility.singleLineHeight);

                // 对于每个统计值，绘制一个属性字段
                for (int i = 0; i < values.arraySize; ++i)
                {
                    SerializedProperty statProperty = values.GetArrayElementAtIndex(i);


                    // 对应枚举到中文字符串
                    string displayName;
                    switch ((EStat)i)
                    {
                        case EStat.Health:
                            displayName = "生命值";
                            break;
                        case EStat.Mana:
                            displayName = "法力值";
                            break;
                        case EStat.PhysicalAttack:
                            displayName = "物理攻击力";
                            break;
                        case EStat.MagicalAttack:
                            displayName = "魔法攻击力";
                            break;
                        case EStat.PhysicalDefense:
                            displayName = "物理防御力";
                            break;
                        case EStat.MagicalDefense:
                            displayName = "魔法防御力";
                            break;
                        case EStat.Agility:
                            displayName = "敏捷";
                            break;
                        case EStat.Luck:
                            displayName = "运气";
                            break;
                        default:
                            displayName = ((EStat)i).ToString();
                            break;
                    }

                    EditorGUI.PropertyField(statRect, statProperty, new GUIContent(displayName));

                    statRect.y += statRect.height + EditorGUIUtility.standardVerticalSpacing;
                }
            }

            // 恢复原始缩进等级
            EditorGUI.indentLevel = indent;
            // 结束绘制属性
            EditorGUI.EndProperty();
        }
    }
}