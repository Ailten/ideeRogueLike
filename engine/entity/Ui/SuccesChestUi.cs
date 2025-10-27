
// class specify for chest on layer endRun.
public class SuccesChestUi : Entity
{
    private static Vector sizeSuccesChest = new(355, 274);
    private List<Succes> listSucces = new();
    private int indexSucces = 0;
    private bool isPrintTheChest = true;

    public SuccesChestUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(0, 0);

        this.geometryTrigger = new Rect(
            sizeSuccesChest * -0.5f,
            sizeSuccesChest
        );
    }

    // set list succes.
    public void setListSucces(List<Succes> listSucces)
    {
        this.listSucces = listSucces;
        this.indexSucces = 0;
        this.isPrintTheChest = true;
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        // nothing more to print.
        if (this.indexSucces >= this.listSucces.Count)
        {
            return;
        }

        // print light ray with rotation.
        const int rayCount = 3;
        Vector sizeRayScaled = new Vector(122, 274) * this.scale * CanvasManager.scaleCanvas;
        const float speedRay = 0.009f;
        for (int i = 0; i < rayCount; i++)
        {
            float scaleRay = (float)Math.Cos((double)UpdateManager.timeSpeedForAnime(EndRunLayer.layer.milisecInLevel) * 0.002);
            scaleRay = (scaleRay + 1) /8 + (1f - 1/8f);

            Vector sizeScaledZoom = sizeRayScaled * scaleRay;
            Rect rectDestRay = new Rect(
                posToDraw, //- sizeRayScaled * new Vector(0.5f, 1f),
                sizeScaledZoom
            );
            Vector origineRay = sizeScaledZoom * new Vector(0.5f, 1f);

            float rotateRay = Vector.lerpF(0, 360, (float)i / rayCount);
            rotateRay += UpdateManager.timeSpeedForAnime(EndRunLayer.layer.milisecInLevel) * speedRay;

            Raylib_cs.Raylib.DrawTexturePro(
                sprite.texture, //texture.
                sprite.getSpriteTileBySpriteType(SpriteType.UiGodRayWin).getRectSource(), //rect source from texture.
                rectDestRay, //rect desintation at screen.
                origineRay, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                rotateRay, //rotation.
                Raylib_cs.Color.White //color (already white).
            );
        }

        if (this.isPrintTheChest)
        {
            Vector sizeChestScaled = sizeSuccesChest * this.scale * CanvasManager.scaleCanvas;
            Rect destDraw = new Rect(
                posToDraw - sizeChestScaled * 0.5f,
                sizeChestScaled
            );

            Raylib_cs.Raylib.DrawTexturePro( // draw chest.
                texture: this.sprite.texture,
                source: this.sprite.getSpriteTileBySpriteType(SpriteType.UiCoffreWin).getRectSource(),
                dest: destDraw,
                origin: origine,
                rotation: 0,
                Raylib_cs.Color.White
            );
        }
        else
        {
            Succes currentSucces = this.listSucces[this.indexSucces];

            // print description.
            string descriptionSucces = currentSucces.getConditionToUnlock();
            const float fontSizeDescription = 40f;
            float fontSizeEval = fontSizeDescription * scale.y * CanvasManager.scaleCanvas; //eval font size and spacing.
            float fontSpacingEval = 2f * scale.y * CanvasManager.scaleCanvas;
    
            Vector textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
                StatusEffectUi.fontDescription,
                descriptionSucces,
                fontSizeEval,
                fontSpacingEval
            );
    
            Vector posReplaceTextAtScreen = new Vector(0, -150); //vector to replace text from center entity.
            posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;
    
            Raylib_cs.Raylib.DrawTextEx(
                StatusEffectUi.fontDescription, //font.
                descriptionSucces, //txt.
                posToDraw + posReplaceTextAtScreen - textRectDest * new Vector(0.5f, 0.5f), //pos in canvas.
                fontSizeEval, //font size.
                fontSpacingEval, //space between two letter.
                Raylib_cs.Color.Blue //color.
            );

            { // block for free reward.
                Card? reward = currentSucces.getCardUnlocked();
                if (reward is not null)
                {
                    // draw card.
                    reward.drawCard(posToDraw, scale: 0.8f);
                }
            }

            { // block for free reward.
                SpriteType? reward = currentSucces.getCharacterUnlocked();
                if (reward is not null)
                {
                    CharacterUi character = new CharacterUi(idLayer, (SpriteType)reward);
                    character.pos = this.pos;
                    character.scale = new(1.8f, 1.8f);
                    character.isDrawPseudo = true;
                    EntityManager.sortAllEntities();
                }
            }

            { // block for free reward.
                StatusEffectType? reward = currentSucces.getStatusEffectUnlocked();
                if (reward is not null)
                {
                    StatusEffectDetailsUi seUi = new StatusEffectDetailsUi(idLayer);
                    seUi.pos = this.pos;
                    seUi.scale = new(2.5f, 2.5f);
                    seUi.setStatusEffect(StaticStatusEffectType.getStatusEffect((StatusEffectType)reward, -1));
                    seUi.isPrintDetails = false;
                    seUi.isPrintNameUnder = true;
                    EntityManager.sortAllEntities();
                }
            }


        }
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (isClickDown) // prevent double click.
            return;

        this.isPrintTheChest = !this.isPrintTheChest;
        if (this.isPrintTheChest)
            this.indexSucces++;

        // remove reward instanciated.
        Entity[] entityToDel = EntityManager.getEntitiesByIdLayer(idLayer)
            .Where(e => e is CharacterUi || e is StatusEffectDetailsUi)
            .ToArray();
        for (int i = entityToDel.Length - 1; i >= 0; i--) {
            EntityManager.removeOneEntity(entityToDel[i]);
        }
    }


}