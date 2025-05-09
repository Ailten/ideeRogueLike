using Raylib_cs;

public static class CanvasManager 
{

    private static Vector _sizeWindow = new(1280, 720); //size default of window.
    public static Vector sizeWindow
    {
        get { return _sizeWindow; }
    }
    public static Vector centerWindow
    {
        get { return sizeWindow / 2; }
    }
    private static Vector _posDecalCanvas = new(); //pos decal for all render (black band).
    public static Vector posDecalCanvas
    {
        get { return _posDecalCanvas; }
    }
    private static float _scaleCanvas = 1.0f; //scale of canvas (for scale every entities rendered, scale window).
    public static float scaleCanvas
    {
        get { return _scaleCanvas; }
    }
    private static int[] rectBlackBorder = {0,0,0,0, 0,0,0,0}; //array of two rect, for black border pos and size.
    private static Color backgroundColor = new Color(25, 25, 25, 255); //color of background canvas render default.

    public static bool isCloseWindow = false;
    public static bool isDebug = false;


    //init CanvasManager : open a window, with name, size ...
    public static void init(string nameWindow="window") 
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow); //window can be resize.
        Raylib.InitWindow((int)sizeWindow.x, (int)sizeWindow.y, nameWindow); //init window.
        Raylib.SetExitKey(KeyboardKey.Null); //replace escape key (not close the window).
    }

    public static void resizeWindow(Vector newSizeWindow)
    {
        float newRatioWindow = newSizeWindow.x / newSizeWindow.y; //ratio make by user.
        float ratioWindowNeed = sizeWindow.x / sizeWindow.y; //ratio want by dev.

        if(newRatioWindow < ratioWindowNeed){ //window is a square but canvas is an horizontal rectangle.
            //set scale canvas.
            _scaleCanvas = newSizeWindow.x / sizeWindow.x;

            //set decal canvas.
            _posDecalCanvas.x = 0.0f;
            _posDecalCanvas.y = (newSizeWindow.y - (newSizeWindow.x / ratioWindowNeed)) /2.0f;

            //set border black.
            rectBlackBorder[0] = 0; //rect top-left.
            rectBlackBorder[1] = 0;
            rectBlackBorder[2] = (int)newSizeWindow.x;
            rectBlackBorder[3] = (int)posDecalCanvas.y;
            rectBlackBorder[4] = 0; //rect down-right.
            rectBlackBorder[5] = (int)(newSizeWindow.y - posDecalCanvas.y);
            rectBlackBorder[6] = (int)newSizeWindow.x;
            rectBlackBorder[7] = (int)posDecalCanvas.y + 1;
        }else{ //window in an horizontal rectangle but canvas is a square.
            //set scale canvas.
            _scaleCanvas = newSizeWindow.y / sizeWindow.y;

            //set decal canvas.
            _posDecalCanvas.x = (newSizeWindow.x - (newSizeWindow.y * ratioWindowNeed)) /2.0f;
            _posDecalCanvas.y = 0.0f;

            //set border black.
            rectBlackBorder[0] = 0; //rect top-left.
            rectBlackBorder[1] = 0;
            rectBlackBorder[2] = (int)posDecalCanvas.x;
            rectBlackBorder[3] = (int)newSizeWindow.y;
            rectBlackBorder[4] = (int)(newSizeWindow.x - posDecalCanvas.x); //rect down-right.
            rectBlackBorder[5] = 0;
            rectBlackBorder[6] = (int)posDecalCanvas.x + 1;
            rectBlackBorder[7] = (int)newSizeWindow.y;
        }

    }

    //draw both black border (when window is not matching ratio).
    private static void drawBlackBorder()
    {
        Raylib.DrawRectangle(
            rectBlackBorder[0], 
            rectBlackBorder[1], 
            rectBlackBorder[2], 
            rectBlackBorder[3], 
            Color.Black
        );
        Raylib.DrawRectangle(
            rectBlackBorder[4], 
            rectBlackBorder[5], 
            rectBlackBorder[6], 
            rectBlackBorder[7], 
            Color.Black
        );
    }

    //draw in window every entities valid.
    public static void drawFrame()
    {
        // --- start drawing phase. --->
        Raylib.BeginDrawing();

        //clean last frame draw.
        Raylib.ClearBackground(backgroundColor);

        //draw all entities.
        EntityManager.entities
        .Where((e) =>  //filter.
            e.isActive &&
            LayerManager.getLayerByIdLayer(e.idLayer).isActive
        ).ToList().ForEach((e) => {

            //eval pos (gizmo) at screen.
            Vector posAtScreen = e.pos;

            if(e.isUi){ //replace on size canvas.
                posAtScreen *= scaleCanvas;
            }else{ //replace world pos to screen pos.

                posAtScreen -= CameraManager.posCam; //substract pos cam to replace all entity in cam view.

                posAtScreen *= CameraManager.zoomCam; //scale for zoom camera.

                posAtScreen += centerWindow * scaleCanvas;

                posAtScreen *= scaleCanvas; //scale to canvas resized.

            }

            posAtScreen += posDecalCanvas;


            //eval size at screen.
            Vector sizeAtScreen = e.size * e.scale * scaleCanvas;
            if(!e.isUi)
                sizeAtScreen *= CameraManager.zoomCam;

            //default get rect source.
            Rectangle rectSourceInTexture = e.sprite.getSpriteTileBySpriteType(e.spriteType).getRectSource();

            //default get rect destination at screen.
            Rectangle rectDest = new Rectangle(
                posAtScreen.x, posAtScreen.y,
                sizeAtScreen.x, sizeAtScreen.y
            );

            //default get gizmo.
            Vector origine = e.encrage * e.size * e.scale * scaleCanvas;
            if(!e.isUi)
                origine *= CameraManager.zoomCam;

            //draw at screen (with all data eval).
            Raylib.DrawTexturePro(
                e.sprite.texture, //texture.
                rectSourceInTexture, //rect source from texture.
                rectDest, //rect desintation at screen.
                origine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                e.rotate, //rotation.
                Color.White //color (already white).
            );


            //event draw after.
            e.drawAfter(posAtScreen, rectDest, origine);


            if(isDebug){

                //debug encrage.
                Raylib.DrawLine(
                    (int)posAtScreen.x -5, (int)posAtScreen.y -5,
                    (int)posAtScreen.x +5, (int)posAtScreen.y +5,
                    Color.Orange
                );
                Raylib.DrawLine(
                    (int)posAtScreen.x -5, (int)posAtScreen.y +5,
                    (int)posAtScreen.x +5, (int)posAtScreen.y -5,
                    Color.Orange
                );

                //debug geometry trigger.
                if(e.geometryTrigger != null){
                    Raylib.DrawRectangleLinesEx(
                        e.geometryTriggerNN.getRectAtScreen(e),
                        1,
                        Color.Orange
                    );
                }

            }

        });

        //draw transition layers.
        if(LayerManager.isTransitionActive){
            drawTransitionLayer();
        }

        //draw two black border over the render entities.
        drawBlackBorder();

        // --- end drawing phase. --->
        Raylib.EndDrawing();

    }

    //draw black screen low opacity, for transition between two layers.
    private static void drawTransitionLayer()
    {

        Vector sizeScreen = sizeWindow * scaleCanvas;

        Raylib.DrawRectangle(
            (int)posDecalCanvas.x, (int)posDecalCanvas.y,
            (int)sizeScreen.x, (int)sizeScreen.y,
            new Color((byte)0, (byte)0, (byte)0, (byte)(LayerManager.transitionOpacity * 255))
        );

    }

}