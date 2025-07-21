
// class for draw a time line of turn character in batle.
public class TimeLineUi : Entity
{

    public TimeLineUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 1990; // under skip turn back Ui to 2000.

        this.size = new(0, 0);
        this.encrage = new(0, 0);
    }

    private static float leftReplacement = -75;
    private static Vector rightArrowCurrentTurn = new(32, 32);
    private static Vector statusEffectSize = new(63, 63);
    private static Vector separatorTurnSize = new(32, 126);
    private static float heightSizeSpacing = 10;
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (!TurnManager.isInFight)
            return;

        List<Character> charactersInFight = TurnManager.getAllCharacters();
        int indexCurrentTurn = TurnManager.getIndexCharacterTurn;

        float leftReplacementScaled = leftReplacement * scale.x * CanvasManager.scaleCanvas;
        Sprite spriteEffect = SpriteManager.findBySpriteType(SpriteType.StatusEffect_BGStatusEffect) ?? throw new Exception("Sprite not found !");
        Vector rightArrowCurrentTurnScaled = rightArrowCurrentTurn * scale * CanvasManager.scaleCanvas;
        Vector statusEffectSizeScaled = statusEffectSize * scale * CanvasManager.scaleCanvas;
        Vector separatorTurnSizeScaled = separatorTurnSize * scale * CanvasManager.scaleCanvas;
        float heightSizeSpacingScaled = heightSizeSpacing * scale.y * CanvasManager.scaleCanvas;

        // draw arrow current turn character.
        Rect rectDestDraw = new(
            (
                posToDraw +
                new Vector(leftReplacementScaled, statusEffectSizeScaled.y / 2) +
                new Vector(-rightArrowCurrentTurnScaled.x, -rightArrowCurrentTurnScaled.y / 2)
            ),
            rightArrowCurrentTurnScaled
        );
        Raylib_cs.Raylib.DrawTexturePro( // draw arrow current turn.
            texture: spriteEffect.texture,
            source: spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_arrowRightTimeLine).getRectSource(),
            dest: rectDestDraw,
            origine,
            0,
            Raylib_cs.Color.White
        );

        const int maxCharacterPrintInTimeLine = 7;
        int loopPrintCharacterCeil = Math.Min(charactersInFight.Count, maxCharacterPrintInTimeLine);
        bool isUnderEndTurnBar = false;
        for (int i = 0; i < loopPrintCharacterCeil; i++)
        {
            int indexClamped = (indexCurrentTurn + i) % charactersInFight.Count; // remap index i for start by char turn.
            Character characterPrint = charactersInFight[indexClamped];

            rectDestDraw = new(
                (
                    posToDraw +
                    new Vector(leftReplacementScaled, (statusEffectSizeScaled.y + heightSizeSpacingScaled) * i) +
                    (isUnderEndTurnBar? new Vector(0, separatorTurnSizeScaled.x / 2 - heightSizeSpacingScaled): new Vector(0, 0))
                ),
                statusEffectSizeScaled
            );

            Raylib_cs.Raylib.DrawTexturePro( // draw background character.
                texture: spriteEffect.texture,
                source: spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_BGStatusEffect).getRectSource(),
                dest: rectDestDraw,
                origine,
                0,
                Raylib_cs.Color.White
            );
            Raylib_cs.Raylib.DrawTexturePro( // draw illu character.
                texture: characterPrint.sprite.texture,
                source: characterPrint.sprite.getSpriteTileBySpriteType(characterPrint.spriteType).getRectSource(),
                dest: rectDestDraw,
                origine,
                0,
                Raylib_cs.Color.White
            );

            bool isLastCharacterList = (indexClamped == charactersInFight.Count - 1);
            bool isAnotherCharacterToPrintAfter = (i != loopPrintCharacterCeil - 1);
            if (isLastCharacterList && isAnotherCharacterToPrintAfter)
            {
                isUnderEndTurnBar = true;

                rectDestDraw = new(
                    (
                        posToDraw +
                        new Vector(leftReplacementScaled + statusEffectSizeScaled.x / 2, (statusEffectSizeScaled.y + heightSizeSpacingScaled) * i) +
                        new Vector(0, statusEffectSizeScaled.y + separatorTurnSizeScaled.x / 4)
                    ),
                    separatorTurnSizeScaled
                );
                Raylib_cs.Raylib.DrawTexturePro( // draw illu character.
                    texture: spriteEffect.texture,
                    source: spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_TimeLineDelimiterTurn).getRectSource(),
                    dest: rectDestDraw,
                    (separatorTurnSizeScaled / 2),
                    90,
                    Raylib_cs.Color.White
                );

            }

        }
    }

}