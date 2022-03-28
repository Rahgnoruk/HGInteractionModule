using HyperGnosys.Core;
using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public abstract class Activator : MonoBehaviour
    {
        [SerializeField] private bool debugging = false;
        [SerializeField] private ModuleComponentList moduleContainers;
        [SerializeField] private ActivableEventProperty onActivableDetected = new ActivableEventProperty();
        [SerializeField] private ActivableEventProperty onActivableUndetected = new ActivableEventProperty();
        public abstract void DetectActivable(Activable newActivable);
        public abstract void UndetectActivable(Activable activable);
        public abstract void Activate(bool isActivateInputDown);
        public ModuleComponentList ModuleContainers { get => moduleContainers; set => moduleContainers = value; }
        public ActivableEventProperty OnActivableDetected { get => onActivableDetected; set => onActivableDetected = value; }
        public ActivableEventProperty OnActivableUndetected { get => onActivableUndetected; set => onActivableUndetected = value; }
        public bool Debugging { get => debugging; set => debugging = value; }
    }
}