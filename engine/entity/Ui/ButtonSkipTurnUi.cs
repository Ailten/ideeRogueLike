
public class ButtonSkipTurnUi : ButtonUi
{

    public ButtonSkipTurnUi(int idLayer) : base(idLayer, SpriteType.ButtonUiSkipTurn)
    {
        this.isUi = true;
        this.zIndex = 2010;

        this.size = new(142, 108);

        this.geometryTrigger = new Rect(
            new(-65, -50), 
            new(130, 95)
        );

        this.castSpriteType.Add(SpriteType.ButtonUi, SpriteType.ButtonUiSkipTurn); //set all cast SpriteType for child.
        this.castSpriteType.Add(SpriteType.ButtonUi_Hover, SpriteType.ButtonUiSkipTurn_Hover);
        this.castSpriteType.Add(SpriteType.ButtonUi_Selected, SpriteType.ButtonUiSkipTurn_Selected);
        this.castSpriteType.Add(SpriteType.ButtonUi_Disabled, SpriteType.ButtonUiSkipTurn_Disabled);
    }

}