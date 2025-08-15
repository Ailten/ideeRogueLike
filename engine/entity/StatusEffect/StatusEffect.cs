
// a parent object for effect on a character (edit process of the caractere during battle).
public class StatusEffect
{
    private int turnStart;
    private int turnEnd;
    private int characterIdWhoApplyEffect;
    private int characterIdWhoHasEffect;
    protected SpriteType spriteType;
    private int idEffect;
    private static int idEffectCount = 0;

    public int getTurnEnd
    {
        get { return turnEnd; }
    }
    public int getTurnUntilEnd
    {
        get { return (turnEnd >= 0 ? turnEnd - TurnManager.getTurnCount : -1); }
    }
    public bool isInfinitTurn
    {
        get { return turnEnd < 0; }
    }
    public int getCharacterIdWhoApplyEffect
    {
        get { return characterIdWhoApplyEffect; }
    }
    public int getCharacterIdWhoHasEffect
    {
        get { return characterIdWhoHasEffect; }
    }

    protected Character? getCharacterWhoApplyEffect
    {
        get { return TurnManager.getAllCharacters().FirstOrDefault(c => c.idEntity == this.characterIdWhoApplyEffect); }
    }
    protected Character getCharacterWhoHasEffect
    {
        get { return TurnManager.getAllCharacters().Find(c => c.idEntity == this.characterIdWhoHasEffect) ?? throw new Exception("CharacterHasApplyEffect has not found in TurnManager !"); }
    }

    public SpriteType GetSpriteType
    {
        get { return spriteType; }
    }

    public int getIdEffect
    {
        get { return this.idEffect; }
    }


    public StatusEffect(SpriteType spriteType, int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1)
    {
        this.spriteType = spriteType;

        this.turnStart = TurnManager.getTurnCount;
        this.turnEnd = (turnLife < 0) ? turnLife : this.turnStart + turnLife;

        this.characterIdWhoHasEffect = characterIdWhoHasEffect;
        this.characterIdWhoApplyEffect = characterIdWhoApplyEffect;

        this.idEffect = idEffectCount++;
    }

    // description of effect.
    public virtual string getDescription()
    {
        throw new Exception("StatusEffect has no description overided !");
    }
    protected string getDescriptionTurn()
    {
        Character? whoApply = getCharacterWhoApplyEffect;
        string nameWhoApply = (whoApply is not null ? whoApply.getName : "---");
        string turnUntil = (getTurnUntilEnd >= 0 ? getTurnUntilEnd.ToString() + "T" : "infini");
        return (
            $"- cible : {getCharacterWhoHasEffect.getName}, " +
            $"par : {nameWhoApply}, " +
            $"dure : {turnUntil}"
        );
    }
    protected virtual string getName()
    {
        return "No name";
    }

    public virtual bool isAMalus()
    {
        return false;
    }

    public SpriteType getBackgroundSprite()
    {
        return (
            (this.isInfinitTurn)? SpriteType.StatusEffect_BGStatusEffectBuff:
            (this.isAMalus())? SpriteType.StatusEffect_BGStatusEffectMalus:
            SpriteType.StatusEffect_BGStatusEffect
        );
    }


    // event call when effect end life.
    public virtual void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false
    )
    { }

    // event call when target start turn.
    public void eventWhenTargetStartTurn() { }
    // event call when target end turn.
    public virtual void eventWhenTargetEndTurn() { }
    // event call when target make damage.
    public virtual void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard) { }


}