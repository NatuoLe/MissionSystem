using System;
using ParadoxNotion.Design;
using GNode.Editor;
using GNode.MissionSystem;
using UnityEngine;

[Name("检测"), Description("任务模板")]
public class Check : MissionRequireTemplate
{
    [SerializeField] protected string eventType;
    [SerializeField] protected int count;

    public override bool CheckMessage(object message)
    {
        if (message is not NodeMessage gameMessage) return false;

        return gameMessage.type.ToString() == eventType;
    }

    public class Handle : MissionRequireTemplateHandle
    {
        private readonly Check require;
        private int count;

        public Handle(Check require) : base(require)
        {
            this.require = require;
        }

        protected override bool UseMessage(object message)
        {
            NodeMessage _message = (NodeMessage)message;
            return (int)_message.args >= require.count;
        }
    }

#if UNITY_EDITOR

    public override string Summary
    {
        get
        {
            return
                $"监听<b><size=12><color=#fffde3> \"{eventType}\" </color></size></b>事件<size=12><b><color=#b1d480> {count} </color></b></size>次";
        }
    }

    protected override void OnInspectorGUI()
    {
        DropdownMenu.MakeMenu("事件类型", eventType, Enum.GetNames(typeof(GNodeEventType)), result => eventType = result);
        count = UnityEditor.EditorGUILayout.IntField("Value", count);
        count = Mathf.Max(1, count);
    }
#endif
}