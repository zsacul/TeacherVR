using System;
using System.Collections;

namespace VRTK.Examples
{
    using UnityEngine;

    public class SwitchControllerObject : VRTK_InteractableObject
    {
        [Header("SwitchControllerObject Settings")]
        
        public CollisionDetectionMode CollisionDetectionMode = CollisionDetectionMode.Continuous;
        public bool AutoGrab = true;

        protected VRTK_ControllerEvents ControllerEvents;

        public override void Grabbed(VRTK_InteractGrab grabbingObject)
        {
            base.Grabbed(grabbingObject);
            ControllerEvents = grabbingObject.controllerEvents;
            ControllerEvents.GripPressed += DestroyObject;
            ControllerEvents.StartMenuPressed += DestroyObject;
            ControllerEvents.TriggerPressed += UseObject;
            grabOverrideButton =
                VRTK_ControllerEvents.ButtonAlias
                    .StartMenuPress; //Bo kontroller sie pokazuje na ungrab jak sie zostawi domyslnie
        }

        public override void StartTouching(VRTK_InteractTouch currentTouchingObject = null)
        {
            base.StartTouching(currentTouchingObject);
            if (AutoGrab)
            {
                VRTK_InteractGrab myGrab = currentTouchingObject.GetComponent<VRTK_InteractGrab>();
                myGrab.AttemptGrab();
            }
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            base.Ungrabbed(previousGrabbingObject);
            if (!AutoGrab)
            {
                Destroy(gameObject);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            interactableRigidbody.collisionDetectionMode = CollisionDetectionMode;
        }

        protected virtual void DestroyObject(object sender, ControllerInteractionEventArgs e)
        {
            ForceReleaseGrab();
            Destroy(gameObject);
        }

        protected virtual void UseObject(object sender, ControllerInteractionEventArgs e)
        {
            StartUsing();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (ControllerEvents != null)
            {
                ControllerEvents.GripPressed -= DestroyObject;
                ControllerEvents.StartMenuPressed -= DestroyObject;
                ControllerEvents.TriggerPressed -= UseObject;
            }
        }
    }
}