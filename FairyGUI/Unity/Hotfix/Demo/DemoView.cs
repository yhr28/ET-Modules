/**
 * 测试场景UI
 * **/
using ETModel;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ETHotfix
{
    public class LoginSceneUI_FairyGUI :BaseUIForms
    {
        //登录按钮
        public const string BTN_LOGIN = "n15";
        //版本文本
        public const string TEXT_VERTION = "Ver";


        public LoginSceneUI_FairyGUI()
        {
            this.pakName = "Login";
            this.cmpName = "loginBg";
            this.CurrentUIType.NeedClearingStack = true;
            this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.HideOther;
            this.CurrentUIType.UIForms_Type = UIFormsType.Normal;

        }

        public override async void InitUI()
        {
            base.InitUI();
            GComponent gcmp;
            gcmp = this.GObject.asCom;
            gcmp.GetChild(BTN_LOGIN).asButton.onClick.Add(OneBtnClick);
            //检查版本/更新操作。。
            gcmp.GetChild(TEXT_VERTION).asTextField.text = "2016xxx";
          
        }

        public override  void DoShowAnimationEvent()
        {
             
        }




        //按钮点击事件
        private void OneBtnClick()
        {
			//continue
  
        }
 

        

    }//class_end
}
