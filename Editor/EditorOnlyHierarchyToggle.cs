using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnitySwitchEditor.Editor
{
    /// <summary>
    /// Draws two toggles at the right edge of every Hierarchy row:
    /// one switches the GameObject's tag between "EditorOnly" and "Untagged"
    /// (EditorOnly rows are tinted so their state is visible at a glance),
    /// the other enables/disables the GameObject (active state).
    /// </summary>
    [InitializeOnLoad]
    internal static class EditorOnlyHierarchyToggle
    {
        const string EditorOnlyTag = "EditorOnly";
        const string DefaultTag = "Untagged";
        const float ToggleWidth = 16f;

        static readonly GUIContent EditorOnlyTooltip = new GUIContent(string.Empty, "EditorOnly");

        static GUIStyle _iconStyle;

        static GUIStyle IconStyle
        {
            get
            {
                if (_iconStyle == null)
                {
                    _iconStyle = new GUIStyle(GUIStyle.none)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        padding = new RectOffset(0, 0, 0, 0)
                    };
                }
                return _iconStyle;
            }
        }

        static EditorOnlyHierarchyToggle()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnItemGUI;
        }

        static void OnItemGUI(int instanceID, Rect rect)
        {
            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (go == null)
                return;

            // EditorOnly: a checkbox (with a tinted row when tagged).
            if (EditorOnlySettings.EditorOnlyEnabled)
            {
                bool isEditorOnly = go.CompareTag(EditorOnlyTag);
                if (isEditorOnly)
                    EditorGUI.DrawRect(rect, EditorOnlySettings.Tint);

                var r = new Rect(rect.xMax - EditorOnlySettings.EditorOnlyOffset - ToggleWidth,
                    rect.y, ToggleWidth, rect.height);
                bool newEditorOnly = GUI.Toggle(r, isEditorOnly, EditorOnlyTooltip);
                if (newEditorOnly != isEditorOnly)
                    ApplyEditorOnly(go, newEditorOnly);
            }

            // Active: an eye icon (open when active, closed/grayed when inactive).
            if (EditorOnlySettings.ActiveEnabled)
            {
                bool isActive = go.activeSelf;
                var r = new Rect(rect.xMax - EditorOnlySettings.ActiveOffset - ToggleWidth,
                    rect.y, ToggleWidth, rect.height);
                var eye = EditorGUIUtility.IconContent(isActive
                    ? "animationvisibilitytoggleon"
                    : "animationvisibilitytoggleoff");
                var eyeContent = new GUIContent(eye.image, isActive ? "Active" : "Inactive");
                if (GUI.Button(r, eyeContent, IconStyle))
                    ApplyActive(go, !isActive);
            }
        }

        // Apply to the whole selection when the clicked object is part of it,
        // otherwise just to the clicked object.
        static GameObject[] ResolveTargets(GameObject clicked)
        {
            return ArrayUtility.Contains(Selection.gameObjects, clicked)
                ? Selection.gameObjects
                : new[] { clicked };
        }

        static void ApplyEditorOnly(GameObject clicked, bool editorOnly)
        {
            GameObject[] targets = ResolveTargets(clicked);
            string tag = editorOnly ? EditorOnlyTag : DefaultTag;

            Undo.RecordObjects(targets, "Toggle EditorOnly");
            foreach (var go in targets)
            {
                go.tag = tag;
                MarkDirty(go);
            }
        }

        static void ApplyActive(GameObject clicked, bool active)
        {
            GameObject[] targets = ResolveTargets(clicked);

            Undo.RecordObjects(targets, "Toggle Active");
            foreach (var go in targets)
            {
                go.SetActive(active);
                MarkDirty(go);
            }
        }

        static void MarkDirty(GameObject go)
        {
            EditorUtility.SetDirty(go);
            if (go.scene.IsValid())
                EditorSceneManager.MarkSceneDirty(go.scene);
        }
    }
}
