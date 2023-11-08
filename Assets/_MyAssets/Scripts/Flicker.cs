using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Flicker : MonoBehaviour
{
    private const int ZERO = 0;

    [Header("References")]
    [SerializeField] private Light _light = default;
    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField] private AudioClip _electricWire = default;

    [Header("Numbers")]
    [SerializeField] private float _minTimeBetweenFlicker = 0.2f;
    [SerializeField] private float _maxTimeBetweenFlicker = 1.0f;
    [SerializeField] private float _minDownTime = 5.0f;
    [SerializeField] private float _maxDownTime = 20.0f;
    [SerializeField] private int _minFlickerNumber = 1;
    [SerializeField] private int _maxFlickerNumber = 6;

    [SerializeField] private float _clipVolume = 0.05f;

    private bool _isActive = true;

    private void Start()
    {
        StartCoroutine(FlickerCoroutine());
    }

    /* ------------------------------------------
     * FLICKER
     * ------------------------------------------
     */
    private IEnumerator FlickerCoroutine() 
    {
        while (_isActive)
        {
            int flickerNumber = Random.Range(_minFlickerNumber, _maxFlickerNumber);

            while (flickerNumber > ZERO)
            {
                PlayAudio();
                SwitchLight();

                flickerNumber--;

                yield return new WaitForSeconds(GetTime(_minTimeBetweenFlicker, _maxTimeBetweenFlicker));
            }

            MakeSureLightIsOn();

            yield return new WaitForSeconds(GetTime(_minDownTime, _maxDownTime));
        }
    }

    /* ------------------------------------------
     * TIME
     * ------------------------------------------
     */
    private float GetTime(float minTime, float maxTime) 
    {
        return Random.Range(minTime, maxTime);
    }

    /* ------------------------------------------
     * AUDIO
     * ------------------------------------------
     */
    private void PlayAudio() 
    {
        _audioSource.PlayOneShot(_electricWire, _clipVolume);
    }

    /* ------------------------------------------
     * LIGHT
     * ------------------------------------------
     */
    private void MakeSureLightIsOn() 
    {
        if (_light.enabled == false)
        {
            PlayAudio();
            LightOn();
        }
    }

    private void SwitchLight() 
    {
        if (_light.enabled == true)
            LightOff();
        else
            LightOn();
    }

    private void LightOn() 
    {
        _light.enabled = true;
    }

    private void LightOff() 
    {
        _light.enabled = false;
    }
}
