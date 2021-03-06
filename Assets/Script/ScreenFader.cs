﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFader : MonoBehaviour
{
    public RawImage rawImage;
    private float floatColorChangeSpeed = 1.0f;
    
    public bool isBlack = false;
    public bool isAnimanating = false;

    private void FadeToClear()
    {
        //插值运算
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, floatColorChangeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 屏幕逐渐暗淡(淡出)
    /// </summary>
    private void FadeToBlack()
    {
        //插值运算
        rawImage.color = Color.Lerp(rawImage.color, Color.black, floatColorChangeSpeed * Time.deltaTime);
    }

    private Tween t;
    /// <summary>
    /// 屏幕的淡入
    /// </summary>
    public void ScreenToClear(Util.NoParmsCallBack successCallback = null)
    {
        if (isAnimanating)
        {
            Debug.Log("b1");
            return;
        }
        isAnimanating = true;
        if (t != null)
        {
            Debug.Log("b2");
            t.Pause();
            t = null;
        }
        Debug.Log("b3");
        t = DOTween.To(() => rawImage.color, x => rawImage.color = x, Color.clear, floatColorChangeSpeed);
        t.OnComplete(() =>
        {
            Debug.Log("b4");
            //设置为完全透明
            rawImage.color = Color.clear;
            //组件的开关设置为关闭的状态
            rawImage.enabled = false;
            isBlack = false;
            isAnimanating = false;
            successCallback?.Invoke();
        });
    }
    
    /// <summary>
    /// 屏幕的淡出
    /// </summary>
    public void ScreenToBlack(Util.NoParmsCallBack successCallback = null)
    {
        if (isAnimanating)
        {
            Debug.Log("a1");
            return;
        }
        isAnimanating = true;
        if (t != null)
        {
            Debug.Log("a2");
            t.Pause();
            t = null;
        }
        Debug.Log("a3");
        rawImage.enabled = true;
        t = DOTween.To(() => rawImage.color, x => rawImage.color = x, Color.black, floatColorChangeSpeed);
        t.OnComplete(() =>
        {
            Debug.Log("a4");
            rawImage.color = Color.black;
            isBlack = true;
            isAnimanating = false;
            successCallback?.Invoke();
        });
    }
}
