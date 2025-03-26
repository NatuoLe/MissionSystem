using System.Collections.Generic;

public class MissionInnerPipeline
{
    private MissionModule _module;
    private MainUI _mainui;

    public MissionInnerPipeline(MissionModule missionModule)
    {
        _module = missionModule;
    }

    public void CheckMoney(double _money)
    {
        if (_money >= 1000)
        {
            _module.SendMessage(new MissionMessage(MissionEventType.MoneyArrived));
        }
    }

    public void CheckLevel(long level)
    {
        if (level >= 5)
        {
            _module.SendMessage(new MissionMessage(MissionEventType.LevelArrived,(int)level));
        }
        
    }

    public void CheckShowUI(string name)
    {
        _module.SendMessage(new MissionMessage(MissionEventType.ShowPopup, name));
    }

    public void HideStepGuide()
    {
        GameManager.Instance.mainUI.HideGuideMask();
    }

    public void ShowMask(string tag)
    {
        if (tag == "Mask_BottomBallbtn")
        {
            GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.MainBottomUI.GetBottomBallBtn());
        }

        if (tag == "Mask_BallBtn")
        {
            GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.MainBottomUI.GetBottomBall()
                .guideBallArea);
        }

        if (tag == "Mask_BlessBtn")
        {
            GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.GetBlessBtn());
        }

        if (tag == "BlessMask1")
        {
            if (GameManager.Instance.mainUI.BlessPopup != null)
            {
                GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.BlessPopup.area1);
            }
        }

        if (tag == "BlessMask2")
        {
            if (GameManager.Instance.mainUI.BlessPopup != null)
            {
                GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.BlessPopup.area2);
            }
        }

        if (tag == "Mask_BadgeBtn")
        {
            GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.MainBottomUI.GetBottomBadgeBtn());
        }
        if (tag == "Mask_PrestigeBtn")
        {
            GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.MainBottomUI.GetBottomBadge().prestigeArea);
        }
        if (tag == "Mask_PrestigePopup")
        {
            if (GameManager.Instance.mainUI.PrestigePopup != null)
            {
                GameManager.Instance.mainUI.ShowGuideMask(GameManager.Instance.mainUI.PrestigePopup.Area);
            }
        }
    }

    public void StepClick(string tag)
    {
        _module.SendMessage(new MissionMessage(MissionEventType.StepClick, tag));
    }

    public void CompleteMission(string key)
    {
        _module.CompletedMission(key);
    }
}