using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GuideGame : MonoBehaviour
{
    public int Level;

    [Button("addlevel")]
    public void AddLevel()
    {
        Level++;
    }
}