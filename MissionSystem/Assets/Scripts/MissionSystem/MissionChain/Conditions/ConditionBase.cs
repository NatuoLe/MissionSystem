using System;
using ParadoxNotion.Design;
using UnityEditor;


namespace GNode.MissionSystem
{
    /// <summary>base class for all mission graph conditions</summary>
    public abstract class ConditionBase : MissionChainObject
    {
        public abstract bool IsConditionMet { get; }
    }
}