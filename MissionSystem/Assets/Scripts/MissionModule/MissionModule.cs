using System.Collections.Generic;
using Newtonsoft.Json;
using RedSaw.MissionSystem;
using ThGold.Common;
using ThGold.Event;

public class MissionModule : SingletonNoNomo<MissionModule>
{
    public static MissionManager<object> MissionManager;
    public static MissionChainManager MissionChainManager;
    private List<MissionChain> _missionChains = new List<MissionChain>();
    private Dictionary<string, MissionChain> _missionChainDic = new Dictionary<string, MissionChain>();
    private List<string> _completedMissions = new List<string>();
    public static MissionInnerPipeline MissionInnerPipeline;

    public void Init()
    {
        _completedMissions = GetData();
        if (_completedMissions == null)
        {
            _completedMissions = new List<string>();
        }

        MissionInnerPipeline = new MissionInnerPipeline(this);
        //configs
        if (!_completedMissions.Contains("Guide01"))
        {
            string key = "Guide01";
            MissionChain chain = Utils.LoadSync<MissionChain>(key);
            _missionChains.Add(chain);
            _missionChainDic.Add(key, chain);
        }

        if (!_completedMissions.Contains("Guide02"))
        {
            string key = "Guide02";
            MissionChain chain = Utils.LoadSync<MissionChain>(key);
            _missionChains.Add(chain);
            _missionChainDic.Add(key, chain);
        }

        if (!_completedMissions.Contains("Guide03"))
        {
            string key = "Guide03";
            MissionChain chain = Utils.LoadSync<MissionChain>(key);
            _missionChains.Add(chain);
            _missionChainDic.Add(key, chain);
        }

        MissionManager = new MissionManager<object>();
        MissionChainManager = new MissionChainManager(MissionManager);
        MissionManager.AddComponent(MissionChainManager);
        EventHandler.Instance.EventDispatcher.AddEventListener(CustomEventName.MissionMessage, MissionMessage);
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
        EventHandler.Instance.EventDispatcher.RemoveEventListener(CustomEventName.MissionMessage, MissionMessage);
        base.OnDestroy();
    }

    public void StartMission(string missionKey)
    {
        MissionChainManager.StartChain(_missionChainDic[missionKey]);
    }

    private void MissionMessage(IEvent ievent)
    {
        MissionManager.SendMessage(ievent.data);
    }

    public void SendMessage(MissionMessage msg)
    {
        MissionManager.SendMessage(msg);
    }

    public void CompletedMission(string key)
    {
        _completedMissions.Add(key);
        SaveData();
    }

    public void SaveData()
    {
        string json = JsonConvert.SerializeObject(_completedMissions, Formatting.Indented);
        PlayerPrefsUtility.SetEncryptedString(PlayerPrefsDefine.GamePlayDoneGuide, json);
    }

    public List<string> GetData()
    {
        string json = PlayerPrefsUtility.GetEncryptedString(PlayerPrefsDefine.GamePlayDoneGuide, "");
        return deserializeGuideList(json);
    }

    private List<string> deserializeGuideList(string json)
    {
        var maps = JsonConvert.DeserializeObject<List<string>>(json);
        return maps;
    }
}