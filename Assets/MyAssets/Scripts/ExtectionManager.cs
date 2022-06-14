using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class ExtectionManager
{
    public static void MyDOMoveZ(this Transform trs, float z, float time = .3f) => trs.DOMoveZ(z, time).SetEase(Ease.Linear); 
}
