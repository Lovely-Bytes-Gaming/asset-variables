using LovelyBytes.AssetVariables;
using NUnit.Framework;
using UnityEngine;

public class TriggerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Should_InvokeDelegateOnce_When_Fired()
    {
        TriggerVariable triggerVariable = ScriptableObject.CreateInstance<TriggerVariable>();
        int executionCount = 0;

        triggerVariable.OnTriggerFired += () =>
        {
            ++executionCount;
        };
        
        triggerVariable.Fire();
        Assert.AreEqual(1, executionCount);
    }

    [Test]
    public void ShouldNot_ThrowException_When_Firing_If_NoSubscribers()
    {
        TriggerVariable triggerVariable = ScriptableObject.CreateInstance<TriggerVariable>();

        try
        {
            triggerVariable.Fire();
        }
        catch (System.NullReferenceException)
        {
            Assert.Fail();
        }
        Assert.Pass();
    }

    // TODO: Find out how we can create the trigger on the main thread and Fire it on another thread
    // [Test]
    // public void Should_ThrowException_When_NotMainThread()
    // {
    //     var triggerVariable = ScriptableObject.CreateInstance<TriggerVariable>();
    //     Assert.Fail();
    // }

    [Test]
    public void Should_ThrowException_When_CalledRecursively()
    {
        TriggerVariable triggerVariable = ScriptableObject.CreateInstance<TriggerVariable>();

        int executionCount = 0;
        triggerVariable.OnTriggerFired += () =>
        {
            if (executionCount > 1)
                return;

            ++executionCount;
            triggerVariable.Fire();
        };

        Assert.Throws<ValidatorException>(triggerVariable.Fire);
    }
}
