public enum GNodeEventType
{
    MoneyArrived,
    StepClick,
    LevelArrived,
    ShowPopup,
    StartStep,
}

public class NodeMessage
{
    public readonly GNodeEventType type;
    public readonly object args;
    public bool hasUsed { get; private set; }

    public NodeMessage(GNodeEventType type, object args = null)
    {
        this.type = type;
        this.args = args;
        this.hasUsed = false;
    }

    /// <summary>使用当前消息</summary>
    public void Use() => 
        hasUsed = true;
}