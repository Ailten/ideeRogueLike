
public class ConvertPurcentHealInShild : StatusEffect
{
    float purcentConvert;

    public ConvertPurcentHealInShild(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, float purcentConvert = 0.5f) :
    base(SpriteType.StatusEffect_ConvertPurcentHealInShild, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.purcentConvert = purcentConvert;
    }
}