﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesManager : Singleton<ResourcesManager>
{
    private ResourcesManager() {}

    public void LoadAsset<T>(int tag, string assetPath, Action<int, T> succeeded, Action<string> failed) where T : UnityEngine.Object
    {
        ResourceRequest resourceRequest = Resources.LoadAsync<T>(assetPath);
        var cor = RequestCoroutine(tag, assetPath, resourceRequest, succeeded, failed);
        //GameManager.mainThreadDispatcher.Commit(cor);
    }

    private IEnumerator RequestCoroutine<T>(int tag, string assetPath, ResourceRequest request, Action<int, T> succeeded, Action<string> failed) where T : UnityEngine.Object
    {
        while (!request.isDone)
            yield return null;

        if (request.asset == null)
        {
            failed("Failed to find asset in :" + assetPath);
            yield break;
        }

        var casted = request.asset as T;
        if (casted == null)
        {
            failed("Failed to cast asset to required type:" + typeof(T));
            yield break;
        }

        succeeded(tag, casted);
    }
}