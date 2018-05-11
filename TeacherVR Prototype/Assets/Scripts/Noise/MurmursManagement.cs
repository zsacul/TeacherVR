using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurmursManagement : MonoBehaviour
{
    static public AudioSource MurmursSource;

    static public bool murmurs;

    // Use this for initialization
    void Start()
    {
        murmurs = false;
        MurmursSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!murmurs && GameController.Instance.EventsManager.ListOfEvents.Count > 0 &&
            GameController.Instance.EventsManager.ListOfEvents[0].name == "Noise")
        {
            StartCoroutine(MurmurLerp(0.1f));
            MurmursSource.Play();
            murmurs = true;
        }
    }

    IEnumerator MurmurLerp(float volume)
    {
        while (MurmursSource.volume < volume)
        {
            yield return new WaitForSeconds(1.5f);
            MurmursSource.volume += 0.01f;
        }
    }
}