using System;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    public class DatabaseWindow : EditorWindow
    {
        // 定义要显示的选项卡的类型数组
        private static readonly Type[] Tabs =
        {
            typeof(HeroSheet), // 英雄数据表
            typeof(MonsterSheet), // 怪物数据表
            typeof(NPCSheet), // NPC数据表
            typeof(AbilitySheet), // 技能数据表
            typeof(Item), // 物品
            typeof(Shop), // 商店
            typeof(Inn), // 客栈
            typeof(Quest), // 任务
            typeof(DialogueSequence), // 对话序列
            typeof(ScriptableAction), // 脚本动作
            typeof(AudioClipResolver), // 音频剪辑解析器
            typeof(SaveFile), // 存档文件
            typeof(NavigationCursorStyle), // 导航光标样式
            typeof(GameConfig) // 游戏配置
        };

        // 定义要显示的选项卡的类型数组和对应的中文名称
        private static readonly (Type Type, string Name)[] TabsName =
        {
            (typeof(HeroSheet), "英雄数据表"),
            (typeof(MonsterSheet), "怪物数据表"),
            (typeof(NPCSheet), "NPC数据表"),
            (typeof(AbilitySheet), "技能数据表"),
            (typeof(Item), "物品"),
            (typeof(Shop), "商店"),
            (typeof(Inn), "客栈"),
            (typeof(Quest), "任务"),
            (typeof(DialogueSequence), "对话序列"),
            (typeof(ScriptableAction), "脚本事件"),
            (typeof(AudioClipResolver), "音频列表"),
            (typeof(SaveFile), "存档文件"),
            (typeof(NavigationCursorStyle), "导航光标样式"),
            (typeof(GameConfig), "游戏配置")
        };


        private static int _selectedTab = 0; // 当前选中的选项卡的索引
        private static int _selectedIndex = -1; // 当前选中的ScriptableObject的索引
        private static Vector2 _scrollPos; // 滚动视图的位置
        private static ScriptableObject[] _scriptableObjects; // 所有的ScriptableObject对象数组
        private static string _searchString = string.Empty; // 搜索字符串

        [MenuItem("Window/Mythril2D/Database")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<DatabaseWindow>();
            window.titleContent = new GUIContent("Database");
            window.Show();
        }

        public class ReferenceCountUtility
        {
            // 获取ScriptableObject的引用计数
            public static int GetReferenceCount(ScriptableObject scriptableObject)
            {
                var path = AssetDatabase.GetAssetPath(scriptableObject);
                var dependencies = AssetDatabase.GetDependencies(path);

                int referenceCount = 0;
                foreach (var dependencyPath in dependencies)
                {
                    var dependency = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(dependencyPath);
                    if (dependency == scriptableObject)
                    {
                        referenceCount++;
                    }
                }

                return referenceCount;
            }
        }

        // 查找指定类型的所有ScriptableObject实例
        public static T[] FindAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            T[] instances = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return instances;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.Width(100));

            var previousSelectedTab = _selectedTab;
            // 显示选项卡，可以选择不同的选项卡
            // _selectedTab = GUILayout.SelectionGrid(_selectedTab, Tabs.Select(t => t.Name).ToArray(), 1);
            _selectedTab = GUILayout.SelectionGrid(_selectedTab, TabsName.Select(t => t.Name).ToArray(), 1);

            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            _scriptableObjects = FindAllInstances<ScriptableObject>();
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            // 搜索框，用于过滤元素
            var previousSearchString = _searchString;
            _searchString = GUILayout.TextField(_searchString, EditorStyles.toolbarSearchField);

            if (previousSearchString != _searchString || _selectedTab != previousSelectedTab)
            {
                _selectedIndex = -1;
            }

            // // 创建一个与所选选项卡匹配的ScriptableObject名称的数组
            // var visibleScriptableObjects = _scriptableObjects
            //     .Where(so => Tabs[_selectedTab].IsAssignableFrom(so.GetType()) && so.name.Contains(_searchString))
            //     .OrderBy(so => so.name);

            // 创建一个与所选选项卡匹配的ScriptableObject名称的数组
            var visibleScriptableObjects = _scriptableObjects
                .Where(so =>
                    TabsName[_selectedTab].Type.IsAssignableFrom(so.GetType()) && so.name.Contains(_searchString))
                .OrderBy(so => so.name);


            var names = visibleScriptableObjects.Select(so => so.name).ToArray();

            // 使用SelectionGrid显示名称，并获取选中的索引
            var previouslySelectedIndex = _selectedIndex;
            _selectedIndex = GUILayout.SelectionGrid(_selectedIndex, names, 1, EditorStyles.objectField);

            // 如果选择了索引，则将活动对象设置为相应的ScriptableObject
            if (_selectedIndex >= 0 && previouslySelectedIndex != _selectedIndex)
            {
                Selection.activeObject = _scriptableObjects.First(so => so.name == names[_selectedIndex]);
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}