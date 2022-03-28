using UnityEngine;
using HyperGnosys.Core;
namespace HyperGnosys.InteractionModule
{
    public class ActivableReceiver : MonoBehaviour
    {
        [SerializeField] private bool debugging = false;
        [SerializeField] private Activator activator;
        public void DetectActivable(ModuleComponentList moduleComponents)
        {
            HGDebug.Log("Detecting Activable", debugging);
            foreach (MonoBehaviour moduleComponent in moduleComponents.ModuleComponents)
            {
                Activable activable = (Activable)moduleComponent;
                if (activable)
                {
                    HGDebug.Log($"Found activable: {activable.gameObject.name}", debugging);
                    activator.DetectActivable(activable);
                }
            }
        }
        public void UndetectActivable(ModuleComponentList moduleComponents)
        {
            HGDebug.Log("Undetecting Activable", debugging);
            foreach (MonoBehaviour moduleComponent in moduleComponents.ModuleComponents)
            {
                Activable activable = (Activable)moduleComponent;
                if (activable != null)
                {
                    HGDebug.Log($"Found activable: {activable.gameObject.name}", debugging);
                    activator.UndetectActivable(activable);
                }
            }
        }
        public bool Debugging { get => debugging; set => debugging = value; }
        public Activator Activator { get => activator; set => activator = value; }
    }
}