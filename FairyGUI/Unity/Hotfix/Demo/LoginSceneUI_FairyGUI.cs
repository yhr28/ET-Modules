/**
 * 登录场景UI
 * **/
using ETModel;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    public class LoginSceneUI_FairyGUI :FairyGUIBaseUI
    {


        GComponent gcmp;

        public LoginSceneUI_FairyGUI()
        {
            this.pakName = "Login";
            this.cmpName = "loginBg";
            UIType = ETModel.FairyGUIType.UITYPE_FIXED;
        }

        public override void InitUI()
        {
            base.InitUI();
            gcmp = gObj.asCom;
            GRoot.inst.AddChild(gcmp);
           
        }


       

    }//class_end
}
