namespace VRTK.Examples
{
    using UnityEngine;

    public class ModelVillage_TeleportLocation : VRTK_DestinationMarker
    {
        public Transform destination;
        private bool lastUsePressedState = false;

        private void OnTriggerStay(Collider collider)
        {
            VRTK_ControllerEvents controller = (collider.GetComponent<VRTK_ControllerEvents>() ? collider.GetComponent<VRTK_ControllerEvents>() : collider.GetComponentInParent<VRTK_ControllerEvents>());
            if (controller != null)
            {
                if (lastUsePressedState == true && !controller.triggerPressed)
                {
                    float distance = Vector3.Distance(transform.position, destination.position);
                    VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller.gameObject);
                    OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerReference));
                }
                lastUsePressedState = controller.triggerPressed;
            }
        }

        public void ForceTeleport()
        {
            float distance = Vector3.Distance(transform.position, destination.position);
            VRTK_ControllerReference controllerReference =
                VRTK_ControllerReference.GetControllerReference(VRTK_DeviceFinder.GetControllerRightHand()
                    .GetComponent<VRTK_ControllerEvents>().gameObject);
            OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerReference));
        }

        public void ForceTeleportTo(Transform location)
        {
            float distance = Vector3.Distance(transform.position, location.position);
            VRTK_ControllerReference controllerReference =
                VRTK_ControllerReference.GetControllerReference(VRTK_DeviceFinder.GetControllerRightHand()
                    .GetComponent<VRTK_ControllerEvents>().gameObject);
            OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, location, new RaycastHit(), location.position, controllerReference));
        }
    }
}