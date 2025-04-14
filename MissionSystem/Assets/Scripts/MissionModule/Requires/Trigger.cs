using System;
using ParadoxNotion.Design;
using GNode.Editor;
using GNode.MissionSystem;
using UnityEngine;

[Name("触发器"), Description("触发就直接完成")]
public class Trigger : MissionRequireTemplate
{
    [SerializeField] private string eventType;
    public override bool CheckMessage(object message)
    {
        if (message is not NodeMessage gameMessage) return false;
        return gameMessage.type.ToString() == eventType;
    }

    public class Handle : MissionRequireTemplateHandle
    {
        private readonly Trigger require;

        public Handle(Trigger require) : base(require)
        {
            this.require = require;
        }
        protected override bool UseMessage(object message)
        {
            return true;
        }
    }

#if UNITY_EDITOR

    public override string Summary
    {
        get
        {
            return $"[触发器]监听<b><size=12><color=#fffde3> \"{eventType}\" </color></size></b>事件<size=12></size>触发";
        }
    }

    protected override void OnInspectorGUI()
    {
        DropdownMenu.MakeMenu("事件类型", eventType, Enum.GetNames(typeof(GNodeEventType)), result => eventType = result);
    }
#endif
}