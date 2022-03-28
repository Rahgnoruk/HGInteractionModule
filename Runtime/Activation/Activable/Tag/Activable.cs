using HyperGnosys.Core;
using UnityEngine;
using UnityEngine.Events;
namespace HyperGnosys.InteractionModule
{
    public class Activable : InteractableTag, IModuleComponent
    {
        [SerializeField] private string description = "Open Door";
        [SerializeField] private UnityEvent<ModuleComponentList> onActivate = new UnityEvent<ModuleComponentList>();
        public virtual void Activate(Activator activator)
        {
            onActivate.Invoke(activator.ModuleContainers);
        }
        public string Description { get => description; set => description = value; }
        public UnityEvent<ModuleComponentList> OnActivate { get => onActivate; set => onActivate = value; }
    }
}