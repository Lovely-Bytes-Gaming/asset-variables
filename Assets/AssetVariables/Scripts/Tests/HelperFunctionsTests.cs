using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LovelyBytes.AssetVariables;
using NUnit.Framework;
using UnityEngine;

public class HelperFunctionsTests 
{
    [Test]
    public void Should_StartFromBeginning_When_IndexGreaterCount()
    {
        int index0 = HelperFunctions.RepeatIndex(7, 5);
        Assert.AreEqual(2, index0);

        int index1 = HelperFunctions.RepeatIndex(13, 5);
        Assert.AreEqual(3, index1);
    }

    [Test]
    public void Should_StartFromEnd_When_IndexSmallerZero()
    {
        int index0 = HelperFunctions.RepeatIndex(-2, 5);
        Assert.AreEqual(3, index0);

        int index1 = HelperFunctions.RepeatIndex(-11, 5);
        Assert.AreEqual(4, index1);
    }
}
