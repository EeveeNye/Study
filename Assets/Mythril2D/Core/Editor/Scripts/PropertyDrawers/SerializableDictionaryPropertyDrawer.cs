//             // From GitHub: https:            //github.com/azixMcAze/Unity-SerializableDictionary

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Gyvr.Mythril2D
{
    // 自定义属性绘制器,用于绘制 SerializableDictionaryBase 和 SerializableHashSetBase
    [CustomPropertyDrawer(typeof(SerializableDictionaryBase), true)]
#if NET_4_6 || NET_STANDARD_2_0
    [CustomPropertyDrawer(typeof(SerializableHashSetBase), true)]
#endif
    public class SerializableDictionaryPropertyDrawer : PropertyDrawer
    {
        // 字典键和值在Property中的相对属性名
        const string KeysFieldName = "m_keys";
        const string ValuesFieldName = "m_values";
        protected const float IndentWidth = 15f;

        // 一些图标和样式
        static GUIContent s_iconPlus = IconContent("Toolbar Plus", "Add entry");
        static GUIContent s_iconMinus = IconContent("Toolbar Minus", "Remove entry");

        static GUIContent s_warningIconConflict =
            IconContent("console.warnicon.sml", "Conflicting key, this entry will be lost");

        static GUIContent s_warningIconOther = IconContent("console.infoicon.sml", "Conflicting key");
        static GUIContent s_warningIconNull = IconContent("console.warnicon.sml", "Null key, this entry will be lost");
        static GUIStyle s_buttonStyle = GUIStyle.none;
        static GUIContent s_tempContent = new GUIContent();

        // 冲突状态
        class ConflictState
        {
            public object conflictKey = null;
            public object conflictValue = null;
            public int conflictIndex = -1;
            public int conflictOtherIndex = -1;
            public bool conflictKeyPropertyExpanded = false;
            public bool conflictValuePropertyExpanded = false;
            public float conflictLineHeight = 0f;
        }

        // 属性标识
        struct PropertyIdentity
        {
            public PropertyIdentity(SerializedProperty property)
            {
                this.instance = property.serializedObject.targetObject;
                this.propertyPath = property.propertyPath;
            }

            public UnityEngine.Object instance;
            public string propertyPath;
        }

        // 冲突状态字典
        static Dictionary<PropertyIdentity, ConflictState> s_conflictStateDict =
            new Dictionary<PropertyIdentity, ConflictState>();

        enum Action
        {
            None,
            Add,
            Remove
        }

        // 绘制字典 
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 设置标签
            label = EditorGUI.BeginProperty(position, label, property);

            Action buttonAction = Action.None; // 按钮动作
            int buttonActionIndex = 0; // 按钮动作索引

            // 获取键和值数组属性
            var keyArrayProperty = property.FindPropertyRelative(KeysFieldName);
            var valueArrayProperty = property.FindPropertyRelative(ValuesFieldName);
            // 获取冲突状态
            ConflictState conflictState = GetConflictState(property);
            // 处理冲突
            if (conflictState.conflictIndex != -1)
            {
                // 插入冲突的键
                keyArrayProperty.InsertArrayElementAtIndex(conflictState.conflictIndex);
                var keyProperty = keyArrayProperty.GetArrayElementAtIndex(conflictState.conflictIndex);
                SetPropertyValue(keyProperty, conflictState.conflictKey);
                keyProperty.isExpanded = conflictState.conflictKeyPropertyExpanded;

                if (valueArrayProperty != null)
                {
                    // 插入冲突的值  
                    valueArrayProperty.InsertArrayElementAtIndex(conflictState.conflictIndex);
                    var valueProperty = valueArrayProperty.GetArrayElementAtIndex(conflictState.conflictIndex);
                    SetPropertyValue(valueProperty, conflictState.conflictValue);
                    valueProperty.isExpanded = conflictState.conflictValuePropertyExpanded;
                }
            }

            // 计算按钮大小
            var buttonWidth = s_buttonStyle.CalcSize(s_iconPlus).x;
            // 绘制折叠箭头和标签
            var labelPosition = position;
            labelPosition.height = EditorGUIUtility.singleLineHeight;
            if (property.isExpanded)
                labelPosition.xMax -= s_buttonStyle.CalcSize(s_iconPlus).x;

            EditorGUI.PropertyField(labelPosition, property, label, false);
            // property.isExpanded = EditorGUI.Foldout(labelPosition, property.isExpanded, label);

            // 绘制字典内容
            if (property.isExpanded)
            {
                // 绘制添加按钮
                var buttonPosition = position;
                buttonPosition.xMin = buttonPosition.xMax - buttonWidth;
                buttonPosition.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.BeginDisabledGroup(conflictState.conflictIndex != -1);
                if (GUI.Button(buttonPosition, s_iconPlus, s_buttonStyle))
                {
                    buttonAction = Action.Add;
                    buttonActionIndex = keyArrayProperty.arraySize;
                }

                EditorGUI.EndDisabledGroup();

                EditorGUI.indentLevel++;
                // 遍历每一行
                var linePosition = position;
                linePosition.y += EditorGUIUtility.singleLineHeight;
                linePosition.xMax -= buttonWidth;

                foreach (var entry in EnumerateEntries(keyArrayProperty, valueArrayProperty))
                {
                    // 绘制键值
                    var keyProperty = entry.keyProperty;
                    var valueProperty = entry.valueProperty;
                    int i = entry.index;

                    float lineHeight = DrawKeyValueLine(keyProperty, valueProperty, linePosition, i);
                    // 绘制删除按钮
                    buttonPosition = linePosition;
                    buttonPosition.x = linePosition.xMax;
                    buttonPosition.height = EditorGUIUtility.singleLineHeight;
                    if (GUI.Button(buttonPosition, s_iconMinus, s_buttonStyle))
                    {
                        buttonAction = Action.Remove;
                        buttonActionIndex = i;
                    }

                    // 绘制冲突标记
                    if (i == conflictState.conflictIndex && conflictState.conflictOtherIndex == -1)
                    {
                        var iconPosition = linePosition;
                        iconPosition.size = s_buttonStyle.CalcSize(s_warningIconNull);
                        GUI.Label(iconPosition, s_warningIconNull);
                    }
                    else if (i == conflictState.conflictIndex)
                    {
                        var iconPosition = linePosition;
                        iconPosition.size = s_buttonStyle.CalcSize(s_warningIconConflict);
                        GUI.Label(iconPosition, s_warningIconConflict);
                    }
                    else if (i == conflictState.conflictOtherIndex)
                    {
                        var iconPosition = linePosition;
                        iconPosition.size = s_buttonStyle.CalcSize(s_warningIconOther);
                        GUI.Label(iconPosition, s_warningIconOther);
                    }


                    linePosition.y += lineHeight;
                }

                EditorGUI.indentLevel--;
            }

            // 执行按钮动作
            if (buttonAction == Action.Add)
            {
                keyArrayProperty.InsertArrayElementAtIndex(buttonActionIndex);
                if (valueArrayProperty != null)
                    valueArrayProperty.InsertArrayElementAtIndex(buttonActionIndex);
            }
            else if (buttonAction == Action.Remove)
            {
                DeleteArrayElementAtIndex(keyArrayProperty, buttonActionIndex);
                if (valueArrayProperty != null)
                    DeleteArrayElementAtIndex(valueArrayProperty, buttonActionIndex);
            }

            // 重置冲突状态
            conflictState.conflictKey = null;
            conflictState.conflictValue = null;
            conflictState.conflictIndex = -1;
            conflictState.conflictOtherIndex = -1;
            conflictState.conflictLineHeight = 0f;
            conflictState.conflictKeyPropertyExpanded = false;
            conflictState.conflictValuePropertyExpanded = false;

            // 遍历所有键
            foreach (var entry1 in EnumerateEntries(keyArrayProperty, valueArrayProperty))
            {
                // 当前键
                var keyProperty1 = entry1.keyProperty;
                int i = entry1.index;
                // 获取键值
                object keyProperty1Value = GetPropertyValue(keyProperty1);

                if (keyProperty1Value == null)
                {
                    var valueProperty1 = entry1.valueProperty;
                    // 保存属性
                    SaveProperty(keyProperty1, valueProperty1, i, -1, conflictState);
                    // 删除该空键项
                    DeleteArrayElementAtIndex(keyArrayProperty, i);
                    if (valueArrayProperty != null)
                        DeleteArrayElementAtIndex(valueArrayProperty, i);

                    break;
                }

                // 检查键冲突
                foreach (var entry2 in EnumerateEntries(keyArrayProperty, valueArrayProperty, i + 1))
                {
                    // 其他键
                    var keyProperty2 = entry2.keyProperty;
                    int j = entry2.index;
                    object keyProperty2Value = GetPropertyValue(keyProperty2);

                    if (ComparePropertyValues(keyProperty1Value, keyProperty2Value))
                    {
                        var valueProperty2 = entry2.valueProperty;
                        SaveProperty(keyProperty2, valueProperty2, j, i, conflictState);
                        DeleteArrayElementAtIndex(keyArrayProperty, j);
                        if (valueArrayProperty != null)
                            DeleteArrayElementAtIndex(valueArrayProperty, j);

                        goto breakLoops;
                    }
                }
            }

            // 跳出所有循环
            breakLoops:
            // 结束属性绘制
            EditorGUI.EndProperty();
        }

        // 绘制一行键和值
        static float DrawKeyValueLine(SerializedProperty keyProperty, SerializedProperty valueProperty,
            Rect linePosition, int index)
        {
            // 判断键是否可展开
            bool keyCanBeExpanded = CanPropertyBeExpanded(keyProperty);

            if (valueProperty != null)
            {
                // 值是否可展开
                bool valueCanBeExpanded = CanPropertyBeExpanded(valueProperty);
                // 根据展开条件调用不同绘制方法
                if (!keyCanBeExpanded && valueCanBeExpanded)
                {
                    return DrawKeyValueLineExpand(keyProperty, valueProperty, linePosition);
                }
                else
                {
                    var keyLabel = keyCanBeExpanded ? ("Key " + index.ToString()) : "";
                    var valueLabel = valueCanBeExpanded ? ("Value " + index.ToString()) : "";
                    return DrawKeyValueLineSimple(keyProperty, valueProperty, keyLabel, valueLabel, linePosition);
                }
            }
            else
            {
                // 无值情况
                if (!keyCanBeExpanded)
                {
                    return DrawKeyLine(keyProperty, linePosition, null);
                }
                else
                {
                    var keyLabel = string.Format("{0} {1}", ObjectNames.NicifyVariableName(keyProperty.type), index);
                    return DrawKeyLine(keyProperty, linePosition, keyLabel);
                }
            }
        }

        // 简单绘制键和值
        static float DrawKeyValueLineSimple(SerializedProperty keyProperty, SerializedProperty valueProperty,
            string keyLabel, string valueLabel, Rect linePosition)
        {
            // 备份标签宽度
            float labelWidth = EditorGUIUtility.labelWidth;

            // 计算标签宽度占比
            float labelWidthRelative = labelWidth / linePosition.width;

            // 计算键高度
            float keyPropertyHeight = EditorGUI.GetPropertyHeight(keyProperty);

            // 绘制键位置
            var keyPosition = linePosition;
            keyPosition.height = keyPropertyHeight;

            // 键位置宽度缩进一定宽度
            keyPosition.width = labelWidth - IndentWidth;

            // 设置键标签宽度
            EditorGUIUtility.labelWidth = keyPosition.width * labelWidthRelative;

            // 绘制键
            EditorGUI.PropertyField(keyPosition, keyProperty, TempContent(keyLabel), true);

            // 计算值高度
            float valuePropertyHeight = EditorGUI.GetPropertyHeight(valueProperty);

            // 绘制值位置  
            var valuePosition = linePosition;
            valuePosition.height = valuePropertyHeight;

            // 值位置x起点设为标签宽度
            valuePosition.xMin += labelWidth;

            // 设置值标签宽度
            EditorGUIUtility.labelWidth = valuePosition.width * labelWidthRelative;

            // 缩进级别减1
            EditorGUI.indentLevel--;

            // 绘制值
            EditorGUI.PropertyField(valuePosition, valueProperty, TempContent(valueLabel), true);

            // 缩进级别加1
            EditorGUI.indentLevel++;

            // 恢复标签宽度
            EditorGUIUtility.labelWidth = labelWidth;

            // 返回行高
            return Mathf.Max(keyPropertyHeight, valuePropertyHeight);
        }

        // 展开式绘制键和值
        static float DrawKeyValueLineExpand(SerializedProperty keyProperty, SerializedProperty valueProperty,
            Rect linePosition)
        {
            // 备份标签宽度
            float labelWidth = EditorGUIUtility.labelWidth;

            // 计算键高度
            float keyPropertyHeight = EditorGUI.GetPropertyHeight(keyProperty);

            // 绘制键
            var keyPosition = linePosition;
            keyPosition.height = keyPropertyHeight;
            keyPosition.width = labelWidth - IndentWidth;
            EditorGUI.PropertyField(keyPosition, keyProperty, GUIContent.none, true);

            // 计算值高度

            float valuePropertyHeight = EditorGUI.GetPropertyHeight(valueProperty);

            // 绘制值
            var valuePosition = linePosition;
            valuePosition.height = valuePropertyHeight;
            EditorGUI.PropertyField(valuePosition, valueProperty, GUIContent.none, true);

            // 恢复标签宽度
            EditorGUIUtility.labelWidth = labelWidth;

            // 返回行高
            return Mathf.Max(keyPropertyHeight, valuePropertyHeight);
        }

        // 绘制键行
        static float DrawKeyLine(SerializedProperty keyProperty, Rect linePosition, string keyLabel)
        {
            // 计算键高度
            float keyPropertyHeight = EditorGUI.GetPropertyHeight(keyProperty);

            // 绘制键
            var keyPosition = linePosition;
            keyPosition.height = keyPropertyHeight;
            keyPosition.width = linePosition.width;

            // 内容
            var keyLabelContent = keyLabel != null ? TempContent(keyLabel) : GUIContent.none;

            // 绘制键
            EditorGUI.PropertyField(keyPosition, keyProperty, keyLabelContent, true);

            return keyPropertyHeight;
        }

        // 属性是否可展开
        static bool CanPropertyBeExpanded(SerializedProperty property)
        {
            // 枚举可展开的属性类型
            switch (property.propertyType)
            {
                case SerializedPropertyType.Generic:
                case SerializedPropertyType.Vector4:
                case SerializedPropertyType.Quaternion:
                    return true;
                default:
                    return false;
            }
        }

        // 保存属性到冲突状态
        static void SaveProperty(SerializedProperty keyProperty, SerializedProperty valueProperty, int index,
            int otherIndex, ConflictState conflictState)
        {
            // 保存键和值

            // 保存行高、索引等状态
            conflictState.conflictKey = GetPropertyValue(keyProperty);
            conflictState.conflictValue = valueProperty != null ? GetPropertyValue(valueProperty) : null;
            float keyPropertyHeight = EditorGUI.GetPropertyHeight(keyProperty);
            float valuePropertyHeight = valueProperty != null ? EditorGUI.GetPropertyHeight(valueProperty) : 0f;
            float lineHeight = Mathf.Max(keyPropertyHeight, valuePropertyHeight);
            conflictState.conflictLineHeight = lineHeight;
            conflictState.conflictIndex = index;
            conflictState.conflictOtherIndex = otherIndex;
            conflictState.conflictKeyPropertyExpanded = keyProperty.isExpanded;
            conflictState.conflictValuePropertyExpanded = valueProperty != null ? valueProperty.isExpanded : false;
        }

        // 行高
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float propertyHeight = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded)
            {
                var keysProperty = property.FindPropertyRelative(KeysFieldName);
                var valuesProperty = property.FindPropertyRelative(ValuesFieldName);

                foreach (var entry in EnumerateEntries(keysProperty, valuesProperty))
                {
                    var keyProperty = entry.keyProperty;
                    var valueProperty = entry.valueProperty;
                    float keyPropertyHeight = EditorGUI.GetPropertyHeight(keyProperty);
                    float valuePropertyHeight = valueProperty != null ? EditorGUI.GetPropertyHeight(valueProperty) : 0f;
                    float lineHeight = Mathf.Max(keyPropertyHeight, valuePropertyHeight);
                    propertyHeight += lineHeight;
                }

                ConflictState conflictState = GetConflictState(property);

                if (conflictState.conflictIndex != -1)
                {
                    propertyHeight += conflictState.conflictLineHeight;
                }
            }

            return propertyHeight;
        }

        static ConflictState GetConflictState(SerializedProperty property)
        {
            ConflictState conflictState;
            PropertyIdentity propId = new PropertyIdentity(property);
            if (!s_conflictStateDict.TryGetValue(propId, out conflictState))
            {
                conflictState = new ConflictState();
                s_conflictStateDict.Add(propId, conflictState);
            }

            return conflictState;
        }

        // 序列化属性值访问器
        static Dictionary<SerializedPropertyType, PropertyInfo> s_serializedPropertyValueAccessorsDict;

        // 静态构造函数
        static SerializableDictionaryPropertyDrawer()
        {
            // 属性值访问器名映射表
            Dictionary<SerializedPropertyType, string> serializedPropertyValueAccessorsNameDict =
                new Dictionary<SerializedPropertyType, string>()
                {
                    { SerializedPropertyType.Integer, "intValue" },
                    { SerializedPropertyType.Boolean, "boolValue" },
                    { SerializedPropertyType.Float, "floatValue" },
                    { SerializedPropertyType.String, "stringValue" },
                    { SerializedPropertyType.Color, "colorValue" },
                    { SerializedPropertyType.ObjectReference, "objectReferenceValue" },
                    { SerializedPropertyType.LayerMask, "intValue" },
                    { SerializedPropertyType.Enum, "intValue" },
                    { SerializedPropertyType.Vector2, "vector2Value" },
                    { SerializedPropertyType.Vector3, "vector3Value" },
                    { SerializedPropertyType.Vector4, "vector4Value" },
                    { SerializedPropertyType.Rect, "rectValue" },
                    { SerializedPropertyType.ArraySize, "intValue" },
                    { SerializedPropertyType.Character, "intValue" },
                    { SerializedPropertyType.AnimationCurve, "animationCurveValue" },
                    { SerializedPropertyType.Bounds, "boundsValue" },
                    { SerializedPropertyType.Quaternion, "quaternionValue" },
                };
            // 获取 SerializedProperty 类型
            Type serializedPropertyType = typeof(SerializedProperty);
            // 创建访问器字典
            s_serializedPropertyValueAccessorsDict = new Dictionary<SerializedPropertyType, PropertyInfo>();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

            foreach (var kvp in serializedPropertyValueAccessorsNameDict)
            {
                // 通过反射获取访问器属性
                PropertyInfo propertyInfo = serializedPropertyType.GetProperty(kvp.Value, flags);
                s_serializedPropertyValueAccessorsDict.Add(kvp.Key, propertyInfo);
            }
        }


        // 创建图标内容
        static GUIContent IconContent(string name, string tooltip)
        {
            // 获取内置图标
            var builtinIcon = EditorGUIUtility.IconContent(name);

            // 返回包装的图标内容
            return new GUIContent(builtinIcon.image, tooltip);
        }

        // 创建临时文本内容
        static GUIContent TempContent(string text)
        {
            // 设置临时内容文本
            s_tempContent.text = text;

            // 返回临时内容
            return s_tempContent;
        }

        // 根据索引删除数组元素
        static void DeleteArrayElementAtIndex(SerializedProperty arrayProperty, int index)
        {
            // 获取要删除的元素属性
            var property = arrayProperty.GetArrayElementAtIndex(index);

            // 如果是对象引用类型
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                // 将其设置为null 
                property.objectReferenceValue = null;
            }

            // 删除该元素
            arrayProperty.DeleteArrayElementAtIndex(index);
        }

        // 获取属性值
        public static object GetPropertyValue(SerializedProperty p)
        {
            PropertyInfo propertyInfo;

            // 如果存在专门的访问器,则使用访问器
            if (s_serializedPropertyValueAccessorsDict.TryGetValue(p.propertyType, out propertyInfo))
            {
                return propertyInfo.GetValue(p, null);
            }
            else
            {
                // 数组类型递归读取
                if (p.isArray)
                    return GetPropertyValueArray(p);
                // 字典类型递归读取
                else
                    return GetPropertyValueGeneric(p);
            }
        }

        // 设置属性值
        static void SetPropertyValue(SerializedProperty p, object v)
        {
            PropertyInfo propertyInfo;

            // 如果存在专门的访问器,则使用访问器
            if (s_serializedPropertyValueAccessorsDict.TryGetValue(p.propertyType, out propertyInfo))
            {
                propertyInfo.SetValue(p, v, null);
            }
            else
            {
                // 数组类型递归设置
                if (p.isArray)
                    SetPropertyValueArray(p, v);

                // 字典类型递归设置
                else
                    SetPropertyValueGeneric(p, v);
            }
        }

        // 读取数组类型属性值
        static object GetPropertyValueArray(SerializedProperty property)
        {
            // 创建同类型对象数组
            object[] array = new object[property.arraySize];

            // 递归读取每个元素
            for (int i = 0; i < property.arraySize; i++)
            {
                SerializedProperty item = property.GetArrayElementAtIndex(i);
                array[i] = GetPropertyValue(item);
            }

            return array;
        }

        // 读取字典类型属性值
        static object GetPropertyValueGeneric(SerializedProperty property)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            // 复制迭代器
            var iterator = property.Copy();

            // 进入子属性
            if (iterator.Next(true))
            {
                // 获得终点属性
                var end = property.GetEndProperty();
                // 递归读取每个子属性
                do
                {
                    string name = iterator.name;
                    object value = GetPropertyValue(iterator);
                    dict.Add(name, value);
                } while (iterator.Next(false) && iterator.propertyPath != end.propertyPath);
            }

            return dict;
        }

        // 设置数组类型属性值
        static void SetPropertyValueArray(SerializedProperty property, object v)
        {
            object[] array = (object[])v;
            property.arraySize = array.Length;
            for (int i = 0; i < property.arraySize; i++)
            {
                SerializedProperty item = property.GetArrayElementAtIndex(i);
                SetPropertyValue(item, array[i]);
            }
        }

        // 设置字典类型属性值
        static void SetPropertyValueGeneric(SerializedProperty property, object v)
        {
            Dictionary<string, object> dict = (Dictionary<string, object>)v;
            var iterator = property.Copy();
            if (iterator.Next(true))
            {
                var end = property.GetEndProperty();
                do
                {
                    string name = iterator.name;
                    SetPropertyValue(iterator, dict[name]);
                } while (iterator.Next(false) && iterator.propertyPath != end.propertyPath);
            }
        }

        // 比较属性值
        static bool ComparePropertyValues(object value1, object value2)
        {
            if (value1 is Dictionary<string, object> && value2 is Dictionary<string, object>)
            {
                var dict1 = (Dictionary<string, object>)value1;
                var dict2 = (Dictionary<string, object>)value2;
                return CompareDictionaries(dict1, dict2);
            }
            else
            {
                return object.Equals(value1, value2);
            }
        }

        // 比较两个字典
        static bool CompareDictionaries(Dictionary<string, object> dict1, Dictionary<string, object> dict2)
        {
            if (dict1.Count != dict2.Count)
                return false;

            foreach (var kvp1 in dict1)
            {
                var key1 = kvp1.Key;
                object value1 = kvp1.Value;

                object value2;
                if (!dict2.TryGetValue(key1, out value2))
                    return false;

                if (!ComparePropertyValues(value1, value2))
                    return false;
            }

            return true;
        }

        // 遍历条目结构
        struct EnumerationEntry
        {
            public SerializedProperty keyProperty;
            public SerializedProperty valueProperty;
            public int index;

            public EnumerationEntry(SerializedProperty keyProperty, SerializedProperty valueProperty, int index)
            {
                this.keyProperty = keyProperty;
                this.valueProperty = valueProperty;
                this.index = index;
            }
        }

        // 遍历函数
        static IEnumerable<EnumerationEntry> EnumerateEntries(SerializedProperty keyArrayProperty,
            SerializedProperty valueArrayProperty, int startIndex = 0)
        {
            if (keyArrayProperty.arraySize > startIndex)
            {
                int index = startIndex;
                var keyProperty = keyArrayProperty.GetArrayElementAtIndex(startIndex);
                var valueProperty = valueArrayProperty != null
                    ? valueArrayProperty.GetArrayElementAtIndex(startIndex)
                    : null;
                var endProperty = keyArrayProperty.GetEndProperty();

                do
                {
                    yield return new EnumerationEntry(keyProperty, valueProperty, index);
                    index++;
                } while (keyProperty.Next(false)
                         && (valueProperty != null ? valueProperty.Next(false) : true)
                         && !SerializedProperty.EqualContents(keyProperty, endProperty));
            }
        }
    }

    // 存储属性绘制器
    [CustomPropertyDrawer(typeof(SerializableDictionaryBase.Storage), true)]
    public class SerializableDictionaryStoragePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.Next(true);
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            property.Next(true);
            return EditorGUI.GetPropertyHeight(property);
        }
    }
}