# **介绍：**
## 使用FairyGUI作为ET的UI模块，使数据与Ui分离，方便ui制作的同时简化代码逻辑的组织难度；结合ET和FairyGUI完善的内存管理体系，使UI模块运行效率更加稳定快速；
## 完善的层级管理，方便管理使用中的Ui以及脚本。
## 本模块在原有支持fairygui的基础上，新增了SUIFW框架对ui的整体管理。suifw是刘国柱老师编写的ui框架，我这里抄袭了95%。框架很好用[https://www.cnblogs.com/LiuGuozhu/p/6943284.html](http://note.youdao.com/)。
# **使用方法：**
## 1.熟悉并掌握FairyGUI的基本用法。
## 2.熟悉并掌握ET框架。
## 3.熟悉SUIFW框架原理。
## 4.将模块内以及需要的依赖文件按照Models的路径结果放入项目内。当然，路径可自定义放置。
## 5.如果使用ILrt的话需要配置Appdomain.cs中的delegate。
## 6.Init中，Game.Scene添加FairyGUIComponent。
## 7.使用FairyGUI制作UI并导入Unity.
## 8.创建自定义的脚本逻辑，继承FairyGUIBaseUI。因为Fairygui中以组件为一个业务单元，所以，尽量将一个FairyGUi中的组件内的相关逻辑写入一个逻辑脚本中。
## 9.在需要的地方，使用FairyGUI类中的静态方法创建并展示UI.在Model层中可以使用优雅的泛型方式创建UI脚本并展示，但是在热更层中要放弃这些。
## 10. Unity Model层和Hotfix层增加宏 [Editor]

# **tips：**
## 1.设计：派生的Ui类型分为Normal和window两种类型，其中Normal对应FairyGUi编辑器中的Component ,Window对应FairyGUi中的window。window 用于弹出层，自动具有模态特性。
## 2.新版的ui模块中会有部分类和原Et框架的UI冲突，可以根据自己的实际情况做调整。
## 3. FairyGUi的全局配置在ETModel.UIManagetComponent的Awake中设置。
## 4.ab包加载功能未完善。
## 5.讨论qq:535421993。