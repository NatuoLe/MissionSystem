using System;
using System.Collections.Generic;
using UnityEngine;

public class MutexUINode<T> where T : IMutex
{
    private Dictionary<int, T> _panelDictionary;
    private List<T> _panelList;
    private int currentState;
    private bool sameMutex;

    public Action<int> OnPanelShow;
    public Action<int> OnPanelClose;

    /// <summary>
    /// On same Panel Action
    /// </summary>
    public Action<int, T> OnSameSexAction;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sameMutex">如果currentState == index ,是否执行</param>
    public MutexUINode(bool sameMutex = false)
    {
        currentState = -1;
        _panelDictionary = new Dictionary<int, T>();
        _panelList = new List<T>();
        this.sameMutex = sameMutex;
    }

    public void Show(int index)
    {
        if (currentState == index && sameMutex) return;


        if (currentState == index)
        {
            if (OnSameSexAction != null)
            {
                OnSameSexAction.Invoke(index,GetObjectById(index));
            }
            else
            {
                Debug.LogError("If you selected sameMutex must need Register OnSameSexAction!");
            }
           
            return;
        }

        // 隐藏当前面板
        _panelDictionary.TryGetValue(currentState, out T hideObj);
        if (hideObj != null)
        {
            hideObj.MutexClose();
            OnPanelClose?.Invoke(currentState); // close action
        }

        if (currentState == index)
        {
            return;
        }

        // 显示目标面板
        _panelDictionary.TryGetValue(index, out T showObj);
        if (showObj != null)
        {
            showObj.MutexShow();
            OnPanelShow?.Invoke(index); // show action
        }


        currentState = index;
    }

    private T GetObjectById(int index)
    {
        _panelDictionary.TryGetValue(index, out T obj);
        return obj;
    }

    public void RegisterPanel(int index, T panel)
    {
        if (!_panelDictionary.ContainsKey(index))
        {
            if (panel != null)
            {
                _panelDictionary.Add(index, panel);
                _panelList.Add(panel);
            }
            else
            {
                Debug.LogError("Panel is NULL");
            }
        }
    }

    public void CancelPanel(int index)
    {
        if (!_panelDictionary.ContainsKey(index))
        {
            _panelDictionary.Remove(index);
            _panelList.RemoveAt(index);
        }
    }

    //when Instance On destroy
    public void OnDestroy()
    {
        OnPanelShow = null;
        OnPanelClose = null;
        _panelDictionary = null;
        OnSameSexAction = null;
    }
}