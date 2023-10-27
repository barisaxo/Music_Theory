using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public static class WaveGenerator
    {
        public static AudioClip CreateSineWave(float freq, float length)
        {
            int sampleFreq = 21000 * 2;
            float amplitude = .35f;

            float[] samples = new float[21000 * 2];
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = (amplitude * Mathf.Sin(Mathf.PI * 2 * i * freq / sampleFreq + 0))
                // +  (.28f * (Mathf.Sin(Mathf.PI * 2 * i * (frequency * 1.25f) / sampleFreq + Mathf.PI)))
                ;
            }
            for (int i = 0; i < 12000; i++)
            {
                samples[i] = samples[i] / (i * 10);
                samples[^(1 + i)] = samples[i] / ((12000 - i) * 10);
            }
            AudioClip _ac = AudioClip.Create(nameof(AudioClip), samples.Length, 1, sampleFreq, false);
            _ac.SetData(samples, 0);

            return _ac;
        }

        public static AudioClip CreateSineWave(float freq)
        {
            int sampleFreq = 21000 * 2;
            float amplitude = .35f;
            float[] samples = new float[21000 * 2];

            for (int i = 0; i < samples.Length; i++)
                samples[i] = amplitude * Mathf.Sin(Mathf.PI * 2 * i * freq / sampleFreq);
            samples[0] = 0;
            samples[^1] = 0;

            AudioClip _ac = AudioClip.Create(nameof(AudioClip), samples.Length, 1, sampleFreq, false);

            _ac.SetData(samples, 0);

            return _ac;
        }

        public static AudioClip CreateIntervalSineWave(float bottom, float top)
        {
            int sampleFreq = 21000 * 2;
            float amplitude = .55f;

            float[] samples = new float[21000 * 2];
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = (amplitude * Mathf.Sin(Mathf.PI * 2 * i * bottom / sampleFreq))
                + (amplitude * Mathf.Sin(Mathf.PI * 2 * i * top / sampleFreq))
                ;
            }
            samples[0] = 0;
            samples[^1] = 0;

            AudioClip _ac = AudioClip.Create(nameof(AudioClip), samples.Length, 1, sampleFreq, false);
            _ac.SetData(samples, 0);

            return _ac;
        }

        public static AudioClip CreateTriadSineWave(float rootFreq, float thirdFreq, float fifthFreq)
        {
            int sampleFreq = 21000 * 2;
            float amplitude = .55f;

            float[] samples = new float[21000 * 2];
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = (amplitude * Mathf.Sin(Mathf.PI * 2 * i * rootFreq / sampleFreq))
                + (amplitude * Mathf.Sin(Mathf.PI * 2 * i * thirdFreq / sampleFreq))
                + (amplitude * Mathf.Sin(Mathf.PI * 2 * i * fifthFreq / sampleFreq))
                ;
            }
            samples[0] = 0;
            samples[^1] = 0;

            AudioClip _ac = AudioClip.Create(nameof(AudioClip), samples.Length, 1, sampleFreq, false);
            _ac.SetData(samples, 0);

            return _ac;
        }

    }
}