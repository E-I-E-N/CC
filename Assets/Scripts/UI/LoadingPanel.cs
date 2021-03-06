﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Loading))]
public class LoadingPanel : MonoBehaviour {
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private Text _percentLabel;

    enum LoadingStep { LOADING_START, LOADING_CONFIG, LOADING_ATLAS, LOADING_PREFAB, LOADING_AUDIO, LOADING_END }
    LoadingStep curStep;
    public bool isStartLoading { get; set; }
    private Loading loading;

    private void Awake()
    {
        loading = GetComponent<Loading>();
        if (loading)
            loading.loadingCompleted.AddListener(LoadingCompleted);
        _loadingSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    void Start () 
	{
        curStep = LoadingStep.LOADING_START;
	}

	void Update () 
	{
        if (isStartLoading)
        {
            isStartLoading = false;
            StartCoroutine(Loading());
        }
	}

    IEnumerator Loading()
    {
        LoadingByStep(curStep);
        yield return null;
        isStartLoading = true;
    }

    void LoadingByStep(LoadingStep step)
    {
        switch (step)
        {
            case LoadingStep.LOADING_START:
                {
                    curStep = LoadingStep.LOADING_CONFIG;
                    _loadingSlider.value += 0.1f;
                }
                break;
            case LoadingStep.LOADING_CONFIG:
                {
                    curStep = LoadingStep.LOADING_ATLAS;
                    loading.LoadingConfig();
                }
                break;
            case LoadingStep.LOADING_ATLAS:
                {
                    curStep = LoadingStep.LOADING_PREFAB;
                    loading.LoadingAtlas();
                }
                break;
            case LoadingStep.LOADING_PREFAB:
                {
                    curStep = LoadingStep.LOADING_AUDIO;
                    loading.LoadingPrefab();
                }
                break;
            case LoadingStep.LOADING_AUDIO:
                {
                    curStep = LoadingStep.LOADING_END;
                    _loadingSlider.value += 0.2f;
                    loading.LoadingAudio();
                }
                break;
            case LoadingStep.LOADING_END:
                {
                    _loadingSlider.value += 0.1f;
                    isStartLoading = false;
                    GameManager.Instance.ActiveOrInActiveUiCardButtions(true);
                    //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
                    Destroy(gameObject);
                }
                break;
        }      
    }

    void LoadingCompleted(float progress)
    {
        Debug.Log("加载完成了:" + progress.ToString());
        _loadingSlider.value += progress;
    }

    void OnSliderChanged(float result)
    {
        float percent = _loadingSlider.value * 100;
        _percentLabel.text = string.Format("{0}%", percent);
    }
}
