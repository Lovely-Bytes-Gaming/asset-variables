using System;
using System.Collections;
using System.Collections.Generic;
using LovelyBytes.AssetVariables;
using UnityEngine;

public class FileWriterTest : MonoBehaviour
{
    [SerializeField] private ScriptableObjectWriter writer;

    private void Start()
    {
        writer.Save();
        writer.Load();
    }
}
