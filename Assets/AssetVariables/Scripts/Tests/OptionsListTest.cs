using LovelyBytes.AssetVariables;
using NUnit.Framework;
using UnityEngine;
using WrapMode = LovelyBytes.AssetVariables.WrapMode;

public class OptionsListTest 
{
    private class IntOptionsList : OptionsList<int> {}
    
    [Test]
    public void Should_ReturnDefaultValue_When_Empty()
    {
        IntOptionsList optionsList = ScriptableObject.CreateInstance<IntOptionsList>();
        const int expected = default;

        Assert.AreEqual(expected, optionsList.Current);
    }

    [Test]
    public void Should_AutoSelect_When_FirstElementAdded()
    {
        IntOptionsList optionsList = ScriptableObject.CreateInstance<IntOptionsList>();
        const int expected = 4;

        optionsList.OnSelectionChanged += (oldValue, newValue)
            => Assert.AreEqual(expected, newValue);
        
        optionsList.Add(expected);
    }
    
    [Test]
    public void Should_ChangeCurrentIndex_When_SettingDifferentIndex()
    {
        IntOptionsList optionsList = GetRange(5);
        
        optionsList.Index = 2;
        Assert.AreEqual(2, optionsList.Index);
    }
    
    [Test]
    public void Should_ClampIndex_When_OutOfBounds_And_WrapModeSetToClamp()
    {
        IntOptionsList optionsList = GetRange(5);
        optionsList.WrapMode = WrapMode.Clamp;
        

        optionsList.Index = 7;
        Assert.AreEqual(4, optionsList.Index);

        optionsList.Index = -2;
        Assert.AreEqual(0, optionsList.Index);
    }
    
    [Test]
    public void Should_RepeatIndex_When_OutOfBounds_And_WrapModeSetToClamp()
    {
        IntOptionsList optionsList = GetRange(5);
        optionsList.WrapMode = WrapMode.Repeat;

        optionsList.Index = 7;
        Assert.AreEqual(HelperFunctions.RepeatIndex(7, optionsList.Count), optionsList.Index);
        
        optionsList.Index = -2;
        Assert.AreEqual(HelperFunctions.RepeatIndex(-2, optionsList.Count), optionsList.Index);
    }

    [Test]
    public void Should_KeepSelection_When_AddElement()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;
        
        optionsList.Add(5);
        Assert.AreEqual(2, optionsList.Current);
    }
    
    [Test]
    public void Should_KeepSelection_When_InsertAfter()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;
        
        optionsList.Insert(3, 666);
        Assert.AreEqual(2, optionsList.Current);
    }
    
    [Test]
    public void Should_KeepSelection_When_InsertAtIndex()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;
        
        optionsList.Insert(2, 666);
        Assert.AreEqual(2, optionsList.Current);
    }
    
    [Test]
    public void Should_KeepSelection_When_InsertBefore()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;
        
        optionsList.Insert(1, 666);
        Assert.AreEqual(2, optionsList.Current);
    }
    
    [Test]
    public void Should_KeepSelection_When_RemoveAfter()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;

        optionsList.RemoveAt(3);
        Assert.AreEqual(2, optionsList.Current);
    }
    
    [Test]
    public void Should_KeepSelection_When_RemoveBefore()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;

        optionsList.RemoveAt(1);
        Assert.AreEqual(2, optionsList.Current);
    }
    
    [Test]
    public void Should_SelectPredecessor_When_RemoveSelection()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;

        optionsList.RemoveAt(2);
        Assert.AreEqual(1, optionsList.Current);
    }

    [Test]
    public void Should_KeepSelection_When_SetOtherElement()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;

        optionsList[3] = 666;
        Assert.AreEqual(2, optionsList.Current);
        
        optionsList[1] = 666;
        Assert.AreEqual(2, optionsList.Current);
    }
    
    [Test]
    public void Should_SwapSelection_When_SetSelection()
    {
        IntOptionsList optionsList = GetRange(5);

        optionsList.Index = 2;

        optionsList[2] = 666;
        Assert.AreEqual(666, optionsList.Current);
    }
    
    private static IntOptionsList GetRange(int count)
    {
        IntOptionsList optionsList = ScriptableObject.CreateInstance<IntOptionsList>();
        
        for (int i = 0; i < count; ++i)
            optionsList.Add(i);

        return optionsList;
    }
}
