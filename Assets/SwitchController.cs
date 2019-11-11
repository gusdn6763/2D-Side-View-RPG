using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SwitchController : MonoBehaviour, IGYSwitch
{
    public GYSwitch bgmSwitch;

    AudioSource audioSource;

    private void Start()
    {
        bgmSwitch.switchController = this;
        audioSource = GetComponent<AudioSource>();
    }


    public void SwitchIsOn(bool isOn)
    {
        if(isOn == true)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}


    /*
public class SwitchController : MonoBehaviour
{
    public GYSwitch bgmSwitch;

    AudioSource audioSource;

    public void SwitchIsOn(bool isOn)
    {
        if (isOn == true)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bgmSwitch.switchIOnAction = (ison) =>
        {

        };
    }
*/