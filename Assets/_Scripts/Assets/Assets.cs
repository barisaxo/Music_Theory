using UnityEngine;

public static class Assets
{
    public static Material Video_Mat => Resources.Load<Material>("Materials/Video_Mat");
    public static Material Stars => Resources.Load<Material>("Skyboxes/Stars");
    public static Material Cosmic => Resources.Load<Material>("Skyboxes/Cosmic");
    public static Sprite White => Resources.Load<Sprite>("Sprites/Misc/White");
    public static AudioClip TypingClicks => Resources.Load<AudioClip>("Audio/SFX/Typing Clicks");



}