using ParadoxNotion.Design;

namespace GNode.MissionSystem
{
    [ParadoxNotion.Design.Icon("start"), Color("E66E71"), Name("Start")]
    [Description("Start a mission No Require")]
    public class NodeStarter : GNodeComposite
    {
        public override bool allowAsPrime => true;
        public string MissionId => $"{graph.name}.NodeStarter.{base.UID}";
    }
}