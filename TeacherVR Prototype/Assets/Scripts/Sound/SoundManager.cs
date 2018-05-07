using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Public enum for referencing individual audio samples - to be spawned wherever
// Generic samples only!
public enum SamplesList { HookBang, ComputerBeep, WaterSplash, BottleSpray, BottleFilling,
    Ladder, BBSliding, Error, Correct, Pop, Clink, WaterRunning, Poof , ShortPoof , MagicPoof, Electric,
    BookBang, // sound of book slamming into floor
    CoinArcade, // retro game style coin sfx
    Squeak, // mouse squeaking
    DeathSqueak, // mouse dying :(
    Simon1, // sfx for simon buttons
    Simon2,
    Simon3,
    Simon4,
    AlarmClock, // bell-type alarm clock
    ThrowWoosh, // woosh of air when throwing something
    Gasp, // surprised egg
    Buzz, // for the fly hit sfx
    FaucetOpen, // opening squeaking faucet/valve
    FaucetClose, // closing ... 
    Paper, // paper crumbling
    FemaleOof, // hurt sound for a female egg
    MaleGrunt, // hurt sound for a male egg
    Success, // event completed
    Murmurs, // looped sfx of noisy students
    Blinds, // looped sliding metal blinds sfx
    Click, // glass-like clink for the light switching sfx
    Clapping, // three claps synced to the clapping animation (more or less)
    MumbleMale, // loop of male student mumbling (TODO sync to animation)
    LeverClick, // clicking sfx for the lever mechanism (to provide audio feedback)
    LeverSwitchOn, // when changing states (to "on", whatever that is)
    LeverSwitchOff // ... (to "off", whatever this is)
};



public class SoundManager : MonoBehaviour {

    // Prefabs, which will be used for pooling
    // Much better idea than setting their properties in code

    // ---------------------------------------------------------------------

    // Generic prefab for spawning one-time samples in the scene
    public GameObject GenericPrefab;

    // ---------------------------------------------------------------------

    
    // Actual in-scene GameObjects which play the sounds
    // The hidden scripts are used to transparently access the actual sfx interface
    // since otherwise GetComponent<> would be called every time we want to play something.
    // Public but hidden in inspector, since they will be overwritten 


    public GameObject ACSource;
    public GameObject LightSource;

    #region TopDoor

    public GameObject TopDoorSource;

    [HideInInspector]
    public TopDoorScript TopDoor;

    #endregion

    // TopDoor:
    // SOpen() - play opening sound
    // SClose() - play closing (bang) sound

    #region LeftScreen

    public GameObject LeftScreenSource;

    [HideInInspector]
    public ScreenScript LeftScreen;

    #endregion

    #region RightScreen

    public GameObject RightScreenSource;

    [HideInInspector]
    public ScreenScript RightScreen;

    #endregion

    // *Screen:
    // SLower() - play sound of lowering the screen
    // SRaise() - play sound of raising the screen


    // ---------------------------------------------------------------------


    // Raw AudioClips which are used to play one-shot samples in the scene
    // Only generic (with no static location) samples are listed here
    // Static meaning static 3D location or a script attached to a persistent game object

    public AudioClip SfxHookBang;
    public AudioClip SfxComputerBeep;
    public AudioClip SfxWaterSplash;
    public AudioClip SfxBottleSpray;
    public AudioClip SfxBottleFilling;
    public AudioClip SfxLadder;
    public AudioClip SfxBBSliding;
    public AudioClip SfxError;
    public AudioClip SfxCorrect;
    public AudioClip SfxPop;
    public AudioClip SfxClink;
    public AudioClip SfxWaterRunning;
    public AudioClip SfxPoof;
    public AudioClip SfxShortPoof;
    public AudioClip SfxMagicPoof;
    public AudioClip SfxElectric;
    public AudioClip SfxBookBang;
    public AudioClip SfxCoinArcade;
    public AudioClip[] SfxSqueak;
    public AudioClip SfxDeathSqueak;
    public AudioClip SfxSimon1;
    public AudioClip SfxSimon2;
    public AudioClip SfxSimon3;
    public AudioClip SfxSimon4;
    public AudioClip SfxAlarmClock;
    public AudioClip SfxThrowWoosh;
    public AudioClip SfxGasp;
    public AudioClip SfxBuzz;
    public AudioClip SfxFaucetOpen;
    public AudioClip SfxFaucetClose;
    public AudioClip SfxPaper;
    public AudioClip SfxFemaleOof;
    public AudioClip SfxMaleGrunt;
    public AudioClip SfxSuccess;
    public AudioClip SfxMurmurs;
    public AudioClip SfxBlinds;
    public AudioClip SfxClick;
    public AudioClip SfxClapping;
    public AudioClip SfxMumbleMale;
    public AudioClip SfxLeverClick;
    public AudioClip SfxLeverSwitchOn;
    public AudioClip SfxLeverSwitchOff;


    // ---------------------------------------------------------------------

    private int _PoolSize = 30000;
    private int _InitialSize = 3;
    private List<GameObject> _SfxParticlesPool;
    private int _FreshPoolInd = 0;

    // ---------------------------------------------------------------------


    public AudioClip ClipFromEnum(SamplesList en)
    {
        switch (en)
        {
            case SamplesList.HookBang:
                return SfxHookBang;

            case SamplesList.ComputerBeep:
                return SfxComputerBeep;

            case SamplesList.WaterSplash:
                return SfxWaterSplash;

            case SamplesList.BottleSpray:
                return SfxBottleSpray;

            case SamplesList.BottleFilling:
                return SfxBottleFilling;

            case SamplesList.Ladder:
                return SfxLadder;

            case SamplesList.BBSliding:
                return SfxBBSliding;

            case SamplesList.Error:
                return SfxError;

            case SamplesList.Correct:
                return SfxCorrect;

            case SamplesList.Pop:
                return SfxPop;

            case SamplesList.Clink:
                return SfxClink;

            case SamplesList.WaterRunning:
                return SfxWaterRunning;

            case SamplesList.Poof:
                return SfxPoof;

            case SamplesList.ShortPoof:
                return SfxShortPoof;

            case SamplesList.MagicPoof:
                return SfxMagicPoof;

            case SamplesList.Electric:
                return SfxElectric;

            case SamplesList.BookBang:
                return SfxBookBang;

            case SamplesList.CoinArcade:
                return SfxCoinArcade;

            case SamplesList.Squeak:
                return SfxSqueak[Random.Range(0,SfxSqueak.Length-1)];

            case SamplesList.DeathSqueak:
                return SfxDeathSqueak;

            case SamplesList.Simon1:
                return SfxSimon1;

            case SamplesList.Simon2:
                return SfxSimon2;

            case SamplesList.Simon3:
                return SfxSimon3;

            case SamplesList.Simon4:
                return SfxSimon4;

            case SamplesList.AlarmClock:
                return SfxAlarmClock;

            case SamplesList.ThrowWoosh:
                return SfxThrowWoosh;

            case SamplesList.Gasp:
                return SfxGasp;

            case SamplesList.Buzz:
                return SfxBuzz;

            case SamplesList.FaucetClose:
                return SfxFaucetClose;

            case SamplesList.FaucetOpen:
                return SfxFaucetOpen;

            case SamplesList.Paper:
                return SfxPaper;

            case SamplesList.FemaleOof:
                return SfxFemaleOof;

            case SamplesList.MaleGrunt:
                return SfxMaleGrunt;

            case SamplesList.Success:
                return SfxSuccess;

            case SamplesList.Murmurs:
                return SfxMurmurs;

            case SamplesList.Blinds:
                return SfxBlinds;

            case SamplesList.Click:
                return SfxClick;

            case SamplesList.Clapping:
                return SfxClapping;

            case SamplesList.MumbleMale:
                return SfxMumbleMale;

            case SamplesList.LeverClick:
                return SfxLeverClick;

            case SamplesList.LeverSwitchOn:
                return SfxLeverSwitchOn;

            case SamplesList.LeverSwitchOff:
                return SfxLeverSwitchOff;
        }

        return null;
    }

    private GameObject SfxFromPool()
    {
        int rind = _SfxParticlesPool.FindIndex(x => !x.activeSelf);

        if(rind == -1)
        {
            _SfxParticlesPool.Add(Instantiate(GenericPrefab));
            rind = _SfxParticlesPool.Count - 1;
        }

        return _SfxParticlesPool[rind];

    }

    public GameObject Play3DAt(AudioClip clip, Transform where, float volume = 1.0f)
    {
        return Play3DAt(clip, where.position, volume);
    }

    public GameObject Play3DAt(SamplesList en, Transform where, float volume = 1.0f)
    {
        return Play3DAt(ClipFromEnum(en), where, volume);
    }

    public GameObject Play3DAt(SamplesList en, Vector3 where, float volume = 1.0f)
    {
        return Play3DAt(ClipFromEnum(en), where, volume);
    }

    public GameObject Play3DAt(AudioClip clip, Vector3 v3, float volume = 1.0f)
    {
        GameObject tmp = SfxFromPool();
        tmp.GetComponent<AudioSource>().clip = clip;
        tmp.GetComponent<AudioSource>().volume = volume;
        tmp.transform.position = v3;
        tmp.GetComponent<AudioSource>().spatialBlend = 1.0f; //full 3D
        tmp.SetActive(true);
        tmp.GetComponent<AudioSource>().Play();
        return tmp;
    }


    public GameObject Play2D(SamplesList en, float volume = 1.0f)
    {
        return Play2D(ClipFromEnum(en), volume);
    }

    public GameObject Play2D(AudioClip clip, float volume = 1.0f)
    {
        GameObject tmp = SfxFromPool();
        tmp.GetComponent<AudioSource>().clip = clip;
        tmp.GetComponent<AudioSource>().volume = volume;
        tmp.GetComponent<AudioSource>().spatialBlend = 0.0f; //full 2D
        tmp.SetActive(true);
        tmp.GetComponent<AudioSource>().Play();
        return tmp;
    }


    public void SetACVolume(float x)
    {
        ACSource.GetComponent<AudioSource>().volume = x;
    }

    public void SetGlobalVolume(float x)
    {
        AudioListener.volume = x;
    }

    public float GetGlobalVolume()
    {
        return AudioListener.volume;
    }


    // Use this for initialization
    void Start () {

        _SfxParticlesPool = new List<GameObject>
        {
            Capacity = _PoolSize
        };

        for (int i = 0; i < _InitialSize; i++)
        {
            GameObject tmp = (GameObject)Instantiate(GenericPrefab);
            tmp.SetActive(false);
            _SfxParticlesPool.Add(tmp);
        }

        TopDoor = TopDoorSource.GetComponent<TopDoorScript>();

        LeftScreen = LeftScreenSource.GetComponent<ScreenScript>();
        RightScreen = RightScreenSource.GetComponent<ScreenScript>();
    }
	
	// No need for this
	void Update () {
        if (Input.GetKey(KeyCode.J))
            LeftScreen.SRaise();
	}
}
