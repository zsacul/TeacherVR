using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxList { HookBang, TopDoorBang, TopDoorOpen, SideDoorBang, SideDoorOpen };

public class SoundManager : MonoBehaviour {

    public GameObject ParticlePrefab;
    public GameObject ACSource;
    public GameObject LightSource;

    public AudioClip SfxHookBang;
    public AudioClip SfxTopDoorBang;
    public AudioClip SfxTopDoorOpen;
    public AudioClip SfxSideDoorBang;
    public AudioClip SfxSideDoorOpen;


    private float _ACNsound;

    private int _PoolSize = 30;
    private GameObject[] _SfxParticlesPool;
    private int _FreshPoolInd = 0;

    public AudioClip ClipFromEnum(SfxList en)
    {
        switch (en)
        {
            case SfxList.HookBang:
                return SfxHookBang;

            case SfxList.TopDoorBang:
                return SfxTopDoorBang;

            case SfxList.TopDoorOpen:
                return SfxTopDoorOpen;

            case SfxList.SideDoorBang:
                return SfxSideDoorBang;

            case SfxList.SideDoorOpen:
                return SfxSideDoorOpen;

        }

        return null;
    }

    private GameObject SfxFromPool()
    {
        _FreshPoolInd %= _PoolSize;

        // TODO adaptive pool
        if (_SfxParticlesPool[_FreshPoolInd].activeSelf)
            return null;

        return _SfxParticlesPool[_FreshPoolInd++];
    }

    public void Play3DAt(AudioClip clip, Transform where)
    {
        Play3DAt(clip, where.position);
    }

    public void Play3DAt(SfxList en, Transform where)
    {
        Play3DAt(ClipFromEnum(en), where);
    }

    public void Play3DAt(SfxList en, Vector3 where)
    {
        Play3DAt(ClipFromEnum(en), where);
    }

    public void Play3DAt(AudioClip clip, Vector3 v3)
    {
        GameObject tmp = SfxFromPool();
        tmp.GetComponent<AudioSource>().clip = clip;
        tmp.transform.position = v3;
        tmp.GetComponent<AudioSource>().spatialBlend = 1.0f; //full 3D
        tmp.SetActive(true);
        tmp.GetComponent<AudioSource>().Play();
    }


    public void Play2D(SfxList en)
    {
        Play2D(ClipFromEnum(en));
    }

    public void Play2D(AudioClip clip)
    {
        GameObject tmp = SfxFromPool();
        tmp.GetComponent<AudioSource>().clip = clip;
        tmp.GetComponent<AudioSource>().spatialBlend = 0.0f; //full 2D
        tmp.SetActive(true);
        tmp.GetComponent<AudioSource>().Play();
    }


    public void SetACVolume(float x)
    {
        ACSource.GetComponent<AudioSource>().volume = x;
    }


    // Use this for initialization
    void Start () {

        _SfxParticlesPool = new GameObject[_PoolSize];
        
        for(int i = 0; i < _PoolSize; i++)
        {
            GameObject tmp = (GameObject)Instantiate(ParticlePrefab);
            tmp.SetActive(false);
            _SfxParticlesPool[i] = tmp;
        }       


        _ACNsound = 1.0f;
        ACSource.GetComponent<AudioSource>().Play();


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
