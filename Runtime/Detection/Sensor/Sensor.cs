using HyperGnosys.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    [Serializable]
    public class Sensor
    {
        [SerializeField] private List<InteractionType> interactionTypes = new List<InteractionType>();
        [SerializeField] private List<ADetectionModifier> modifiers = new List<ADetectionModifier>();
        public List<InteractionType> Sense(Transform detectorTransform, InteractableTag interactable, bool debugging)
        {
            (bool, List<InteractionType>) matches = InteractionTypeUtilities.InteractionMatches(interactionTypes, interactable);
            if (!matches.Item1)
            {
                HGDebug.Log($"{interactable.name} was not detectable through this Detector's Interaction Types", detectorTransform, debugging);
                return null;
            }
            foreach (ADetectionModifier detectionModifier in modifiers)
            {
                if (detectionModifier == null) continue;
                if (!detectionModifier.ModifyDetection(detectorTransform, interactable))
                {
                    return null;
                }
            }
            return interactionTypes;
        }
    }
}