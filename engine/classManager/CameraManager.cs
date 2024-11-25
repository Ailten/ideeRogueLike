
public static class CameraManager
{
    private static Vector _posCam = new();
    public static Vector posCam
    {
        get { return _posCam; }
    }

    private static float _zoomCam = 1f;
    public static float zoomCam
    {
        get { return _zoomCam; }
    }


    //reset posCam to 0 or Vector set.
    public static void resetPosCam(Vector newPos=new())
    {
        _posCam = newPos;
    }

    //apply a movement to pos cam.
    public static void movePosCam(Vector movement)
    {
        float reverceZoom = ((zoomCam -1) *-1) +1;
        reverceZoom = Math.Max(reverceZoom, 0.3f);
        _posCam += movement * reverceZoom;
    }

    //reset zoomCam to 1.
    public static void resetZoomCam()
    {
        _zoomCam = 0.5f;
    }

    //edit zoom cam.
    public static void editZoomCam(float edit)
    {
        const float intencityZoomScale = 0.1f;
        float editedZoom = _zoomCam + edit * intencityZoomScale;

        const float zoomCamMin = 0.3f;
        const float zoomCamMax = 2.0f;
        _zoomCam = Math.Clamp(editedZoom, zoomCamMin, zoomCamMax);
    }

}