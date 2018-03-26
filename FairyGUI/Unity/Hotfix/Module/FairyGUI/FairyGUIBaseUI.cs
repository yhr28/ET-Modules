using FairyGUI;

namespace ETHotfix
{
    public class FairyGUIBaseUI : Component
    {
        //包名
        public string pakName;
        //组件名
        public string cmpName;
        //FairyGUI的实例对象
        public GObject gObj;
        //ui类型
        public ETModel.FairyGUIType UIType = ETModel.FairyGUIType.UITYPE_POPUP;

        public FairyGUIBaseUI() { }

        /// <summary>
        /// InitUI 初始化ui
        /// </summary>
        public virtual void InitUI()
        {

        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public virtual void Close()
        {
            Game.Scene.GetComponent<FairyGUIComponent>().Destory(this);
        }


        /// <summary>
        /// 关闭层
        /// </summary>
        public virtual void CloseLayer()
        {
            Game.Scene.GetComponent<FairyGUIComponent>().DestoryLayer(this);
        }


        /// <summary>
        /// 关闭所有的UI（清空当前场景）
        /// </summary>
        public virtual void CloseAll()
        {
            Game.Scene.GetComponent<FairyGUIComponent>().DestoryAll();
        }

        /// <summary>
        /// 播放UI特效。音效等。
        /// </summary>
        public virtual void DisplayEffect()
        {

        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            //关闭FairyGUi组件
            if (gObj != null)
            {
                gObj.Dispose();
            }
            base.Dispose();
        }


    }//class end
}
