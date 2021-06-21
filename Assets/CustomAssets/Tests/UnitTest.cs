using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class UnitTest
{

    DifficultyChecksTimer difficultyChecksTimer;
    PlayerMotor playerMotor;
    PlayerStateMachine playerStateMachine;
    BounderyDetection bounderyDetection;
    //Setup the test scene
    [OneTimeSetUp]
    public void LoadShopScene()
    {
        // Load the Scene to do unit test. In the scope of this project, this is fine. In a more complicated project, a game scene could take
        // a long time to load, in which case it's better to create test scenes to do unit tests
        SceneManager.LoadScene(2);
    }

    //Setup the unit tests here
    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        yield return null; //yield return null skips one frame, this is to make sure that this happens after the scene is loaded
        difficultyChecksTimer = Resources.FindObjectsOfTypeAll<DifficultyChecksTimer>()[0];
        playerMotor = Resources.FindObjectsOfTypeAll<PlayerMotor>()[0];
        playerStateMachine = Resources.FindObjectsOfTypeAll<PlayerStateMachine>()[0];
        bounderyDetection = Resources.FindObjectsOfTypeAll<BounderyDetection>()[0];
    }

    //------------------------------------------------------------------------------------------------
    //                                              EventQueue Tests
    //------------------------------------------------------------------------------------------------
    [UnityTest]
    public IEnumerator EventQueueInstanceInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(EventQueue.eventQueue, "No eventQueue in the scene");


    }
    [UnityTest]
    public IEnumerator EventQueueThrowsExceptionsWhenUnsubscribingWithInvalidEvetType()
    {
        //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
        yield return null;

        //Creates a delegate that call gridBuyView.ShopModel.SelectItemByIndex(-1), the test runner will run the function, and
        //check if an ArgumentOutOfRangeException is thrown, the unit test would fail if no ArgumentOutOfRangeException
        //was thrown
        Assert.Throws<System.ArgumentOutOfRangeException>(delegate
        {
            EventQueue.eventQueue.UnSubscribe(EventType.UNITTESTS, null);
        });
    }

    [UnityTest]
    public IEnumerator EventQueueThrowsExceptionsWhenPublishingAnEventWithInvalidEvetType()
    {
        //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
        yield return null;

        //Creates a delegate that call gridBuyView.ShopModel.SelectItemByIndex(-1), the test runner will run the function, and
        //check if an ArgumentOutOfRangeException is thrown, the unit test would fail if no ArgumentOutOfRangeException
        //was thrown
        Assert.Throws<System.ArgumentOutOfRangeException>(delegate
        {
            EventQueue.eventQueue.AddEvent(new EventData(EventType.UNITTESTS));
            EventQueue.eventQueue.PublishEvents();
        });
    }
    //------------------------------------------------------------------------------------------------
    //                                              System Tests
    //------------------------------------------------------------------------------------------------

    [UnityTest]
    public IEnumerator GameSessionStatsInstanceInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(GameSessionStats.instance, "No GameSeesionStats in the scene");
    }

    //This case tests if the grid buy view displays the correct amount of Items
    [UnityTest]
    public IEnumerator ZoneListIsNotEmpty()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        Assert.NotZero(GameSessionStats.instance.GetZoneListCount());
    }

    //This case tests if the grid buy view displays the correct amount of Items
    [UnityTest]
    public IEnumerator SettingsListIsNotEmpty()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        Assert.NotZero(GameSessionStats.instance.GetDifficultyListCount());
    }
    //[UnityTest]
    //public IEnumerator GameSessionStatsStartWithDifficultyListEmptyException()
    //{
    //    //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
    //    yield return null;


    //    Assert.Throws<System.ArgumentOutOfRangeException>(delegate
    //    {
    //        GameSessionStats.instance.Start();
    //    });
    //}

    [UnityTest]
    public IEnumerator GameSessionStatsChangeDifficultyWithWrongEventException()
    {
        //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
        yield return null;


        Assert.Throws<System.ArgumentOutOfRangeException>(delegate
        {
            GameSessionStats.instance.OnCheckDifficulty(new EventData(EventType.UNITTESTS));
        });
    }

    [UnityTest]
    public IEnumerator GameSessionStatsChangeDifficultyWithEmptyDifficultyListException()
    {
        //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
        yield return null;

        GameSessionStats.instance.ResetDifficultyList();
        //CheckDifficultyEventData e =new CheckDifficultyEventData()
        Assert.Throws<System.ArgumentOutOfRangeException>(delegate
        {
            GameSessionStats.instance.OnCheckDifficulty(new EventData(EventType.CHANGEDIFFICULTY));
        });
    }

    [UnityTest]
    public IEnumerator DifficultyChecksListIsNotEmpty()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        Assert.NotZero(difficultyChecksTimer.GetDifficultyChecksListCount());
    }
    //------------------------------------------------------------------------------------------------
    //                                              Player Tests
    //------------------------------------------------------------------------------------------------
    [UnityTest]
    public IEnumerator PlayerMotorControllerIsInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(playerMotor.GetPlayerControllerStates(), "No PLayerControllerStates");
    }

    [UnityTest]
    public IEnumerator PlayerMotorEffectsIsInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(playerMotor.GetPlayerEffectStates(), "No PLayerControllerEffects");
    }
    [UnityTest]
    public IEnumerator PlayerMotorInStateMachineIsInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(playerStateMachine.GetPLayerMotor(), "No GameSeesionStats in the scene");
    }
    [UnityTest]
    public IEnumerator QTEInStateMachineIsInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(playerStateMachine.GetQTESystem(), "No GameSeesionStats in the scene");
    }
    [UnityTest]
    public IEnumerator BounderiessInStateMachineIsInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(playerStateMachine.GetBounderiesDetection(), "No GameSeesionStats in the scene");
    }
    [UnityTest]
    public IEnumerator NectarHandlerInStateMachineIsInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(playerStateMachine.GetNectarHandler(), "No GameSeesionStats in the scene");
    }

    [UnityTest]
    public IEnumerator CenterOfMapIsInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(bounderyDetection.getCenterOfMap(), "No GameSeesionStats in the scene");
    }
    //------------------------------------------------------------------------------------------------
    //                                              HumanAI Tests
    //------------------------------------------------------------------------------------------------
}
