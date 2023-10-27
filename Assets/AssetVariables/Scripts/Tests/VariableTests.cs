using LovelyBytes.AssetVariables;
using NUnit.Framework;
using UnityEngine;

public class VariableTests
{
    [Test]
    public void Should_InvokeDelegateOnce_When_Fired()
    {
        IntVariable intVariable = ScriptableObject.CreateInstance<IntVariable>();
        int executionCount = 0;

        intVariable.OnValueChanged += (_, _) =>
        {
            ++executionCount;
        };

        intVariable.Value = 42;
        Assert.AreEqual(1, executionCount);
    }

    [Test]
    public void ShouldNot_ThrowException_When_Firing_If_NoSubscribers()
    {
        IntVariable intVariable = ScriptableObject.CreateInstance<IntVariable>();

        try
        {
            intVariable.Value = 42;
        }
        catch (System.NullReferenceException)
        {
            Assert.Fail();
        }
        Assert.Pass();
    }
    
    [Test]
    public void Should_ProvideCorrectOldValue_When_ValueChanged()
    {
        IntVariable intVariable = ScriptableObject.CreateInstance<IntVariable>();
        intVariable.Value = 42;

        intVariable.OnValueChanged += (oldValue, _) =>
        {
            Assert.AreEqual(42, oldValue);
        };

        intVariable.Value = 666;
    }
    
    [Test]
    public void Should_ProvideCorrectNewValue_When_ValueChanged()
    {
        IntVariable intVariable = ScriptableObject.CreateInstance<IntVariable>();
        intVariable.Value = 42;

        intVariable.OnValueChanged += (_, newValue) =>
        {
            Assert.AreEqual(666, newValue);
        };

        intVariable.Value = 666;
    }
    
    [Test]
    public void Should_UpdateValue_Before_ValueChangedDelegate()
    {
        IntVariable intVariable = ScriptableObject.CreateInstance<IntVariable>();

        intVariable.OnValueChanged += (oldValue, newValue) =>
        {
            Assert.AreEqual(intVariable.Value, newValue);
        };

        intVariable.Value = 42;
    }

    [Test]
    public void ShouldNot_InvokeDelegate_When_SetWithoutNotify()
    {
        IntVariable intVariable = ScriptableObject.CreateInstance<IntVariable>();

        intVariable.OnValueChanged += (_, _) =>
        {
            Assert.Fail();
        };

        intVariable.SetWithoutNotify(42);
        Assert.Pass();
    }

    // Helper class to perform tests whether OnBeforeSet is correctly applied
    private class TestVariable : Variable<int>
    {
        protected override void OnBeforeSet(ref int value)
        {
            value *= 2;
        }
    }

    [Test]
    public void Should_ApplyModifications_Before_Set()
    {
        TestVariable testVariable = ScriptableObject.CreateInstance<TestVariable>();
        testVariable.Value = 42;
        
        Assert.AreEqual(42 * 2, testVariable.Value);
    }
    
    [Test]
    public void Should_ApplyModifications_Before_SetWithoutNotify()
    {
        TestVariable testVariable = ScriptableObject.CreateInstance<TestVariable>();
        testVariable.SetWithoutNotify(42);
        
        Assert.AreEqual(42 * 2, testVariable.Value);
    }
}
