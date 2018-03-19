using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Events")]
public class Events : ScriptableObject
{
    new public string name = "New Event";
    public Sprite icon = null;
    //public bool isDefaultItem = false;

    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + name);
    }

    public virtual void Unequip()
    {
        // Unequip the item
        // Something might happen

        Debug.Log("Unequip " + name);
    }
}