using ParadoxNotion.Design;
using RedSaw.MissionSystem;
using UnityEditor;
using UnityEngine;

[Name("CloseGuideMask"), Category("Guide"), Description("关闭强引导遮罩")]
public class CloseGuideMask : ActionBase
{

    public override void Execute()
    {
        MissionModule.MissionInnerPipeline.HideStepGuide();
    }
    
}