
public class LittleButtonUi : ButtonUi
{

    public LittleButtonUi(int idLayer) : base(idLayer, SpriteType.CheckBoxUi)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(93, 92);

        this.geometryTrigger = new Rect(
            new(-45, -45), 
            new(90, 90)
        );

        this.castSpriteType.Add(SpriteType.ButtonUi, SpriteType.CheckBoxUi); //set all cast SpriteType for child.
        this.castSpriteType.Add(SpriteType.ButtonUi_Hover, SpriteType.CheckBoxUi_Hover);
        this.castSpriteType.Add(SpriteType.ButtonUi_Selected, SpriteType.CheckBoxUi_Selected);
    }



}