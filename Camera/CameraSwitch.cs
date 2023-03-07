using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    void Awake()
    {
        // Make sure only one camera is enabled at the start
        camera1.enabled = true;
        camera2.enabled = false;
    }

    public void SwitchCameras()
    {
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
    }
}