using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ControllerObjectsSpawner : MonoBehaviour
{
    public GameObject[] Objects;
    public float angle = 20;

    private List<GameObject> ObjectsInstances = new List<GameObject>();

    private GameObject RightHand;
    private GameObject LeftHand;

    void Start()
    {
        ObjectsInstances.Clear();
        StartCoroutine(FindDevices());
    }

    private IEnumerator FindDevices()
    {
        yield return new WaitForEndOfFrame();
        RightHand = VRTK_DeviceFinder.GetControllerRightHand();
        LeftHand = VRTK_DeviceFinder.GetControllerLeftHand();

        RightHand.GetComponent<VRTK_ControllerEvents>().GripPressed += RightHand_GripPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().GripPressed += LeftHand_GripPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().GripReleased += RightHand_GripReleased;
        LeftHand.GetComponent<VRTK_ControllerEvents>().GripReleased += LeftHand_GripReleased;
    }

    private void Spawn(Transform HandTransform)
    {
        int sign = 1;
        int count = 0;
        foreach (GameObject obj in Objects)
        {
            sign *= -1;
            count++;
            GameObject instantiate =
                Instantiate(obj, HandTransform.position + HandTransform.forward / 2, HandTransform.rotation);
            float correct = 0;
            if (Objects.Length % 2 == 0) correct = angle / 2;
            instantiate.transform.RotateAround(HandTransform.position, HandTransform.up,
                sign * (count / 2) * angle - correct);
            /*instantiate.transform.eulerAngles =
                new Vector3(obj.transform.eulerAngles.x, instantiate.transform.eulerAngles.y, obj.transform.rotation.z);*/
            Rigidbody rb = instantiate.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
            ObjectsInstances.Add(instantiate);
        }
    }

    private void DeSpawn()
    {
        foreach (GameObject obj in ObjectsInstances)
        {
            if (obj.GetComponent<VRTK_InteractableObject>().isGrabbable)
                Destroy(obj);
        }
        ObjectsInstances.Clear();
    }

    private void RightHand_GripPressed(object sender, ControllerInteractionEventArgs e)
    {
        Spawn(RightHand.transform);
    }

    private void LeftHand_GripPressed(object sender, ControllerInteractionEventArgs e)
    {
        Spawn(LeftHand.transform);
    }

    private void RightHand_GripReleased(object sender, ControllerInteractionEventArgs e)
    {
        DeSpawn();
    }

    private void LeftHand_GripReleased(object sender, ControllerInteractionEventArgs e)
    {
        DeSpawn();
    }
}