public enum MissionEventType
{
    MoneyArrived,
    StepClick,
    LevelArrived,
    ShowPopup,
}

public class MissionMessage
{
    public readonly MissionEventType type;
    public readonly object args;
    public bool hasUsed { get; private set; }

    public MissionMessage(MissionEventType type, object args = null)
    {
        this.type = type;
        this.args = args;
        this.hasUsed = false;
    }

    /// <summary>使用当前消息</summary>
    public void Use() => 
        hasUsed = true;
}