
public struct Rect
{

    public Vector posStart;
    public Vector size;


    public Rect(Vector posStart, Vector size)
    {
        this.posStart = posStart;
        this.size = size;
    }


    //get the rect replace at screen pos.
    public Rect getRectAtScreen(Entity e)
    {
        //eval pos (gizmo) at screen.
        Vector posAtScreen = e.pos;

        if(e.isUi){ //replace on size canvas.
            posAtScreen += (this.posStart * e.scale);

            posAtScreen *= CanvasManager.scaleCanvas;
        }else{ //replace world pos to screen pos.

            posAtScreen -= CameraManager.posCam; //substract pos cam to replace all entity in cam view.

            posAtScreen += (this.posStart * e.scale);

            posAtScreen *= CameraManager.zoomCam; //scale for zoom camera.

            posAtScreen += CanvasManager.centerWindow * CanvasManager.scaleCanvas;

            posAtScreen *= CanvasManager.scaleCanvas; //scale to canvas resized.

        }

        posAtScreen += CanvasManager.posDecalCanvas;


        //eval size at screen.
        Vector sizeAtScreen = this.size * e.scale * CanvasManager.scaleCanvas;
        if(!e.isUi)
            sizeAtScreen *= CameraManager.zoomCam;


        //build rect at screen.
        return new Rect(
            posAtScreen,
            sizeAtScreen
        );
    }


    public static implicit operator Raylib_cs.Rectangle(Rect a) => new(a.posStart, a.size);

    public static implicit operator Rect(Raylib_cs.Rectangle a) => new(new(a.X, a.Y), new(a.Width, a.Height));

}