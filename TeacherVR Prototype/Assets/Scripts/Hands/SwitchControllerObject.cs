using System;
using System.Collections;

namespace VRTK.Examples
{
    using UnityEngine;

    public class SwitchControllerObject : VRTK_InteractableObject
    {
        [Header("SwitchControllerObject Settings")]
        public CollisionDetectionMode CollisionDetectionMode = CollisionDetectionMode.Continuous;

        public bool AutoHoldOnGrab = true;
        public ColliderType UseOnAwake = ColliderType.Ignore;

        public enum ColliderType
        {
            AllToTrigger,
            MainToTrigger,
            FirstChildToTrigger,
            Ignore
        }

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

            if (UseOnAwake == ColliderType.AllToTrigger) SetAllColliders(transform, false);
            if (UseOnAwake == ColliderType.MainToTrigger) SetMainCollider(false);
            if (UseOnAwake == ColliderType.FirstChildToTrigger) SetFirstChildCollider(false);
        }

        public override void StartTouching(VRTK_InteractTouch currentTouchingObject = null)
        {
            base.StartTouching(currentTouchingObject);
            if (AutoHoldOnGrab)
            {
                VRTK_InteractGrab myGrab = currentTouchingObject.GetComponent<VRTK_InteractGrab>();
                myGrab.AttemptGrab();
               // transform.localPosition = Vector3.zero;
            }
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
        {
            base.Ungrabbed(previousGrabbingObject);
            if (!AutoHoldOnGrab)
            {
                Destroy(gameObject);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            interactableRigidbody.collisionDetectionMode = CollisionDetectionMode;
            if (UseOnAwake == ColliderType.AllToTrigger) SetAllColliders(transform, true);
            if (UseOnAwake == ColliderType.MainToTrigger) SetMainCollider(true);
            if (UseOnAwake == ColliderType.FirstChildToTrigger) SetFirstChildCollider(true);
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
        
        private void SetAllColliders(Transform root, bool val)
        {
            var col = root.GetComponent<Collider>();
            if (col != null) col.isTrigger = val;
            foreach (Transform child in root.transform)
            {
                SetAllColliders(child, val);
            }
        }

        private void SetMainCollider(bool val)
        {
            var col = GetComponent<Collider>();
            if (col != null) col.isTrigger = val;
        }

        private void SetFirstChildCollider(bool val)
        {
            var col = transform.GetChild(0).GetComponent<Collider>();
            if (col != null) col.isTrigger = val;
        }
    }
}