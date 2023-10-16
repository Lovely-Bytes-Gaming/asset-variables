using System.Collections;
using System.Collections.Generic;
using LovelyBytes.AssetVariables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ObservableTest
{
    // A Test behaves as an ordinary method
[Test]
    public void Should_InvokeDelegateOnce_When_Fired()
    {
        var observable = new Observable<int>();
        int executionCount = 0;

        observable.OnValueChanged += (_, _) =>
        {
            ++executionCount;
        };

        observable.Value = 42;
        Assert.AreEqual(1, executionCount);
    }

    [Test]
    public void Should_ThrowNoException_When_Firing_If_NoSubscribers()
    {
        var observable = new Observable<int>();

        try
        {
            observable.Value = 42;
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
        var observable = new Observable<int>(42);

        observable.OnValueChanged += (oldValue, _) =>
        {
            Assert.AreEqual(42, oldValue);
        };

        observable.Value = 666;
    }
    
    [Test]
    public void Should_ProvideCorrectNewValue_When_ValueChanged()
    {
        var observable = new Observable<int>(42);

        observable.OnValueChanged += (_, newValue) =>
        {
            Assert.AreEqual(666, newValue);
        };

        observable.Value = 666;
    }
    
    [Test]
    public void Should_UpdateValue_Before_ValueChangedDelegate()
    {
        var observable = new Observable<int>();

        observable.OnValueChanged += (oldValue, newValue) =>
        {
            Assert.AreEqual(observable.Value, newValue);
        };

        observable.Value = 42;
    }

    [Test]
    public void ShouldNot_InvokeDelegate_When_SetWithoutNotify()
    {
        var observable = new Observable<int>();

        observable.OnValueChanged += (_, _) =>
        {
            Assert.Fail();
        };

        observable.SetWithoutNotify(42);
        Assert.Pass();
    }

    // Helper class to perform tests whether OnBeforeSet is correclty applied
    private class TestObservable : Observable<int>
    {
        protected override void OnBeforeSet(ref int value)
        {
            value *= 2;
        }
    }

    [Test]
    public void Should_ApplyModifications_Before_Set()
    {
        var testVariable = new TestObservable();
        testVariable.Value = 42;
        
        Assert.AreEqual(42 * 2, testVariable.Value);
    }
    
    [Test]
    public void Should_ApplyModifications_Before_SetWithoutNotify()
    {
        var testVariable = new TestObservable();
        testVariable.SetWithoutNotify(42);
        
        Assert.AreEqual(42 * 2, testVariable.Value);
    }
}
