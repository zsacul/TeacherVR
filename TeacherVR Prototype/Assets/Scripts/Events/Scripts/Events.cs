using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Events/Basic Event")]
public class Events : ScriptableObject
{
    new public string name = "New Event";
    public Sprite icon = null;
    public string description;
    public Status status;
    public int lvl;

    public virtual void StartEvent()
    {
        Debug.Log("Starting " + name);
    }

    public virtual void AbortEvent()
    {
        Debug.Log("Aborting " + name);
    }

    public enum Status
    {
        Nothing,
        Progress,
        Complete
    }

    public void Pkt(int pkt)
    {
        Debug.Log("+Pkt " + name);
    }
}