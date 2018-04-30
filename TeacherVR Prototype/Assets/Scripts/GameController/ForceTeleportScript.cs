namespace VRTK.Examples
{
    using UnityEngine;

    public class ForceTeleportScript : VRTK_DestinationMarker
    {
        public Transform GameStartDestinationPoint;
        public Transform GameSummaryDestinationPoint;

        public void ForceTeleportTo(Transform destination)
        {
            float distance = Vector3.Distance(transform.position, destination.position);
            VRTK_ControllerReference controllerReference =
                VRTK_ControllerReference.GetControllerReference(VRTK_DeviceFinder.GetControllerRightHand());
            OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(),
                destination.position, controllerReference));
        }

        public void ForceTeleportToStart()
        {
            ForceTeleportTo(GameStartDestinationPoint);
        }

        public void ForceTeleportToGameSummary()
        {
            ForceTeleportTo(GameSummaryDestinationPoint);
        }
    }
}