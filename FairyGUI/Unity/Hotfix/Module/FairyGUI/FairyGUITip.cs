/**
 * FairyGUI的功用弹出框
 * 
 * **/
using FairyGUI;
using System.Collections;
using UnityEngine;

namespace ETHotfix
{
    public class FairyGUITip:FairyGUIBaseUI
    {
        private GTextField textField;

        public FairyGUITip()
        {
            this.pakName = "Common";
            this.cmpName = "Tip";
        }

        public override void InitUI()
        {
            base.InitUI();
            GComponent gCmp = gObj.asCom;
            textField = gCmp.GetChild("textContent").asTextField;
            GRoot.inst.ShowPopup(gCmp);
            gCmp.SetXY(Screen.width / 2, Screen.height / 2);
        }

        /// <summary>
        /// 设置提示内容
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(string content)
        {
            textField.text = content;
        }

       
    }
}
