using GNode.MissionSystem;

public class StartRequire: MissionRequireTemplate
{
    public override bool CheckMessage(object message)
    {
        return true;
    }
    public class Handle : MissionRequireTemplateHandle
    {
        private readonly StartRequire require;
        private int count;

        public Handle(StartRequire require) : base(require)
        {
            this.require = require;
        }
        protected override bool UseMessage(object message)
        {
            return true;
        }
    }
}