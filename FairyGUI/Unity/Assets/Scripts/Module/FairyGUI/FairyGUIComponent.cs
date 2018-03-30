/**
* 基于FairyGUI的UI组件。
* 
* 0.初始化FairyGUI的全局配置。加载包、ab等。
* 1.统一了UI配置、入口。
* 2.方便管理UI脚本。
* **/

using ETModel;
using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static FairyGUI.UIContentScaler;

namespace ETModel
{
    [ObjectSystem]
    public class FairyGUIComponentAwakeSystem : AwakeSystem<FairyGUIComponent>
    {
        public override void Awake(FairyGUIComponent self)
        {
            self.Awake();
        }
    }
    public class FairyGUIComponent : Component
    {
        public static FairyGUIComponent Instance;

        //FairyBaseUI 用于挂载FairyBaseUI 的容器
        private FairyGUIContainer baseUIContainer;

        //管理Ui的层级关系
        private Dictionary<ETModel.FairyGUIType, List<FairyGUIBaseUI>> _LayerDictionary;

        #region 定义了公共方法
        /// <summary>
        /// 初始化FairyGUI，配置全局参数。
        /// </summary>
        public void Awake()
        {
            Instance = this;
            baseUIContainer = ComponentFactory.Create<FairyGUIContainer>();
            _LayerDictionary = new Dictionary<FairyGUIType, List<FairyGUIBaseUI>>();
            _LayerDictionary.Add(ETModel.FairyGUIType.UITYPE_FIXED, new List<FairyGUIBaseUI>());
            _LayerDictionary.Add(ETModel.FairyGUIType.UITYPE_POPUP, new List<FairyGUIBaseUI>());

            //加载UI包
#if UNITY_EDITOR
          //  UIPackage.AddPackage("UI/ASource");
#else

            //ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            //resourcesComponent.LoadOneBundle($"ui");
            //ABInfo abInfo = resourcesComponent.GetABInfoByName("ui");
            //UIPackage.AddPackage(abInfo.AssetBundle);
#endif
            //UI的配置
            //Groot的参数初始化
            //GRoot.inst.SetContentScaleFactor(1136, 640, ScreenMatchMode.MatchWidthOrHeight);
           // Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("uiaudio.unity3d");

           //UIConfig.buttonSound = (AudioClip)UIPackage.GetItemAssetByURL("ui://ASource/buttonclick");
            //GameObject bunddleObject = (GameObject)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("uiaudio.unity3d", "UIAudio");
            //UIConfig.buttonSound =bunddleObject.Get<AudioClip>("ButtonClickAudio");
            //UIConfig.modalLayerColor = new Color(186f, 85f, 211f, 0.4f);

        }
        /// <summary>
        /// 创建UI Mono层使用
        /// </summary>
        /// <returns></returns>
        public T Create<T>() where T : FairyGUIBaseUI, new()
        {
            if (baseUIContainer.IsDisposed)
            {
                baseUIContainer = ComponentFactory.Create<FairyGUIContainer>();
            }
            T t = baseUIContainer.GetComponent<T>();
            if (t == null)
            {

                t = baseUIContainer.AddComponent<T>();
#if UNITY_EDITOR

                UIPackage.AddPackage("UI/" + t.pakName);
#endif
                t.gObj = UIPackage.CreateObject(t.pakName, t.cmpName);
                //加入到对应层中。
                _LayerDictionary[t.UIType].Add(t);
            }
            t.InitUI();
            return t;
        }
        /// <summary>
        /// 创建UI ILRuntime层使用
        /// </summary>
        /// <returns></returns>
        public GObject Create(string pakName,string cmpName) 
        {
#if UNITY_EDITOR

            UIPackage.AddPackage("UI/" + pakName);
#endif
            return UIPackage.CreateObject(pakName, cmpName);
        }


        /// <summary>
        /// 关闭同层ui.
        /// </summary>
        /// <param name="ui"></param>
        public void DestoryLayer(FairyGUIBaseUI ui)
        {
            List<FairyGUIBaseUI> uIList = _LayerDictionary[ui.UIType];

            if (uIList == null || uIList.Count <= 0)
            {
                return;
            }
            for (int i = uIList.Count - 1; i >= 0; i--)
            {
                FairyGUIBaseUI uiListItem = uIList[i];
                if (uiListItem != null && !uiListItem.IsDisposed)
                {
                    uiListItem.Close();
                }
            }
            uIList.Clear();
        }

        /// <summary>
        /// 清除所有UI
        /// </summary>
        public void DestoryAll()
        {
            foreach (List<FairyGUIBaseUI> uIList in _LayerDictionary.Values)
            {
                if (uIList == null || uIList.Count <= 0)
                {
                    continue;
                }

                for (int i = uIList.Count - 1; i >= 0; i--)
                {
                    FairyGUIBaseUI uiListItem = uIList[i];
                    if (uiListItem != null && !uiListItem.IsDisposed)
                    {
                        uiListItem.Dispose();
                    }
                }
                uIList.Clear();
            }
            baseUIContainer.Dispose();
        }

      
        /// <summary>
        /// 销毁UI Mono使用
        /// </summary>
        public void Destory(FairyGUIBaseUI ui) 
        {
            //从层中移除
            _LayerDictionary[ui.UIType].Remove(ui);
            baseUIContainer.RemoveComponent(ui.GetType());
        }


        /// <summary>
        /// 组件销毁执行的析构函数
        /// </summary>
        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            //清空层级管理容器
            foreach (List<FairyGUIBaseUI> uIList in _LayerDictionary.Values)
            {
                if (uIList == null || uIList.Count <= 0)
                {
                    continue;
                }

                for (int i = uIList.Count - 1; i >= 0; i--)
                {
                    FairyGUIBaseUI uiListItem = uIList[i];
                    if (uiListItem != null && !uiListItem.IsDisposed)
                    {
                        uiListItem.Dispose();
                    }
                }
                uIList.Clear();
            }
            _LayerDictionary.Clear();
            //清空baseUIContainer
            baseUIContainer.Dispose();
            base.Dispose();
        }
        #endregion

    }//class_end
}//
