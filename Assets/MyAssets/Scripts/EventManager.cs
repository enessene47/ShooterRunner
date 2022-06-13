using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static void ResetEvent()
    {
        startEvent = null;
        failEvent = null;
        successEvent = null;
        nextEvent = null;
        retryEvent = null;
    }


    public delegate void StartHandler();
    public delegate void FailHandler();
    public delegate void SuccessHandler();

    public delegate void NextHandler();
    public delegate void RetryHandler();

    public static event StartHandler startEvent;
    public static event FailHandler failEvent;
    public static event SuccessHandler successEvent;

    public static event NextHandler nextEvent;
    public static event RetryHandler retryEvent;

    #region EventAwakes
    public static void AwakeStartEvent() => startEvent();
    public static void AwakeFailEvent() => failEvent();
    public static void AwakeSuccessEvent() => successEvent();
    public static void AwakeNextEvent() => nextEvent();
    public static void AwakeRetryEvent() => retryEvent();
    #endregion
}
