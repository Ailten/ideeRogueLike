
public class SuccessDetails : Entity
{
    private Succes success;
    public bool isUnlocked;
    public SuccessDetails(int idLayer, Succes success) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 3100;

        this.size = new(0, 0);

        this.success = success;
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {

        // print condition success.
        {
            string text = this.success.getConditionToUnlock();
            const float fontSize = 40f;
            float fontSizeScaled = fontSize * scale.y * CanvasManager.scaleCanvas;
            float fontSpacingSizeScaled = 2f * scale.y * CanvasManager.scaleCanvas;
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(
                StatusEffectUi.fontDescription,
                text,
                fontSizeScaled,
                fontSpacingSizeScaled
            );
            Vector posReplaceText = new(0f, -80f);

            Raylib_cs.Raylib.DrawTextEx(
                StatusEffectUi.fontDescription, //font.
                text, //txt.
                posToDraw + posReplaceText - sizeText * 0.5f, //pos in canvas.
                fontSizeScaled, //font size.
                fontSpacingSizeScaled, //space between two letter.
                (this.isUnlocked? Raylib_cs.Color.Blue: Raylib_cs.Color.DarkBlue) //color.
            );
        }

        // print placeholder when not unlocked.
        if (!this.isUnlocked)
        {

            const string text = "?";
            const float fontSize = 90f;
            float fontSizeScaled = fontSize * scale.y * CanvasManager.scaleCanvas;
            float fontSpacingSizeScaled = 2f * scale.y * CanvasManager.scaleCanvas;
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(
                StatusEffectUi.fontDescription,
                text,
                fontSizeScaled,
                fontSpacingSizeScaled
            );

            Raylib_cs.Raylib.DrawTextEx(
                StatusEffectUi.fontDescription, //font.
                text, //txt.
                posToDraw - sizeText * 0.5f, //pos in canvas.
                fontSizeScaled, //font size.
                fontSpacingSizeScaled, //space between two letter.
                Raylib_cs.Color.Black //color.
            );

        }
        else  // print when succes unlocked.
        {

            // print if is card.
            {
                Card? reward = this.success.getCardUnlocked();
                if (reward is not null)
                {
                    // draw card.
                    reward.drawCard(posToDraw, scale: 0.35f);
                }
            }

            // print if is character.
            {
                SpriteType? reward = this.success.getCharacterUnlocked();
                if (reward is not null)
                {
                    SpriteType rewardSpriteType = (SpriteType)reward;
                    Sprite rewardSprite = SpriteManager.findBySpriteType(rewardSpriteType) ?? throw new Exception("sprite not found !");
                    Rect rectDestReward = new(
                        posToDraw,
                        new Vector(126, 126)
                    );

                    Raylib_cs.Raylib.DrawTexturePro(
                        texture: rewardSprite.texture,
                        source: rewardSprite.getSpriteTileBySpriteType(rewardSpriteType).getRectSource(),
                        dest: rectDestReward,
                        origin: rectDestReward.size * 0.5f,
                        rotation: 0,
                        Raylib_cs.Color.White
                    );
                }
            }

            // print if is StatusEffect.
            {
                StatusEffectType? reward = this.success.getStatusEffectUnlocked();
                if (reward is not null)
                {
                    SpriteType rewardSpriteType = ((StatusEffectType)reward).getSpriteType();
                    Sprite rewardSprite = SpriteManager.findBySpriteType(rewardSpriteType) ?? throw new Exception("sprite not found !");
                    Rect rectDestReward = new(
                        posToDraw,
                        new Vector(63, 63) * 2f
                    );

                    Raylib_cs.Raylib.DrawTexturePro(
                        texture: rewardSprite.texture,
                        source: rewardSprite.getSpriteTileBySpriteType(rewardSpriteType).getRectSource(),
                        dest: rectDestReward,
                        origin: rectDestReward.size * 0.5f,
                        rotation: 0,
                        Raylib_cs.Color.White
                    );
                }
            }

        }

    }

}