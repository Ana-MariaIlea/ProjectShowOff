using System.Collections;
using System.Collections.Generic;


public enum EventType
{
    #region System events
    UNITTESTS,
    STARTGAME,
    CHECKDIFFICULTY,
    CHANGEDIFFICULTY,
    NECTARCOLLECTTUTORIAL,
    #endregion
    #region Nectar&Flower events
    COLECTNECTAR,
    NECTARCOLLECTSTART,
    NECTARCOLLECTEND,
    NECTARSTORED,
    PICKFLOWER,
    DESTROYFLOWER,
    FLOWERPESTICIDECOLLISION,
    #endregion
    #region Player events
    CHANGEPLAYERSTATE,
    PLAYERPESTICIDECOLLISION,
    CHANGEZONE,
    MINIGAMEFAIL,
    #endregion
    #region Human events
    CHANGESTATESTART,
    CHANGESTATE,
    ENDSTATE,
    HANDLEHUMANOBJECT,
    HANDLEHUMANPARTICLE,
    #endregion
    #region UI events
    NECTARONBEETEXTCHANGE,
    NECTARONTRUNKTEXTCHANGE,
    #endregion
    #region Sound Events
    PLAYMINIGAMESOUND,
    PLAYTUTORIALSOUND,
    PLAYSCOREINCREASESOUND,
    PLAYSPRAYPARTICLESSOUND,
    PLAYBEETAKEOFFSOUND,
    PLAYBEELANDINGSOUND
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
