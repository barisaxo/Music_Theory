using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineGenerator : MonoBehaviour
{
    [SerializeField, Range(0, 1)] public float amp = 0.5f;
    [SerializeField] public float freq = 261.62f; // middle C

    private double _phase;
    private int _sampleRate;

    private AudioSource _source;
    public AudioSource Source => _source ? _source : _source = gameObject.AddComponent<AudioSource>();

    private void Awake()
    {
        _sampleRate = AudioSettings.outputSampleRate;
    }

    private void Start()
    {
        _ = Source;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        double phaseIncrement = freq / _sampleRate;
        for (int sample = 0; sample < data.Length; sample += channels)
        {
            float value = Mathf.Sin((float)_phase * 2 * Mathf.PI) * amp;
            _phase = (_phase + phaseIncrement) % 1;

            for (int channel = 0; channel < channels; channel++)
            {
                data[sample + channel] = value;
            }
        }
    }
}

public static class SineManager
{
    private static SineGenerator _generator;
    public static SineGenerator SineGenerator => _generator ? _generator :
        _generator = new GameObject(nameof(SineGenerator)).AddComponent<SineGenerator>();

    //public static void PlayOneShot(float freq)
    //{
    //    Play().StartCoroutine();

    //    IEnumerator Play()
    //    {
    //        SineGenerator.gameObject.SetActive(true);
    //        SineGenerator.freq = freq;
    //        yield return new WaitForSecondsRealtime(1.5f);
    //        SineGenerator.gameObject.SetActive(false);
    //    }
    //}

}