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

        public override async void InitUI()
        {
            base.InitUI();
            gcmp = gObj.asCom;
            GRoot.inst.AddChild(gcmp);
            gcmp.GetChild("n15").asButton.onClick.Add(OneBtnClick);
            gcmp.GetChild("AccountLogin").asButton.onClick.Add(OneAccountLoginBtnClick);

            //检查版本/更新操作。。
            gcmp.GetChild("Ver").asTextField.text = "2016xxx";
            //开始动效果
            await ETModel.Game.Scene.GetComponent<TimerComponent>().WaitAsync(1);
            Transition trans = gcmp.GetTransition("t2");
            trans.Play();
        }


        //按钮点击事件
        private void OneBtnClick()
        {

            FairyGUI.Open(typeof(LobbyBgUI));
            FairyGUI.Open(typeof(TopBarUI));
            FairyGUI.Open(typeof(BottomBarUI));
            this.Close();
        }

        private void OneAccountLoginBtnClick()
        {
            Debug.Log("yyyyyy");
        }

        

    }//class_end
}
