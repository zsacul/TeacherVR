using System;
using System.Collections;

namespace VRTK.Examples
{
    using UnityEngine;

    public class Sword : VRTK_InteractableObject
    {
        public bool DestroyOnGripPress = false;
        private float impactMagnifier = 120f;
        private float collisionForce = 0f;
        private float maxCollisionForce = 4000f;
        private VRTK_ControllerReference controllerReference;
        private VRTK_ControllerEvents ce;
        private bool canTouch = true;

        public float CollisionForce()
        {
            return collisionForce;
        }

        public override void Grabbed(VRTK_InteractGrab grabbingObject)
        {
            base.Grabbed(grabbingObject);
            ce = grabbingObject.controllerEvents;
            controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);
            controllerReference = null;
        }

        public override void StartTouching(VRTK_InteractTouch currentTouchingObject = null)
        {
            base.StartTouching(currentTouchingObject);
            VRTK_InteractGrab myGrab = currentTouchingObject.GetComponent<VRTK_InteractGrab>();
            if(canTouch) myGrab.AttemptGrab();
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            controllerReference = null;
            interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        protected override void Update()
        {
            base.Update();
            if (ce.gripPressed)
            {
                ForceReleaseGrab();
                canTouch = false;
                if(DestroyOnGripPress)Destroy(gameObject);
                StartCoroutine(wait());
            }
        }

        private IEnumerator wait()
        {
            yield return new WaitForSeconds(1f);
            canTouch = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed())
            {
                collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude * impactMagnifier;
                var hapticStrength = collisionForce / maxCollisionForce;
                VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.5f, 0.01f);
            }
            else
            {
                collisionForce = collision.relativeVelocity.magnitude * impactMagnifier;
            }
        }
    }
}