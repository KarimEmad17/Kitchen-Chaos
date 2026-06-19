using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string Player_Prefs_Sound_Volume = "SoundVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipsRefsSO audioRefs;
    private float volume=1f;
    // Start is called before the first frame update
    void Start()
    {
        DeliveryManager.Instance.OnRecipeSucceed+=DeliveryManager_OnRecipeSucceed;
        DeliveryManager.Instance.OnRecipeFail+=DeliveryManager_OnRecipeFail;
        CuttingCounter.OnAnyCut+=CuttingCounter_OnAnyCut;
        PlayerControllerScript.Instence.OnPickUpSomething+=Player_OnPickUpSomthing;
        BaseCounter.OnDropSomething+=Player_OnDropSomething;
        TrashCounter.OnDropTrash+=TrashCounter_OnDropTrash;
    }
    private void Awake()
    {
        Instance = this;
      volume=  PlayerPrefs.GetFloat(Player_Prefs_Sound_Volume, 1f);

    }

    private void TrashCounter_OnDropTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioRefs.trash, trashCounter.transform.position);
    }

    private void Player_OnDropSomething(object sender, System.EventArgs e)
    {
        BaseCounter counter = sender as BaseCounter;
        PlaySound(audioRefs.objectDrop, counter.transform.position);
    }

    private void Player_OnPickUpSomthing(object sender, System.EventArgs e)
    {
        PlayerControllerScript player = sender as PlayerControllerScript;
        PlaySound(audioRefs.objectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioRefs.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        
        PlaySound(audioRefs.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeSucceed(object sender, System.EventArgs e)
    {
        PlaySound(audioRefs.deliverySucces, DeliveryCounter.Instance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip[] audioClipArray ,Vector3 position,float volume = 1f)
    {

        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
    }
    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {

        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootStepSound( Vector3 position, float volume = 1f)
    {

        AudioSource.PlayClipAtPoint(audioRefs.footStep[Random.Range(0, audioRefs.footStep.Length)], position, volume);
    }
    public void PlayCountDownSound()
    {

        AudioSource.PlayClipAtPoint(audioRefs.warning[Random.Range(0,audioRefs.warning.Length)], Vector3.zero);
    }
    public void PlayWarningSound( Vector3 position)
    {

        AudioSource.PlayClipAtPoint(audioRefs.warning[Random.Range(0, audioRefs.warning.Length)], position );
    }
    public void ChangeVolume()
    {
        volume+=0.1f;
        if (volume>1)
        {
            volume=0f;
        }
        PlayerPrefs.SetFloat(Player_Prefs_Sound_Volume, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
