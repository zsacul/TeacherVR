﻿using System;
using System.Collections;

namespace VRTK.Examples
{
    using UnityEngine;

    public class SwitchControllerObject : VRTK_InteractableObject
    {
        protected VRTK_ControllerEvents ce;

        public override void Grabbed(VRTK_InteractGrab grabbingObject)
        {
            base.Grabbed(grabbingObject);
            ce = grabbingObject.controllerEvents;
            ce.GripPressed += Hand_GripPressed;
            ce.TriggerPressed += Hand_TriggerPressed;
            grabOverrideButton =
                VRTK_ControllerEvents.ButtonAlias
                    .StartMenuPress; //Bo kontroller sie pokazuje na ungrab jak sie zostawi domyslnie
        }

        public override void StartTouching(VRTK_InteractTouch currentTouchingObject = null)
        {
            base.StartTouching(currentTouchingObject);
            VRTK_InteractGrab myGrab = currentTouchingObject.GetComponent<VRTK_InteractGrab>();
            myGrab.AttemptGrab();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        protected virtual void Hand_GripPressed(object sender, ControllerInteractionEventArgs e)
        {
            ForceReleaseGrab();
            Destroy(gameObject);
        }

        protected virtual void Hand_TriggerPressed(object sender, ControllerInteractionEventArgs e)
        {
            StartUsing();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (ce != null)
            {
                ce.GripPressed -= Hand_GripPressed;
                ce.TriggerPressed -= Hand_TriggerPressed;
            }
        }
    }
}