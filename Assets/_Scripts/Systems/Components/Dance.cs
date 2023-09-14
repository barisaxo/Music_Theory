using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{
    //private void OnEnable()
    //{
    //    Debug.Log("I'm Enabled");
    //    DanceManager.Io.AddDancer(this);
    //}
    //private void OnDisable()
    //{
    //    Debug.Log("I'm Disabled");
    //    DanceManager.Io.RemoveDancer(this);
    //}
}

public class DanceManager
{
    #region INSTANCE
    private DanceManager() { }

    static public DanceManager Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static DanceManager _io;
        internal static DanceManager Io => _io ??= new DanceManager();
    }
    #endregion INSTANCE

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    //private static void AutoInit() => MonoHelper.OnUpdate += Io.Loop;

    private readonly List<Dancer> Dancers = new();
    private bool running;

    public void AddDancer(Dance dancer)
    {
        foreach (var d in Dancers) if (d.dancer == dancer) return;

        Dancers.Add(new Dancer
        {
            dancer = dancer,
            pos = dancer.transform.position,
            scale = dancer.transform.localScale,
            pOff = new Vector2(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f)),
            sOff = new Vector2(Random.Range(0, .15f), Random.Range(0, .15f)),
            rotz = dancer.transform.rotation.z,
            rotzOff = Random.Range(-1f, 1f),
            interval = Random.Range(.5f, 1f) + (Random.value * .5f),
            timer = 0
        });

        if (Dancers.Count > 0 && !running)
        {
            running = true;
            MonoHelper.OnUpdate += Io.DanceLoop;
        }
    }

    public void RemoveDancer(Dance dancer)
    {
        List<Dancer> temp = new();

        foreach (var d in Dancers)
            if (d.dancer == dancer)
                temp.Add(d);


        foreach (var d in temp)
            if (d.dancer == dancer)
            {
                d.dancer.transform.localScale = d.scale;
                d.dancer.transform.SetLocalPositionAndRotation(d.pos, Quaternion.Euler(0, 0, d.rotz));
                Dancers.Remove(d);
            }

        if (Dancers.Count == 0 && running)
        {
            running = false;
            MonoHelper.OnUpdate -= Io.DanceLoop;
        }
    }

    void DanceLoop()
    {
        for (int i = 0; i < Dancers.Count; i++)
        {
            if ((Dancers[i].timer += Time.deltaTime) > Dancers[i].interval)
            {
                Dancers[i].flip = !Dancers[i].flip;
                Dancers[i].timer -= Dancers[i].interval;

                if (Dancers[i].flip)
                {
                    Dancers[i].dancer.transform.SetLocalPositionAndRotation(
                        Dancers[i].pos + Dancers[i].pOff,
                        Quaternion.Euler(0, 0, Dancers[i].rotzOff));

                    Dancers[i].dancer.transform.localScale = Dancers[i].scale + Dancers[i].sOff;
                }
                else
                {
                    Dancers[i].dancer.transform.SetLocalPositionAndRotation(
                        Dancers[i].pos,
                        Quaternion.Euler(0, 0, Dancers[i].rotz));

                    Dancers[i].dancer.transform.localScale = Dancers[i].scale;
                }
            }
        }
    }

    class Dancer
    {
        public Dance dancer;
        public Vector3 pos;
        public Vector3 scale;
        public Vector3 pOff;
        public Vector3 sOff;
        public float rotz;
        public float rotzOff;
        public float interval;
        public float timer;
        public bool flip;
    }
}