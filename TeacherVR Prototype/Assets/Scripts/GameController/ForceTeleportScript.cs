namespace VRTK.Examples
{
    using UnityEngine;

    public class ForceTeleportScript : VRTK_DestinationMarker
    {
        public Transform GameStartDestinationPoint;
        public Transform GameSummaryDestinationPoint;

        public void ForceTeleportTo(Transform destination, Quaternion rotation)
        {
            float distance = Vector3.Distance(transform.position, destination.position);
            VRTK_ControllerReference controllerReference =
                VRTK_ControllerReference.GetControllerReference(VRTK_DeviceFinder.GetControllerRightHand());
            OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(),
                destination.position, controllerReference, false, rotation));
        }

        public void ForceTeleportToStart()
        {
            ForceTeleportTo(GameStartDestinationPoint, GameStartDestinationPoint.rotation);
        }

        public void ForceTeleportToGameSummary()
        {
            Quaternion rot = GameSummaryDestinationPoint.rotation;
            rot.eulerAngles = new Vector3(0,90,0);
            ForceTeleportTo(GameSummaryDestinationPoint, rot);
        }
    }
}