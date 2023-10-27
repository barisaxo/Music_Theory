using UnityEngine;

public sealed class SkyboxRotate
{
    private SkyboxRotate() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void AutoInit()
    {
        Skybox = RenderSettings.skybox = Assets.Cosmic;
        Skybox.SetFloat("_Rotation", Random.Range(-180, 180));
        RotSpeed = .04f * Random.value < .5f ? 1 : -1;
        MonoHelper.OnUpdate += RotateSkybox;
    }

    static Material Skybox;
    static float RotSpeed;

    static void RotateSkybox() =>
        Skybox.SetFloat("_Rotation", Skybox.GetFloat("_Rotation") + Time.deltaTime * RotSpeed);
}
