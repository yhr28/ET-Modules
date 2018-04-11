自定义  添加 删除 监听事件
hotfix init 挂载组件
Game.Scene.AddComponent<EventCenterController>();

组件添加监听
EventCenterController  eventCenter = Game.Scene.GetComponent<EventCenterController>();

  
eventCenter.AddMsg(WarEvent.testEvet, GetMsg);
eventCenter.AddMsg(WarEvent.testEvet, GetMsg1);
eventCenter.AddMsg(WarEvent.testEvet, GetMsg2);
        void GetMsg()
        {
            Log.Info("get new evnet 没参数");
        }

        void GetMsg1(object obj)
        void GetMsg1<T>(T obj)
        {
            Log.Info($"get new evnet 参数1 {obj} ");
        }

        void GetMsg2(object obj, object obj1)
        {
            Log.Info($"get new evnet 参数2  {obj}  {obj1}");
        }

发送事件
             ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            rc.Get<GameObject>("Button").GetComponent<Button>().onClick.Add(() => { eventCenter.SendMsg(WarEvent.testEvet); });
            rc.Get<GameObject>("Button (1)").GetComponent<Button>().onClick.Add(() => { eventCenter.SendMsg(WarEvent.testEvet,"asdsad"); });
            rc.Get<GameObject>("Button (2)").GetComponent<Button>().onClick.Add(() => { eventCenter.SendMsg(WarEvent.testEvet,2, gameObject); });

删除监听
    eventCenter.RemoveMsg(testEvet.testEvet, XXXXX);