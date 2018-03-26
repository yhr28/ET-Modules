/**
* 基于FairyGUI的UI组件。
* 0.初始化FairyGUI的全局配置。加载包、ab等。
* 1.统一了UI配置、入口。
* 2.提供UI的层级管理。
* 3 为了使用Ilruntime，放弃优雅的泛型，使用非泛型方法创建ui逻辑脚本。
* 
* **/

using System;
using System.Collections.Generic;
using ETModel;

using static FairyGUI.UIContentScaler;

namespace ETHotfix
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
        //Mono层的组件。
        public static ETModel.FairyGUIComponent MonoInstance;
      
        //FairyBaseUI 用于挂载FairyBaseUI 的容器(当前展示的UI)
        private FairyGUIContainer baseUIContainer;

        //管理Ui的层级关系
        private Dictionary<ETModel.FairyGUIType, List<FairyGUIBaseUI>> _LayerDictionary;




        #region 定义了公共方法
        /// <summary>
        /// 初始化FairyGUI
        /// </summary>
        public void Awake()
        {
          
            MonoInstance = ETModel.Game.Scene.GetComponent<ETModel.FairyGUIComponent>();
            baseUIContainer = ComponentFactory.Create<FairyGUIContainer>();
            _LayerDictionary = new Dictionary<FairyGUIType, List<FairyGUIBaseUI>>();
            _LayerDictionary.Add(ETModel.FairyGUIType.UITYPE_FIXED, new List<FairyGUIBaseUI>());
            _LayerDictionary.Add(ETModel.FairyGUIType.UITYPE_POPUP, new List<FairyGUIBaseUI>());

        }

        /// <summary>
        /// 非泛型方法创建UI
        /// </summary>
        /// <param name="uIType"></param>
        /// <returns></returns>
        internal FairyGUIBaseUI Create(Type uIType)
        {
            if (baseUIContainer.IsDisposed)
            {
                baseUIContainer = ComponentFactory.Create<FairyGUIContainer>();
            }

            FairyGUIBaseUI t = baseUIContainer.GetComponent(uIType) as FairyGUIBaseUI;
            if (t == null)
            {
                t = baseUIContainer.AddComponent(uIType) as FairyGUIBaseUI;
                t.gObj = MonoInstance.Create(t.pakName, t.cmpName);
                //加入到对应层中。
                _LayerDictionary[t.UIType].Add(t);
                ShowLayer();
            }
            t.InitUI();
            return t;
        }

        public void ShowLayer()
        {
            Log.Debug("固定窗体:"+_LayerDictionary[FairyGUIType.UITYPE_FIXED].Count.ToString());
            Log.Debug("弹出窗体:"+_LayerDictionary[FairyGUIType.UITYPE_POPUP].Count.ToString());
            Log.Debug("全部窗体:" + baseUIContainer.GetComponents().Length.ToString());
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
                    if (uiListItem != null&& !uiListItem.IsDisposed)
                    {
                        uiListItem.Dispose();
                    }
                }
                uIList.Clear();
            }
            baseUIContainer.Dispose();
            ShowLayer();
        }


        /// <summary>
        /// 销毁UI,调用UI组件时，会调用dispose同时销毁gui组件
        /// </summary>
        /// <param name="ui"></param>
        public void Destory(FairyGUIBaseUI ui) 
        {
            //从层中移除
            _LayerDictionary[ui.UIType].Remove(ui);
            //从脚本容器中移除（会调用dispose）
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
            foreach(List<FairyGUIBaseUI> uIList in _LayerDictionary.Values)
            {
                if(uIList == null || uIList.Count<=0)
                {
                    continue;
                }
                 
                for (int i = uIList.Count-1; i >=0 ; i--)
                {
                    FairyGUIBaseUI uiListItem = uIList[i];
                    if (uiListItem != null&& !uiListItem.IsDisposed)
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
