using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Tutorial_Point_Spawn_Control : MonoBehaviour
{
    public Tutorial_Point_Anim_Control tutorial_point_user;
    
    void Start()
    {
        foreach (var zone in transform.parent.GetComponentsInChildren<VRTK_SnapDropZone>())
        {
            zone.ObjectUnsnappedFromDropZone += Zone_ObjectUnsnappedFromDropZone;
        }
    }

    void OnDestroy()
    {
        foreach (var zone in transform.parent.GetComponentsInChildren<VRTK_SnapDropZone>())
        {
            zone.ObjectUnsnappedFromDropZone -= Zone_ObjectUnsnappedFromDropZone;
        }
    }

    private void Zone_ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
        tutorial_point_user.Abort();
    }
}