using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnitySwitchEditor.Editor
{
    /// <summary>
    /// User settings for the EditorOnly / Active Hierarchy controls, persisted
    /// via <see cref="EditorPrefs"/> and editable from Edit &gt; Preferences &gt;
    /// EditorOnly Switch. Each control has its own enable flag and offset.
    /// </summary>
    internal static class EditorOnlySettings
    {
        const string EditorOnlyEnabledKey = "UnitySwitchEditor.EditorOnly.Enabled";
        const string TintKey = "UnitySwitchEditor.EditorOnly.Tint";
        const string EditorOnlyOffsetKey = "UnitySwitchEditor.EditorOnly.Offset";
        const string ActiveEnabledKey = "UnitySwitchEditor.Active.Enabled";
        const string ActiveOffsetKey = "UnitySwitchEditor.Active.Offset";

        public static readonly Color DefaultTint = new Color(1f, 0.5f, 0f, 0.12f);
        public const float DefaultEditorOnlyOffset = 50f;
        public const float DefaultActiveOffset = 68f;
        public const float MaxOffset = 256f;

        public static bool EditorOnlyEnabled
        {
            get => EditorPrefs.GetBool(EditorOnlyEnabledKey, true);
            set => EditorPrefs.SetBool(EditorOnlyEnabledKey, value);
        }

        public static Color Tint
        {
            get
            {
                string html = EditorPrefs.GetString(TintKey, string.Empty);
                if (!string.IsNullOrEmpty(html) &&
                    ColorUtility.TryParseHtmlString("#" + html, out Color color))
                    return color;
                return DefaultTint;
            }
            set => EditorPrefs.SetString(TintKey, ColorUtility.ToHtmlStringRGBA(value));
        }

        /// <summary>Offset of the EditorOnly checkbox from the row's right edge, in pixels.</summary>
        public static float EditorOnlyOffset
        {
            get => Mathf.Clamp(EditorPrefs.GetFloat(EditorOnlyOffsetKey, DefaultEditorOnlyOffset), 0f, MaxOffset);
            set => EditorPrefs.SetFloat(EditorOnlyOffsetKey, Mathf.Clamp(value, 0f, MaxOffset));
        }

        public static bool ActiveEnabled
        {
            get => EditorPrefs.GetBool(ActiveEnabledKey, true);
            set => EditorPrefs.SetBool(ActiveEnabledKey, value);
        }

        /// <summary>Offset of the Active eye icon from the row's right edge, in pixels.</summary>
        public static float ActiveOffset
        {
            get => Mathf.Clamp(EditorPrefs.GetFloat(ActiveOffsetKey, DefaultActiveOffset), 0f, MaxOffset);
            set => EditorPrefs.SetFloat(ActiveOffsetKey, Mathf.Clamp(value, 0f, MaxOffset));
        }

        [SettingsProvider]
        static SettingsProvider CreateProvider()
        {
            return new SettingsProvider("Preferences/EditorOnly Switch", SettingsScope.User)
            {
                label = "EditorOnly Switch",
                guiHandler = _ =>
                {
                    EditorGUILayout.LabelField("EditorOnly (checkbox)", EditorStyles.boldLabel);
                    ToggleRow("Enable", () => EditorOnlyEnabled, v => EditorOnlyEnabled = v);

                    using (new EditorGUI.DisabledScope(!EditorOnlyEnabled))
                    {
                        ColorFieldRow("Row Tint", () => Tint, v => Tint = v);
                        SliderRow("Right Offset", () => EditorOnlyOffset, v => EditorOnlyOffset = v);
                    }

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Active (eye icon)", EditorStyles.boldLabel);
                    ToggleRow("Enable", () => ActiveEnabled, v => ActiveEnabled = v);

                    using (new EditorGUI.DisabledScope(!ActiveEnabled))
                        SliderRow("Right Offset", () => ActiveOffset, v => ActiveOffset = v);

                    EditorGUILayout.Space();
                    if (GUILayout.Button("Reset to Default", GUILayout.Width(140f)))
                    {
                        EditorOnlyEnabled = true;
                        Tint = DefaultTint;
                        EditorOnlyOffset = DefaultEditorOnlyOffset;
                        ActiveEnabled = true;
                        ActiveOffset = DefaultActiveOffset;
                        EditorApplication.RepaintHierarchyWindow();
                    }
                },
                keywords = new HashSet<string>
                {
                    "EditorOnly", "Active", "Hierarchy", "Tint", "Color", "Offset", "Position", "Toggle", "Enabled"
                }
            };
        }

        static void ToggleRow(string label, System.Func<bool> get, System.Action<bool> set)
        {
            EditorGUI.BeginChangeCheck();
            bool v = EditorGUILayout.Toggle(label, get());
            if (EditorGUI.EndChangeCheck())
            {
                set(v);
                EditorApplication.RepaintHierarchyWindow();
            }
        }

        static void ColorFieldRow(string label, System.Func<Color> get, System.Action<Color> set)
        {
            EditorGUI.BeginChangeCheck();
            Color v = EditorGUILayout.ColorField(label, get());
            if (EditorGUI.EndChangeCheck())
            {
                set(v);
                EditorApplication.RepaintHierarchyWindow();
            }
        }

        static void SliderRow(string label, System.Func<float> get, System.Action<float> set)
        {
            EditorGUI.BeginChangeCheck();
            float v = EditorGUILayout.Slider(label, get(), 0f, MaxOffset);
            if (EditorGUI.EndChangeCheck())
            {
                set(v);
                EditorApplication.RepaintHierarchyWindow();
            }
        }
    }
}
