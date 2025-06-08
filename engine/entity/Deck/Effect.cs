
public enum EffectCard
{
    NoEffect, //no effect.

    Hit, //make damage to the target.
    
    Shild //add shild to the target.
}


public static class StaticEffectCard
{
    public static string getDetails(this EffectCard effectCard)
    {
        switch(effectCard)
        {
            case(EffectCard.NoEffect):
                return "ne fait rien.";
            case(EffectCard.Hit):
                return ("- "+effectCard.ToString()+" :\n"+
                    "effectue N points de degat a la cible.\n"+
                    "arrivé a 0 points de vie, la cible meure."
                );
            case(EffectCard.Shild):
                return ("- "+effectCard.ToString()+" :\n" +
                    "donne N points de bouclier a la cible.\n"+
                    "les points de bouclier protege les points de vie.\n"+
                    "les points de bouclier sont perdu en debut de tour."
                );
            default:
                return "cette effet n'a pas de description.";
        }
    }
}