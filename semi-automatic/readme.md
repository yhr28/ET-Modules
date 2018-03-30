# 半自动化的ET服务端开发。
## 针对服务器端的相关规则，对服务端的开发进行一定程度的抽象，实现半自动化的开发，提高开发效率，实现“三天一个服务端”的目标。
### 抽象的前提就是规范，所有的事情都要有严格的规范，比如变量名的规范、参数的规范等，只有做到有迹可循，才能完成事半功倍的抽象。 Et中的服务端开发过程是有迹可循的。每个服务器都是由不同的进程组成，而每个进程又是由不同的组件和功能组成的，由此，一个可视化的结构可以是下面这样的

```
Server
    -服务器进程
        -Event事件
        -Component 数据组件、逻辑组件
        -handler处理器。
```

### 这样的话，我们在做一个et服务端的时候，无非就是在这些event、Component、handler中填写代码。所以一个半自动地生成对应的event、Component、handler结构化文件，真的是可以大大提高开发效率的。

## handler处理器半自动生成工具说明.
####  handler的分类：

```
根据Et的服务端设计，handler被分为如下四类：
    AMhandler       普通消息处理器
    AMActorHandler  普通Rpc消息的处理器
    AActorHanler    普通的actor处理器
    AActorRpcHandler actorRpc处理器
    
    其中，两种rpc都是有返回值的。
    
```

####  规范

```
我们要做的就是根据我们自己的业务需求使用上述的四种hanler做好网络消息的处理。
而使用上述处理器的前提就是有对应的消息类型；

使用半自动handler生成器对消息协议的命名规范：
    普通的消息：方向_动作。例如C2G_Login
    普通Rpc请求消息：方向_动作。例如C2G_Login
    普通Rpc响应消息：方向_动作。例如G2C_Login
    普通Actor消息：：Actor_方向_实体_动作 例如 Actor_C2G2M_Unit_Fight
    Actor 的Rpc消息请求：Actor_方向_实体_动作 例如 Actor_C2G2M_Unit_Fight
    Actor的Rpc消息的响应：Actor_方向_实体_动作 例如 Actor_M2G2C_Unit_Fight
    
使用半自动handler生成器对handler的命名规范：
    要处理消息的+hanler.例如 C2G_LoginHandler
    

```

####  工具说明

```

解压缩后得到如下文件：
AMActorRpcHandler.template  Actor的Rpc消息处理器的模板
AMessagehandler.template    普通的消息处理器的模板
AMRpcHandler.template       普通Rpc消息的处理器的模板
IActorHandler.template      普通的actor处理器的模板

gen                         这是一个文件夹，输出目录

HandlerManifest.txt         要生成的handler列表。将设计好的handler名字写到这个文件中。
GenHandler.py               python脚本，执行它开始生成handler.cs文件。

```

####  使用流程

1. 使用proto生成工具生成设计好的协议文件。协议必须遵循上述命名规范。
2. 设计好需要的Handler,将Handler名称放入【HandlerManifest.txt】文件中。
```
例如：
Actor_G2M_Unit_LogoutHandler
Actor_G2M_Room_ReconnectionHandler_RPC
G2M_CreateRoomHandler_RPC
G2M_EnterRoomHandler_RPC
G2M_GetCreatedRoomsHandler_RPC
Actor_C2G2M_Unit_LeaveRoomHandler_RPC
Actor_C2G2M_Unit_ReadyHandler_RPC
Actor_C2G2M_Unit_GetRoomInfoHandler_RPC
Actor_C2G2M_Unit_XiazhuHandler_RPC
Actor_C2G2M_Unit_KaipaiHanler_RPC
Actor_C2G2M_Unit_QiangzhuangHandler_RPC
Actor_C2G2M_Unit_TalkHandler
###### 注意！！！对于需要有返回值的RPC类消息的处理，需要在Handler后加上_RPC加以标记。
```

3. 执行GenHandler.py 
4. 在gen中查看生成的文件，导入项目中，做微调即可。然后就可以在这些handler里加入自己的逻辑啦。
