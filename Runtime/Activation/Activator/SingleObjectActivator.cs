using HyperGnosys.Core;

namespace HyperGnosys.InteractionModule
{
    public class SingleObjectActivator : Activator
    {
        private Activable currentActivable;
        public override void DetectActivable(Activable newActivable)
        {
            HGDebug.Log($"Current Activable: {currentActivable?.gameObject.name}", Debugging);
            HGDebug.Log($"New Activable: {newActivable.gameObject.name}", Debugging);
            if(currentActivable == newActivable)
            {
                return;
            }
            else
            {
                if(currentActivable != null)
                {
                    UndetectActivable(currentActivable);
                }
                currentActivable = newActivable;
                this.OnActivableDetected.Raise(currentActivable);
            }
        }
        public override void UndetectActivable(Activable activable)
        {
            if(currentActivable == activable)
            {
                this.OnActivableUndetected.Raise(currentActivable);
                currentActivable = null;
            }
        }
        public override void Activate(bool isActivateInputDown)
        {
            HGDebug.Log("Attempting Activation", Debugging);
            if (currentActivable != null && isActivateInputDown)
            {
                HGDebug.Log("Activating", Debugging);
                currentActivable.Activate(this);
            }
        }
    }
}