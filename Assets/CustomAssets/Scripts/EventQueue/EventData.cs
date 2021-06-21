﻿using System.Collections;
using System.Collections.Generic;


public enum EventType
{
    UNITTESTS,
    STARTGAME,
    COLECTNECTAR,
    NECTARCOLLECTSTART,
    NECTARCOLLECTEND,
    NECTARSTORED,
    CHANGESTATESTART,
    CHANGESTATE,
    ENDSTATE,
    CHANGEZONE,
    PLAYERPESTICIDECOLLISION,
    FLOWERPESTICIDECOLLISION,
    CHECKDIFFICULTY,
    CHANGEDIFFICULTY,
    CHANGEPLAYERSTATE,
    PICKFLOWER,
    DESTROYFLOWER,
    NECTARCOLLECTTUTORIAL,
    NECTARONBEETEXTCHANGE,
    NECTARONTRUNKTEXTCHANGE,
    MINIGAMEFAIL,

    #region Sound Events
    PLAYMINIGAMESOUND
    #endregion
}
public class EventData 
{
    public EventType eventType;
    public EventData(EventType type)
    {
        eventType = type;
    }

}
