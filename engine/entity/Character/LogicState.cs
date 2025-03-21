
public enum LogicState
{

    skipTurn, //skip turn and reset logic state index.

    chase, //run to the closest oponent.
    //chaseVariousDistance, //run to the closest oponent (at distance minimum to make the most long atk).
    //fear, //run to the most far position of every oponent.
    //runForHelpAlly, //run to the closest ally.
    //runForHelpAllyVariousDistance, //run to the closest ally (at distance minimum to make the most long atk).

    bigestHit,
    //tinnyestHit,
    //BigestSelfHelp,
    //TinnyestSelfHelp,
    //BigestAllyHelp,
    //TinnyestAllyHelp,

}