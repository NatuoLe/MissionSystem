using GNode.MissionSystem;
using UnityEngine;
using UnityEngine.UI;

public class MissionUIController : MonoBehaviour
{
    [SerializeField] private GameObject missionItemPrefab;
    
}

public class MissionShowItemView
{
    private Text missionName;

    private Text missionProgress;
    private Mission<object> mission;

    public MissionShowItemView(GameObject gameObject)
    {
        missionName = gameObject.transform.Find("missionName").GetComponent<Text>();
        missionProgress = gameObject.transform.Find("missionProgress").GetComponent<Text>();
    }

    public void Refresh(Mission<object> mission)
    {
        this.mission = mission;
        missionName.text = this.mission.id.ToString();
        missionProgress.text = "(0/1)";
    }
}