using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class UnitTest
{
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

    }

    [UnityTest]
    public IEnumerator EventQueueInstanceInitiated()
    {
        yield return null; //yield return null skips one frame, waits for the Unity scene to load

        //now test if a eventQueue is initiated
        Assert.IsNotNull(EventQueue.eventQueue, "No eventQueue in the scene");
    }
}
