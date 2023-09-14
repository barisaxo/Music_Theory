using UnityEngine;

public class Cam
{
    #region INSTANCE
    private Cam()
    {
        _ = Camera;
        _ = UICamera;
        _ = AudioListener;
    }

    public static Cam Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static Cam _io;
        internal static Cam Io => _io ??= new Cam();
        internal static void Destruct() => _io = null;
    }

    public void SelfDestruct()
    {
        Object.Destroy(_cam.gameObject);
        Instance.Destruct();
    }
    #endregion INSTANCE

    public static float MainOrthoX => Io.Camera.orthographicSize * Io.Camera.aspect;
    public static float MainOrthoY => Io.Camera.orthographicSize;

    public static float UIOrthoX => Io.UICamera.orthographicSize * Io.Camera.aspect;
    public static float UIOrthoY => Io.UICamera.orthographicSize;

    private Camera _cam;
    public Camera Camera
    {
        get
        {
            return _cam != null ? _cam : _cam = SetUpCam();
            static Camera SetUpCam()
            {
                Camera c = GameObject.Find("Camera") != null ? GameObject.Find("Camera").GetComponent<Camera>() :
                   Object.Instantiate(Resources.Load<Camera>("Prefabs/Cameras/Camera"));
                Object.DontDestroyOnLoad(c);
                c.orthographic = false;
                c.fieldOfView = 60;
                c.transform.position = Vector3.back * 10;
                c.backgroundColor = new Color(Random.Range(.9f, 1f), Random.Range(.8f, 1f), Random.Range(.85f, 1f));
                c.gameObject.SetActive(true);
                return c;
            }
        }
    }

    private Camera _uicam;
    public Camera UICamera
    {
        get
        {
            return _uicam != null ? _uicam : _uicam = SetUpCam();
            static Camera SetUpCam()
            {
                Camera c = GameObject.Find("UICamera") != null ? GameObject.Find("UICamera").GetComponent<Camera>() :
                   Object.Instantiate(Resources.Load<Camera>("Prefabs/Cameras/UICamera"));
                Object.DontDestroyOnLoad(c);
                c.orthographicSize = 5;
                c.orthographic = true;
                c.transform.position = Vector3.back * 10;
                c.gameObject.SetActive(true);
                return c;
            }
        }
    }

    private AudioListener _audioListener;
    public AudioListener AudioListener => _audioListener != null ? _audioListener :
         Camera.TryGetComponent(out AudioListener ao) ? _audioListener = ao :
        _audioListener = Camera.gameObject.AddComponent<AudioListener>();

    public void SetObliqueness(Vector2 v2) => SetObliqueness(v2.x, v2.y);
    public void SetObliqueness(float horizObl, float vertObl)
    {
        Matrix4x4 mat = Io.Camera.projectionMatrix;
        mat[0, 2] = horizObl;
        mat[1, 2] = vertObl;
        Io.Camera.projectionMatrix = mat;
    }

}





//SetObliqueness(1, 2);
//void SetObliqueness(float horizObl, float vertObl)
//{
//    Matrix4x4 mat = c.projectionMatrix;
//    mat[0, 2] = horizObl;
//    mat[1, 2] = vertObl;
//    c.projectionMatrix = mat;
//https://docs.unity3d.com/Manual/ObliqueFrustum.html
//}
