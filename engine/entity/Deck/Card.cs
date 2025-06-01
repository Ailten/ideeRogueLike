
public struct Card
{

    public SpriteType cardIllu; //sprite of illustation card.
    public CardColor cardColor; //sprite of background card.
    public CardEdition cardEdition; //special effect upper the card.
    public bool isRecto = false; //to know if it has to print masked.

    public int APCost; //the amount of AP need for use the card.
    public Vector distanceToUse; //distance can be use (x=min, y=max).

    public List<KeyValuePair<EffectCard, int>> effects; //all effects of a card (with an int of value).


    //default constructor.
    public Card(SpriteType cardIllu, CardColor? cardColor = null, CardEdition cardEdition = CardEdition.Default, int APCost = 0, Vector distanceToUse = new(), List<KeyValuePair<EffectCard, int>>? effects = null)
    {
        this.cardIllu = cardIllu;
        this.cardColor = cardColor ?? StaticCardColor.getRandomColor();
        this.cardEdition = cardEdition;
        this.APCost = APCost;
        this.distanceToUse = distanceToUse;
        this.effects = effects ?? new();
    }

    //constructor with single effect.
    public Card(SpriteType cardIllu, CardColor? cardColor = null, CardEdition cardEdition = CardEdition.Default, int APCost = 0, Vector distanceToUse = new(), KeyValuePair<EffectCard, int>? effect = null)
    {
        this.cardIllu = cardIllu;
        this.cardColor = cardColor ?? StaticCardColor.getRandomColor();
        this.cardEdition = cardEdition;
        this.APCost = APCost;
        this.distanceToUse = distanceToUse;
        this.effects = new();
        if(effect != null)
            this.effects.Add(effect ?? throw new Exception("Card effect is null !"));
    }


    //use a card in battle (do this effect).
    public static void useACard(Character characterLauncher, Card cardToUse, Vector posTarget)
    {
        //loop on every effect of the card.
        for(int i = 0; i<cardToUse.effects.Count; i++){
            EffectCard currentEffectType = cardToUse.effects[i].Key;
            int currentEffectValue = cardToUse.effects[i].Value;

            Character? characterTarget = TurnManager.getCharacterAtIndexPos(posTarget);

            switch(currentEffectType){
                case(EffectCard.hit):
                    if(characterTarget == null)
                        continue;
                    effectCardHit(characterLauncher, currentEffectValue, characterTarget);
                    break;

                case(EffectCard.shild):
                    if(characterTarget == null)
                        continue;
                    effectCardShild(characterLauncher, currentEffectValue, characterTarget);
                    break;

                default:
                    throw new Exception($"useACard find a EffectCard with no effect {currentEffectType} !");
            }

        }
    }
    private static void effectCardHit(Character characterLauncher, int effectValue, Character characterTarget)
    {
        characterLauncher.makeDamage(characterTarget, effectValue);
    }
    private static void effectCardShild(Character characterLauncher, int effectValue, Character characterTarget)
    {
        characterLauncher.giveShild(characterTarget, effectValue);
    }


}