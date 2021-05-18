﻿using System.Collections;
using System.Collections.Generic;


public enum EventType
{
    CLIENTCONNECT,
    CLIENTDISCONNESCT,
    STARTGAME,
    NECTARCOLLECTSTART,
    NECTARCOLLECTEND,
    NECTARSTORED,
    CHANGESTATE,
    CHANGEZONE,
    PLAYERPESTICIDECOLLISION,
    FLOWERPESTICIDECOLLISION
}
public class EventData 
{
    public EventType eventType;
    public EventData(EventType type)
    {
        eventType = type;
    }

}
