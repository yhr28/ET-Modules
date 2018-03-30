# **介绍：**
使用FairyGUI作为ET的UI模块，使数据与Ui分离，方便ui制作的同时简化代码逻辑的组织难度；结合ET和FairyGUI完善的内存管理体系，使UI模块运行效率更加稳定快速；只需要暴露两个接口即可完成UI的展示与关闭；
有初步的层级管理，方便管理使用中的Ui以及脚本。
# **使用方法：**
## 1.熟悉并掌握FairyGUI的基本用法。
## 2.熟悉并掌握ET框架。
## 3.将模块内以及需要的依赖文件按照Models的路径结果放入项目内。当然，路径可自定义放置。
## 4.如果使用ILrt的话需要配置Appdomain.cs中的delegate。
## 5.Init中，Game.Scene添加FairyGUIComponent。
## 6.使用FairyGUI制作UI并导入Unity.
## 7.创建自定义的脚本逻辑，继承FairyGUIBaseUI。因为Fairygui中以组件为一个业务单元，所以，尽量将一个FairyGUi中的组件内的相关逻辑写入一个逻辑脚本中。
## 8.在需要的地方，使用FairyGUI类中的静态方法创建并展示UI.在Model层中可以使用优雅的泛型方式创建UI脚本并展示，但是在热更层中要放弃这些。



# Demo.制作一个初始化页面。
## Fairygui编辑器中拼装Ui.包名为“Demo”,组件名为“Login”.
## Unity中创建脚本LoginUI.cs
```
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


    

        public LoginSceneUI_FairyGUI()
        {   
            //所在包名 必填
            this.pakName = "Demo";
            //组件名 必填
            this.cmpName = "Login";
            //ui的类型，根据指定的UI类型放入不同的Ui层中。 非必填。
            UIType = ETModel.FairyGUIType.UITYPE_FIXED;
        }

        public override async void InitUI()
        {
            base.InitUI();
            
            //可以做一些特效动画
            //可以注册一些按钮事件。
            //处理一些数据等。
            
            GRoot.inst.AddChild(gObj);//将Ui放入root层（这个层是FairyGUI的层的概念，由插件管理。）展示。
            
          
        }

 

    }//class_end
```
## 在代码中展示LoginUI
```
 LoginSceneUI_FairyGUI ui = FairyGUI.Open(typeof(LoginSceneUI_FairyGUI));
```
## 关闭LoginUI.

```
ui.Close();
```


## 关闭同层UI.(每个ui都可以关闭同层ui)

```
ui.CloseLayer();
```

## 关闭所有UI.(每个Ui都可以关闭所有内存中的UI.)

```
ui.CloseAll();
```








FairyGUI地址：[http://www.fairygui.com/] 
