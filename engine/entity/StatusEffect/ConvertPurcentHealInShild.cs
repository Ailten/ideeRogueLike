
public class ConvertPurcentHealInShild : StatusEffect
{
    float purcentConvert;

    public ConvertPurcentHealInShild(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, float purcentConvert = 0.5f) :
    base(SpriteType.StatusEffect_ConvertPurcentHealInShild, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.purcentConvert = purcentConvert;
    }


    public override string getDescription()
    {
        int purcent = (int)(this.purcentConvert * 100);
        return (
            $"- {this.getName()} :\n" +
            $"converti {purcent}% des soin realise en bouclier.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Convertion soin bouclier";
    }
    public override bool isAMalus()
    {
        return this.purcentConvert < 0f;
    }


    public override void eventWhenMakeAHeal(ref Character target, ref int healIncrement, ref PackageRefCard? refCard)
    {
        int convertAmount = (int)(healIncrement * this.purcentConvert);

        healIncrement -= convertAmount;

        this.getCharacterWhoHasEffect.giveShild(target, convertAmount, refCard);
    }
}