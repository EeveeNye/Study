using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;

namespace Gyvr.Mythril2D
{
    [CustomEditor(typeof(MonsterSheet))]
    public class MonsterSheetEditor : OdinEditor
    {
        private int m_previewLevel = 1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MonsterSheet sheet = target as MonsterSheet;

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("等级预览", EditorStyles.boldLabel);

            m_previewLevel = EditorGUILayout.IntSlider("等级", m_previewLevel, 1, Stats.MaxLevel);

            Stats previewStats = sheet.stats[m_previewLevel];
            int experience = sheet.experience[m_previewLevel];
            int money = sheet.money[m_previewLevel];

            int total = previewStats.GetTotal();
            int average = (int)math.round(total / 5.0f);

            GUI.enabled = false;
            EditorGUILayout.IntField("生命值", previewStats[EStat.Health]);
            EditorGUILayout.IntField("法力值", previewStats[EStat.Mana]);
            EditorGUILayout.IntField("物理攻击", previewStats[EStat.PhysicalAttack]);
            EditorGUILayout.IntField("魔法攻击", previewStats[EStat.MagicalAttack]);
            EditorGUILayout.IntField("物理防御", previewStats[EStat.PhysicalDefense]);
            EditorGUILayout.IntField("魔法防御", previewStats[EStat.MagicalDefense]);
            EditorGUILayout.IntField("敏捷度", previewStats[EStat.Agility]);
            EditorGUILayout.IntField("幸运值", previewStats[EStat.Luck]);
            EditorGUILayout.Space();
            EditorGUILayout.IntField("总计", total);
            EditorGUILayout.IntField("平均值", average);
            EditorGUILayout.Space();
            EditorGUILayout.IntField("经验值", experience);
            EditorGUILayout.IntField("金钱", money);

            EditorGUILayout.Space();
            GUI.enabled = true;
        }
    }
}