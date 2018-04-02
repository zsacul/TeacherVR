using System.Collections;
using UnityEngine;
using VRTK;

public class TiltWindow : MonoBehaviour
{
    public enum Hand
    {
        Right,
        Left
    }

    public bool VR = false;
    public Hand VRHandToTrack;
    private VRTK_UIPointer UIPointerR;
    private VRTK_UIPointer UIPointerL;
    private Transform HeadsetTranform;
    public Vector2 range = new Vector2(5f, 3f);

    Transform mTrans;
    Quaternion mStart;
    Vector2 mRot = Vector2.zero;

    private IEnumerator FindHands()
    {
        yield return new WaitForEndOfFrame();
        UIPointerR = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_UIPointer>();
        UIPointerL = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_UIPointer>();
        HeadsetTranform = VRTK_DeviceFinder.HeadsetTransform();
    }

    void Start()
    {
        mTrans = transform;
        mStart = mTrans.localRotation;
        StartCoroutine(FindHands());
    }

    void Update()
    {
        Vector3 pos;
        float x = 0;
        float y = 0;
        if (VR)
        {
            if (VRHandToTrack == Hand.Right && UIPointerR.PointerActive())
            {
                pos = UIPointerR.GetOriginPosition();

                if (HeadsetTranform.rotation.eulerAngles.y > 315 && HeadsetTranform.rotation.eulerAngles.y < 360 ||
                    HeadsetTranform.rotation.eulerAngles.y > 0 && HeadsetTranform.rotation.eulerAngles.y < 45)
                {
                    x = UIPointerL.GetOriginPosition().x - pos.x;
                    y = UIPointerL.GetOriginPosition().y - pos.y;
                }
                else if (HeadsetTranform.rotation.eulerAngles.y > 45 && HeadsetTranform.rotation.eulerAngles.y < 135)
                {
                    x = -UIPointerL.GetOriginPosition().z + pos.z;
                    y = UIPointerL.GetOriginPosition().y - pos.y;
                }
                else if (HeadsetTranform.rotation.eulerAngles.y > 135 && HeadsetTranform.rotation.eulerAngles.y < 225)
                {
                    x = -UIPointerL.GetOriginPosition().x + pos.x;
                    y = UIPointerL.GetOriginPosition().y - pos.y;
                }
                else
                {
                    x = UIPointerL.GetOriginPosition().z - pos.z;
                    y = UIPointerL.GetOriginPosition().y - pos.y;
                }
            }
            else if (UIPointerL.PointerActive())
            {
                pos = UIPointerL.GetOriginPosition();
                if (HeadsetTranform.rotation.eulerAngles.y >= 315 && HeadsetTranform.rotation.eulerAngles.y <= 360 ||
                    HeadsetTranform.rotation.eulerAngles.y >= 0 && HeadsetTranform.rotation.eulerAngles.y <= 45)
                {
                    x = UIPointerR.GetOriginPosition().x - pos.x;
                    y = UIPointerR.GetOriginPosition().y - pos.y;
                }
                else if (HeadsetTranform.rotation.eulerAngles.y >= 45 && HeadsetTranform.rotation.eulerAngles.y <= 135)
                {
                    x = -UIPointerR.GetOriginPosition().z + pos.z;
                    y = UIPointerR.GetOriginPosition().y - pos.y;
                }
                else if (HeadsetTranform.rotation.eulerAngles.y >= 135 && HeadsetTranform.rotation.eulerAngles.y <= 225)
                {
                    x = -UIPointerR.GetOriginPosition().x + pos.x;
                    y = UIPointerR.GetOriginPosition().y - pos.y;
                }
                else
                {
                    x = UIPointerR.GetOriginPosition().z - pos.z;
                    y = UIPointerR.GetOriginPosition().y - pos.y;
                }
            }
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