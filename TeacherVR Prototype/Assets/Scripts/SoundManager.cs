using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public GameObject ParticlePrefab;
    public GameObject ACSource;
    public AudioClip TestClip;
    private float _ACNsound;

    private int _PoolSize = 30;
    private GameObject[] _SfxParticlesPool;
    private int _FreshPoolInd = 0;

    private GameObject SfxFromPool()
    {
        _FreshPoolInd %= _PoolSize;

        // TODO adaptive pool
        if (_SfxParticlesPool[_FreshPoolInd].activeSelf)
            return null;

        return _SfxParticlesPool[_FreshPoolInd++];
    }

    public void PlaySfxAt(AudioClip clip, Transform where)
    {
        PlaySfxAt(clip, where.position);
    }

    public void PlaySfxAt(AudioClip clip, Vector3 v3)
    {
        GameObject tmp = SfxFromPool();
        tmp.GetComponent<AudioSource>().clip = clip;
        tmp.transform.position = v3;
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

        // for Sfx testing!
        if(Input.GetKeyDown(KeyCode.H))
        {
            //test position at the left chair in the hall
            Vector3 tmp = GameObject.Find("25_chair_3").transform.position;
            PlaySfxAt(TestClip, tmp);
        }
		
	}
}
