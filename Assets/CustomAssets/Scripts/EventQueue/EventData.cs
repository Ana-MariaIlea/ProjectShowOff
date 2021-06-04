using System.Collections;
using System.Collections.Generic;


public enum EventType
{
    CLIENTCONNECT,
    CLIENTDISCONNESCT,
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
    DESTROYFLOWER
}
public class EventData 
{
    public EventType eventType;
    public EventData(EventType type)
    {
        eventType = type;
    }

}
