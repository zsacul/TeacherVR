using System.Collections;
using UnityEngine;
using VRTK;

public class TiltWindow : MonoBehaviour
{
    public bool VR = false;
    public Hand VRHandToTrack;
    public Vector2 range = new Vector2(5f, 3f);

    private VRTK_UIPointer UIPointerR;
    private VRTK_UIPointer UIPointerL;
    private Transform HeadsetTranform;

    private float x = 0;
    private float y = 0;
    private Vector3 pos;

    private Transform mTrans;
    private Quaternion mStart;
    private Vector2 mRot = Vector2.zero;

    public enum Hand
    {
        Right,
        Left
    }

    private IEnumerator FindDevices()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        UIPointerR = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_UIPointer>();
        UIPointerL = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_UIPointer>();
        HeadsetTranform = VRTK_DeviceFinder.HeadsetTransform();
    }

    void Start()
    {
        mTrans = transform;
        mStart = mTrans.localRotation;
    }

    void OnEnable()
    {
        if(VR) StartCoroutine(FindDevices());
    }

    private void setXY(VRTK_UIPointer Pointer, VRTK_UIPointer CorrectionPointer)
    {
        if (Pointer != null && Pointer.PointerActive())
        {
            pos = Pointer.GetOriginPosition();

            if (HeadsetTranform.rotation.eulerAngles.y > 315 && HeadsetTranform.rotation.eulerAngles.y < 360 ||
                HeadsetTranform.rotation.eulerAngles.y > 0 && HeadsetTranform.rotation.eulerAngles.y < 45)
            {
                x = CorrectionPointer.GetOriginPosition().x - pos.x;
                y = CorrectionPointer.GetOriginPosition().y - pos.y;
            }
            else if (HeadsetTranform.rotation.eulerAngles.y > 45 && HeadsetTranform.rotation.eulerAngles.y < 135)
            {
                x = -CorrectionPointer.GetOriginPosition().z + pos.z;
                y = CorrectionPointer.GetOriginPosition().y - pos.y;
            }
            else if (HeadsetTranform.rotation.eulerAngles.y > 135 && HeadsetTranform.rotation.eulerAngles.y < 225)
            {
                x = -CorrectionPointer.GetOriginPosition().x + pos.x;
                y = CorrectionPointer.GetOriginPosition().y - pos.y;
            }
            else
            {
                x = CorrectionPointer.GetOriginPosition().z - pos.z;
                y = CorrectionPointer.GetOriginPosition().y - pos.y;
            }
        }
    }

    void Update()
    {
        if (VR)
        {
            if (VRHandToTrack == Hand.Right) setXY(UIPointerR, UIPointerL);
            else setXY(UIPointerL, UIPointerR);
        }
        else
        {
            pos = Input.mousePosition;
            float halfWidth = Screen.width * 0.5f;
            float halfHeight = Screen.height * 0.5f;
            x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
            y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
        }

        mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);

        mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f);
    }
}