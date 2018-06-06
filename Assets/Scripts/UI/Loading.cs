﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventUtils;
using UnityEngine.U2D;
using Newtonsoft.Json;

public class Loading : MonoBehaviour 
{
    enum ConfigTag { CARD = 10, KINGTOWER = 11, ENEMY = 12, WE = 13 }
    enum AtlasTag { CARD = 20 , UI = 21 }

    [HideInInspector]public UEvent_f loadingCompleted = new UEvent_f();

	void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    public void LoadingConfig()
    {
        ResourcesManager.Instance.LoadAsset<TextAsset>((int)ConfigTag.CARD, "Configs/cardconfig", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
        ResourcesManager.Instance.LoadAsset<TextAsset>((int)ConfigTag.KINGTOWER, "Configs/kingTower", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
        ResourcesManager.Instance.LoadAsset<TextAsset>((int)ConfigTag.ENEMY, "Configs/enemyconfig", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
        ResourcesManager.Instance.LoadAsset<TextAsset>((int)ConfigTag.WE, "Configs/weconfig", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
    }

    public void LoadingAtlas()
    {
        ResourcesManager.Instance.LoadAsset<SpriteAtlas>((int)AtlasTag.CARD, "Atlas/CardsAtlas", LoadingAtlasCompleted, (result) => {
            Debug.Log(result);
        });

        ResourcesManager.Instance.LoadAsset<SpriteAtlas>((int)AtlasTag.CARD, "Atlas/BattleUIAtlas", LoadingAtlasCompleted, (result) => {
            Debug.Log(result);
        });
    }

    public void LoadingPrefab()
    {
        foreach (var item in DataManager.Instance.battleAllCardSprites)
        {
            string cardname = item.Key;
            Debug.Log("card name : " + cardname);
            ResourcesManager.Instance.LoadAsset<GameObject>((int)AtlasTag.CARD, "Prefabs/Archer", LoadingPrefabCompleted, (result) => {
                Debug.Log(result);
            });
        }
    }

    public void LoadingAudio()
    {
        
    }

    void LoadingConfigCompleted(int congfigTag, TextAsset textAsset)
    {
        switch((ConfigTag)congfigTag)
        {
            case ConfigTag.CARD:
                DataManager.Instance.SetCardsByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
            case ConfigTag.KINGTOWER:
                DataManager.Instance.SetKingTowersByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
            case ConfigTag.ENEMY:
                DataManager.Instance.SetEnemyCampByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
            case ConfigTag.WE:
                DataManager.Instance.SetWeCampByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
        }
    }

    void LoadingAtlasCompleted(int atlasTag, SpriteAtlas spriteAtlas)
    {
        switch((AtlasTag)atlasTag)
        {
            case AtlasTag.CARD:
                DataManager.Instance.SetCardSpritesByAtlas(spriteAtlas);
                loadingCompleted.Invoke(0.1f);
                break;
            case AtlasTag.UI:
                loadingCompleted.Invoke(0.1f);
                break;
        }
    }

    void LoadingPrefabCompleted(int prefabTag, GameObject prefabObj)
    {
        
    }
}