// Compatibility shim for projects that contain the Control Freak 2 runtime
// scripts without the optional editor tooling folder.
//
// Unity Build Automation compiles UNITY_EDITOR blocks while importing the
// project. The original Control Freak 2 runtime sources reference optional
// editor helpers from those blocks, so provide harmless editor-only stubs.
// None of this code is included in the Android player.

#if UNITY_EDITOR
using System;
using UnityEngine;

namespace ControlFreak2Editor
{
    internal sealed class CFEditorStyles
    {
        private static readonly CFEditorStyles instance = new CFEditorStyles();
        public static CFEditorStyles Inst => instance;

        public Texture2D texFinger;
        public Texture2D texPinchHint;
        public Texture2D texTwistHint;
    }

    internal static class Assistant
    {
        public static void CaptureAxis(string name) { }
        public static void CaptureButton(string name) { }
        public static void CaptureKey(KeyCode key) { }
        public static void CaptureTouch() { }
        public static void CaptureMousePos() { }
        public static void CaptureScrollWheel() { }
        public static void CaptureCursorLock() { }
    }

    internal static class CFEditorUtils
    {
        public static void AddOnHierarchyChange(Action callback) { }
        public static void RemoveOnHierarchyChange(Action callback) { }
    }

    internal static class CFGUI
    {
        public static void CreateUndo(params object[] args) { }
        public static void EndUndo(params object[] args) { }
    }

    internal static class UnityInputManagerToRigDialog
    {
        public static void ShowDialog(object rig) { }
    }

    internal static class TouchControlWizardUtils
    {
        public static ControlFreak2.TouchControlPanel GetRigPanel(object rig) => null;

        // Control Freak 2 expects an integer count here (0 means no EventSystem).
        public static int IsThereEventSystemInTheScene()
        {
            return UnityEngine.Object.FindObjectsOfType<UnityEngine.EventSystems.EventSystem>().Length;
        }

        public static Sprite GetDefaultSuperTouchZoneSprite(string controlName) => null;
        public static Sprite GetDefaultAnalogJoyHatSprite(string controlName) => null;
        public static Sprite GetDefaultButtonSprite(string controlName) => null;
        public static Sprite GetDefaultTrackPadSprite(string controlName) => null;
        public static Sprite GetDefaultWheelSprite(string controlName) => null;

        public static void CreateSuperTouchZoneAnimator(params object[] args) { }
        public static void CreateTouchJoystickSimpleAnimator(params object[] args) { }
        public static void CreateButtonAnimator(params object[] args) { }
        public static void CreateTouchTrackPadAnimator(params object[] args) { }
        public static void CreateWheelAnimator(params object[] args) { }
    }
}
#endif
