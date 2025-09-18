
public class CharacterGhost : CharacterMob
{
    public CharacterGhost(Vector posIndexCel) : base(SpriteType.Character_Ghost, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.firstRetMP);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.fuit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 5;
        this.AP = APmax;
        this.HPmax = 30;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(6, 11);

        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 2.5f)); // res negative on shiny card.
        this.AddStatusEffect(new PushWallMakeRallMP(this.idEntity, -1, -1, 1)); // push to wall make 1 rall mp.

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Drama,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 3,
                distanceToUse: new(3, 5),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.RetMP, 1)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Splash,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 3),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 1),
                    new KeyValuePair<EffectCard, int>(EffectCard.Push, 1)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        
    }
}