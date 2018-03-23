using ETModel;
using System;
using System.Collections.Generic;
/**
 * 基于FairyGUI的UI组件UI工具类
 * 
 * 
 * **/


namespace ETHotfix
{
    public class FairyGUI
    {
        /// <summary>
        /// 非泛型方法开启UI.
        /// </summary>
        /// <param name="UIType">UI脚本的类型</param>
        /// <returns></returns>
        public static FairyGUIBaseUI Open(Type uIType)  
        {
            return Game.Scene.GetComponent<FairyGUIComponent>().Create(uIType);
        }

        /// <summary>
        /// 打开UI,同时记录上一层UI
        /// </summary>
        /// <param name="uIType">UI脚本的类型</param>
        /// <param name="preLayerUI">上层UI脚本的引用</param>
        /// <returns></returns>
        public static FairyGUIBaseUI Open(Type uIType,FairyGUIBaseUI preLayerUI)
        {
            FairyGUIBaseUI t =  Game.Scene.GetComponent<FairyGUIComponent>().Create(uIType);
       //     t.preLayerUI = preLayerUI;
            return t;
        }



        /// <summary>
        /// 弹出提示框
        /// </summary>
        /// <param name="content"></param>
        public static void Tip(string content)
        {
            FairyGUITip tip = Open(typeof(FairyGUITip)) as FairyGUITip;
            tip.SetContent(content);
        }

    }//class_end
}
