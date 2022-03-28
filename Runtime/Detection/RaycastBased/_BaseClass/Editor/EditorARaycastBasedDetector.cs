using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace HyperGnosys.InteractionModule
{
    [CustomEditor(typeof(ARaycastBasedDetector), true)]
    [CanEditMultipleObjects]
    public class EditorARaycastBasedDetector : Editor
    {
        private const string TARGET_LAYER_NAME = "Interactable";
        private ARaycastBasedDetector raycastBasedDetector;
        private SerializedProperty isFirstReset;
        public void Reset()
        {
            raycastBasedDetector = (ARaycastBasedDetector)target;
            isFirstReset = serializedObject.FindProperty("isFirstReset");
            if (isFirstReset.boolValue)
            {
                isFirstReset.boolValue = false;
                serializedObject.ApplyModifiedProperties();
                raycastBasedDetector.TargetLayerMask = LayerMask.GetMask(TARGET_LAYER_NAME);
            }
        }
    }
}
#endif