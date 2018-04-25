namespace VRTK.Examples
{
    using UnityEngine;

    public class WaterSpray : SwitchControllerObject
    {
        public float bulletSpeed = 200f;
        public float bulletLife = 5f;
        public float fireDelay = 0.5f;

        private float lastFire = 0f;

        private GameObject bullet;
        private GameObject trigger;

        private PourWater pourWater;

        private float minTriggerRotation = -10f;
        private float maxTriggerRotation = 45f;

        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
        {
            base.Grabbed(currentGrabbingObject);

            //Limit hands grabbing when picked up
            if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) ==
                SDK_BaseController.ControllerHand.Left)
            {
                allowedTouchControllers = AllowedController.LeftOnly;
                allowedUseControllers = AllowedController.LeftOnly;
            }
            else if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) ==
                     SDK_BaseController.ControllerHand.Right)
            {
                allowedTouchControllers = AllowedController.RightOnly;
                allowedUseControllers = AllowedController.RightOnly;
            }
        }

        public override void StartUsing(VRTK_InteractUse currentUsingObject)
        {
            base.StartUsing(currentUsingObject);
            if (Time.time > lastFire + fireDelay && pourWater.Use())
            {
                lastFire = Time.time;
                FireBullet();
                VRTK_ControllerHaptics.TriggerHapticPulse(
                    VRTK_ControllerReference.GetControllerReference(ce.gameObject), 0.63f, 0.2f, 0.01f);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            bullet = transform.Find("Bullet").gameObject;
            pourWater = GetComponentInChildren<PourWater>();
            bullet.SetActive(false);

            trigger = transform.Find("Head/Trigger").gameObject;
        }

        protected override void Update()
        {
            base.Update();
            if (ce)
            {
                var pressure = (maxTriggerRotation * ce.GetTriggerAxis()) - minTriggerRotation;
                trigger.transform.localEulerAngles = new Vector3(0f, 0f, pressure);
            }
            else
            {
                trigger.transform.localEulerAngles = new Vector3(0f, 0f, minTriggerRotation);
            }
        }

        private void FireBullet()
        {
            GameObject bulletClone =
                Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
            bulletClone.SetActive(true);
            Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed);
            Destroy(bulletClone, bulletLife);
        }
    }
}