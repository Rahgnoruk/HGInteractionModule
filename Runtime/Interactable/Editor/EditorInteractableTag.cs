using HyperGnosys.Core;
using HyperGnosys.EditorUtilities;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace HyperGnosys.InteractionModule
{
    [CustomEditor(typeof(InteractableTag), true)]
    [CanEditMultipleObjects]
    public class EditorInteractableTag : Editor
    {
        private InteractableTag interactableTag;
        private const string INTERACTABLE_LAYER_NAME = "Interactable";
        private SerializedProperty isFirstReset;
        private void Reset()
        {
            interactableTag = (InteractableTag)target;
            isFirstReset = serializedObject.FindProperty("isFirstReset");
            if (isFirstReset.boolValue)
            {
                AssignInteractableLayer();
                isFirstReset.boolValue = false;
                serializedObject.ApplyModifiedProperties();
            }
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Reassign Interactable Layer"))
            {
                AssignInteractableLayer();
            }
        }
        private void AssignInteractableLayer()
        {
            Debug.LogWarning($"Assigning {INTERACTABLE_LAYER_NAME} Layer to {target.name}", 
                interactableTag.gameObject);
            LayerUtilities.CreateAndAssignLayer(interactableTag.gameObject, INTERACTABLE_LAYER_NAME);
        }
    }
}
#endif