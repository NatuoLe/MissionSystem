using System.Collections.Generic;
using GNode.MissionSystem;
using ThGold.Common;
using ThGold.Event;

public class MissionModule : SingletonNoNomo<MissionModule>
{
    public static MissionManager<object> MissionManager;
    public static MissionChainManager MissionChainManager;
    private List<MissionChain> _missionChains = new List<MissionChain>();
    private Dictionary<string, MissionChain> _missionChainDic = new Dictionary<string, MissionChain>();
    private List<string> _completedMissions = new List<string>();

    public void Init()
    {

        if (_completedMissions == null)
        {
            _completedMissions = new List<string>();
        }


      
        MissionManager = new MissionManager<object>();
        MissionChainManager = new MissionChainManager(MissionManager);
        MissionManager.AddComponent(MissionChainManager);

    }

    public void StartMissions()
    {
        if (_missionChains.Count > 0)
        {
            MissionChainManager.StartChain(_missionChains[0]);
        }
    }

    protected override void OnDestroy()
    {

        base.OnDestroy();
    }

    public void StartMission(string missionKey)
    {
        MissionChainManager.StartChain(_missionChainDic[missionKey]);
    }
    public void StartMission(MissionChain chain)
    {
        MissionChainManager.StartChain(chain);
    }
    private void MissionMessage(IEvent ievent)
    {
        MissionManager.SendMessage(ievent.data);
    }

    public void SendMessage(NodeMessage msg)
    {
        MissionManager.SendMessage(msg);
    }

    public void CompletedMission(string key)
    {
        _completedMissions.Add(key);
   
    }
    
}