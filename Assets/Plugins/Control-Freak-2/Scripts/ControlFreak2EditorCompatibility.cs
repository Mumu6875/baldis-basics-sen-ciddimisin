// Compatibility shim for projects that contain the Control Freak 2 runtime
// scripts without the optional editor tooling folder.
//
// The original runtime scripts reference a few ControlFreak2Editor helpers
// inside UNITY_EDITOR blocks. Unity Build Automation compiles those blocks
// while importing the project, so a missing editor package otherwise causes
// CS0246 before the Android player build can start.

#if UNITY_EDITOR
using UnityEngine;

namespace ControlFreak2Editor
{
    internal sealed class CFEditorStyles
    {
        private static readonly CFEditorStyles instance = new CFEditorStyles();

        public static CFEditorStyles Inst => instance;

        // These textures are only fallbacks for editor-only touch markers.
        // Runtime/mobile input does not depend on them.
        public Texture2D texFinger;
        public Texture2D texPinchHint;
        public Texture2D texTwistHint;
    }

    internal static class UnityInputManagerToRigDialog
    {
        public static void ShowDialog(object rig)
        {
            // Optional Control Freak editor utility is not included in this project.
        }
    }

    internal static class TouchControlWizardUtils
    {
        public static ControlFreak2.TouchControlPanel GetRigPanel(object rig)
        {
            return null;
        }

        public static Sprite GetDefaultSuperTouchZoneSprite(string controlName)
        {
            return null;
        }

        public static void CreateSuperTouchZoneAnimator(
            object zone,
            string suffix,
            Sprite sprite,
            int layer,
            string undoName)
        {
            // Optional editor-only wizard. No-op when editor tooling is absent.
        }
    }
}
#endif
