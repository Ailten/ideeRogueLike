
public class ButtonOptionUi : ButtonUi
{
    public ButtonOptionUi(int idLayer) : base(idLayer, SpriteType.ButtonUiOption)
    {
        this.isUi = true;
        this.zIndex = 2010;

        this.size = new(202, 109);

        this.geometryTrigger = new Rect(
            new(-99, -51),
            new(202, 109)
        );

        this.castSpriteType.Add(SpriteType.ButtonUi, SpriteType.ButtonUiOption); //set all cast SpriteType for child.
        this.castSpriteType.Add(SpriteType.ButtonUi_Hover, SpriteType.ButtonUiOption_Hover);
        this.castSpriteType.Add(SpriteType.ButtonUi_Selected, SpriteType.ButtonUiOption_Selected);
        this.castSpriteType.Add(SpriteType.ButtonUi_Disabled, SpriteType.ButtonUiOption_Disabled);
    }
}