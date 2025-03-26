using ParadoxNotion.Design;
using RedSaw.MissionSystem;
using UnityEditor;
using UnityEngine;

[Name("showMask"), Category("Guide"), Description("开启强制引导遮罩")]
public class ShowTagMask : ActionBase
{
    [SerializeField] private string message;

    public override void Execute()
    {
        MissionModule.MissionInnerPipeline.ShowMask(message);
    }
#if UNITY_EDITOR

    public override string Summary
    {
        get
        {
            return $"ShowMask: \"{message}\"";
        }
    }
        
    protected override void OnInspectorGUI()
    {
        message = UnityEditor.EditorGUILayout.TextField("MaskTag", message);
    }
#endif
}