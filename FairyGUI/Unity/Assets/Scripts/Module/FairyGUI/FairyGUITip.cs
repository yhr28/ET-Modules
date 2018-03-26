/**
 * FairyGUI的功用弹出框
 * 
 * **/
using FairyGUI;
using System.Collections;
using UnityEngine;

namespace ETModel
{
    public class FairyGUITip:FairyGUIBaseUI
    {
        private GTextField textField;

        private void Awake()
        {
            this.pakName = "Common";
            this.cmpName = "Tip";
        }


        public override void InitUI()
        {
            base.InitUI();
            GComponent gCmp = gObj.asCom;
            textField = gCmp.GetChild("textContent").asTextField;
            GRoot.inst.ShowPopup(gObj.asCom);
            gCmp.SetXY(Screen.width / 2, Screen.height / 2);
        }


        public void SetContent(string content)
        {
            textField.text = content;
        }

       
    }
}
