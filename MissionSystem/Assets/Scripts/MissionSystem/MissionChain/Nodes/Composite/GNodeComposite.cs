using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;


namespace GNode.MissionSystem
{

    ///<summary> Base class for BehaviourTree Composite nodes.</summary>
    abstract public class GNodeComposite : NodeBase
    {

        public override string name { get { return base.name.ToUpper(); } }
        public override bool allowAsPrime => true;
        sealed public override int maxOutConnections { get { return -1; } }
        sealed public override Alignment2x2 commentsAlignment { get { return Alignment2x2.Right; } }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR


#endif

    }
}