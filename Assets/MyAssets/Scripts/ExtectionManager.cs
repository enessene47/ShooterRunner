using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public static class ExtectionManager
{
    public static void MyDOMoveZ(this Transform trs, float z, float time = .3f) => trs.DOMoveZ(z, time).SetEase(Ease.Linear);

    public static void MyDORotate(this Transform trs, Vector3 vec, float time = .3f) => trs.DORotate(vec, time).SetEase(Ease.Linear);

    public static void MyDOAnchorPos(this RectTransform trs, Vector3 pos, float time = .8f, Action act = null) =>
        trs.DOAnchorPos(pos, time).SetEase(Ease.Linear).OnComplete(() => act());
}
