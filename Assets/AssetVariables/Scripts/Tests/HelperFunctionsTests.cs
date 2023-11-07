using System;
using System.Reflection;
using LovelyBytes.AssetVariables;
using NUnit.Framework;

public class HelperFunctionsTests 
{
    // Helper class to test compare functions
    private class TestComparable : IComparable<TestComparable>
    {
        private readonly int _id = 0;
        public TestComparable(int id)
        {
            _id = id;
        }
        public int CompareTo(TestComparable other)
        {
            if (ReferenceEquals(this, other))
                return 0;

            return ReferenceEquals(other, null) 
                ? 1 
                : _id.CompareTo(other._id);
        }
    }
    
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
    
    [Test]
    public void Should_ConsiderNullEqualNull()
    {
        bool areEqual = HelperFunctions.AreEqual<TestComparable>(default, default);
        Assert.IsTrue(areEqual);
    }

    [Test]
    public void Should_ConsiderEqualValuesAsEqual()
    {
        TestComparable comp0 = new(0);
        TestComparable comp1 = new(0);

        bool areObjectsEqual = HelperFunctions.AreEqual(comp0, comp1);
        Assert.IsTrue(areObjectsEqual);

        bool areValuesEqual = HelperFunctions.AreEqual(2, 2);
        Assert.IsTrue(areValuesEqual);
    }
    
    [Test]
    public void Should_ConsiderSelfEqualToSelf()
    {
        TestComparable comp = new(0);

        bool areEqual = HelperFunctions.AreEqual(comp, comp);
        Assert.IsTrue(areEqual);
    }
    
    [Test]
    public void ShouldNot_ConsiderNullEqualToObject()
    {
        TestComparable comp0 = new(0);
        TestComparable comp1 = default;

        bool areEqual = HelperFunctions.AreEqual(comp0, comp1);

        Assert.IsFalse(areEqual);
    }
    
    [Test]
    public void ShouldNot_ConsiderDistinctValuesAsEqual()
    {
        TestComparable comp0 = new(0);
        TestComparable comp1 = new(1);

        bool areObjectsEqual = HelperFunctions.AreEqual(comp0, comp1);
        Assert.IsFalse(areObjectsEqual);
        
        bool areValuesEqual = HelperFunctions.AreEqual(2, 3);
        Assert.IsFalse(areValuesEqual);
    }

    [Test]
    public void Should_ConsiderNullAsNull()
    {
        MethodInfo methodInfo = GetGenericMethod("IsNull",typeof(TestComparable));
        bool isNull = (bool)methodInfo.Invoke(null, new object[]{null});
        
        Assert.IsTrue(isNull);
    }
    
    [Test]
    public void ShouldNot_ConsiderValueTypeAsNull()
    {
        MethodInfo methodInfo = GetGenericMethod("IsNull",typeof(int));
        bool isNull = (bool)methodInfo.Invoke(null, new object[]{1});
        
        Assert.IsFalse(isNull);
    }
    
    [Test]
    public void ShouldNot_ConsiderObjectAsNull()
    {
        MethodInfo methodInfo = GetGenericMethod("IsNull",typeof(TestComparable));
        bool isNull = (bool)methodInfo.Invoke(null, new object[]{ new TestComparable(1) });
        
        Assert.IsFalse(isNull);
    }

    private static MethodInfo GetGenericMethod(string name, Type genericParameter)
    {
        MethodInfo methodInfo = typeof(HelperFunctions).GetMethod(name,
            BindingFlags.NonPublic | BindingFlags.Static);
        
        MethodInfo genericMethod = methodInfo?
            .MakeGenericMethod(genericParameter);

        return genericMethod;
    }
}
