
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

        //TODO: print light ray with rotation.

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

            { // block for free reward.
                Card? reward = currentSucces.getCardUnlocked();
                if (reward is not null)
                {
                    // draw card.
                    reward.drawCard(posToDraw);
                }
            }

            { // block for free reward.
                SpriteType? reward = currentSucces.getCharacterUnlocked();
                if (reward is not null)
                {
                    CharacterUi character = new CharacterUi(idLayer, (SpriteType)reward);
                    character.isDrawPseudo = false;
                    EntityManager.sortAllEntities();
                }
            }

            { // block for free reward.
                StatusEffectType? reward = currentSucces.getStatusEffectUnlocked();
                if (reward is not null)
                {
                    StatusEffectDetailsUi seUi = new StatusEffectDetailsUi(idLayer);
                    seUi.setStatusEffect(StaticStatusEffectType.getStatusEffect((StatusEffectType)reward, -1));
                    seUi.isPrintDetails = false;
                    EntityManager.sortAllEntities();
                }
            }


        }
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
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