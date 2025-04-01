using System;
using UnityEngine.UI;

public class PayButton
{
    private Button _button;
    public PayButton(Button btn,Action action)
    {
        _button = btn;
        SetAction(action);
    }
    // 是否可以点击的标志
    private bool _isClickable = true;

    // 暴露一个接口用于检查是否可以点击
    public bool CheckClickable()
    {
        return _isClickable;
    }

    // 暴露一个接口用于更新是否可以点击
    public void UpdateClickable(bool canClick)
    {
        _isClickable = canClick;
        UpdateButtonState();
    }

    // 暴露一个接口用于传入执行逻辑方法
    public void SetAction(System.Action action)
    {
        _button.onClick.RemoveAllListeners(); // 移除之前的监听器
        _button.onClick.AddListener(() =>
        {
            if (_isClickable)
            {
                action?.Invoke();
            }
        });
    }

    // 暴露一个接口用于更新界面
    public void UpdateUI(bool canClick)
    {
        UpdateClickable(canClick);
    }

    // 内部方法：更新按钮的交互状态
    private void UpdateButtonState()
    {
        _button.interactable = _isClickable;
    }

    public void Clear()
    {
        _button.onClick.RemoveAllListeners();
    }
}