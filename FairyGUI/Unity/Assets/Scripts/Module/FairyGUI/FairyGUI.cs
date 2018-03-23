/**
 * 基于FairyGUI的UI组件UI工具类
 * 
 * 
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    public class FairyGUI
    {
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Open<T>() where T:FairyGUIBaseUI,new()
        {
            T t = FairyGUIComponent.Instance.Create<T>();
            return t;
        }


        /// <summary>
        /// 弹出提示框
        /// </summary>
        /// <param name="content"></param>
        public static void Tip(string content)
        {
            FairyGUITip tip = Open<FairyGUITip>();
            tip.SetContent(content);
        }

    }//class_end
}
