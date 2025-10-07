
public enum LogicState
{
    // skip turn.
    skipTurn, //skip turn and reset logic state index.

    // every state.
    chase, //run to the closest oponent.
    firstHit, //execute the first card in hand who has at least one effect "hit" (and if the target is on the right dist).
    firstCardPlayableOponent, //execute the first card usable on a target oponent (and not usable on it self).
    firstAttire, //execute the first card in hand who has effect "attire" (on target ennemy).
    shildAlly, //use the first card has effect Shild on an ally on range card (with lower HP).
    fuit, //run in oposit direction of oponent.
    selfShild, //use first card Shild effect on self.
    firstRetMP, //use first card making ret mp.
    firstCardPlayableOnEmpty, //use a card on a empty case (ex: use for invoke).
    firstOneAPCardOnOponent, //use the first at one AP to an oponent.
    firstTwoAPCardOnOponent, //same 2.
    firstTreeAPCardOnOponent, //same 3.
    firstFourAPCardOnOponent, //same 4.
    firstSelfCard, //playe the first card makable on self (dist 0 min).

    // or.
    chase_or_firstAttire,

    // if.
    chase_ifCardInHand,
    doNextStateIfHpIsUnderTenPurcent
}