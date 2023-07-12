using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;

namespace Gyvr.Mythril2D
{
    [CustomEditor(typeof(HeroSheet))]
    public class HeroSheetEditor : OdinEditor
    {
        private int m_previewLevel = 1;

        public int GetTotalExperienceRequired(HeroSheet sheet, int level)
        {
            return level > 0 ? sheet.experience[level] + GetTotalExperienceRequired(sheet, level - 1) : 0;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            HeroSheet sheet = target as HeroSheet;

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("等级预览", EditorStyles.boldLabel); // Evolution Preview

            m_previewLevel = EditorGUILayout.IntSlider("等级", m_previewLevel, Stats.MinLevel, Stats.MaxLevel); // Level

            int experienceRequired = sheet.experience[m_previewLevel];
            int experienceRequiredTotal = GetTotalExperienceRequired(sheet, m_previewLevel);

            GUI.enabled = false;
            EditorGUILayout.IntField("所需经验", experienceRequired); // Experience Required
            EditorGUILayout.IntField("总计所需经验", experienceRequiredTotal); // Experience Required Total
            GUI.enabled = true;
        }
    }
}