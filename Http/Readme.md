# 文件说明

`Server.Model` > `Module` > `Http`下面:

> HttpComponent.cs 在原来的基础上，添加了中间件、IOC容器。

> HttpHandlerAttribute.cs 增加Get和Post两个特性，用于MVC中间的实现。

> IHttpHandler.cs 增加 `AHttpHandler` 抽象类以及两个便捷响应方法 `Ok`，`Error`。

> MvcMiddleware.cs MVC中间件的实现，主要就是Get和Post请求参数的注入。


`Server.Hotfix` > `Module` > `Http`下面:
> HttpComponentSystem.cs 逻辑，添加各种中间件，如：MVC中间件、用拦截器的中间件以及自定义中间件。

# 起步

## 第一步
确认在`Server.Hotfix` > `Module` > `Http` > `HttpComponentComponentAwakeSystem.cs`中，添加`MVC`中间件。
```cs
[ObjectSystem]
public class HttpComponentComponentAwakeSystem : AwakeSystem<HttpComponent>
{
    public override void Awake(HttpComponent self)
    {
        // 添加MVC中间件
        self.UseMvc();

        self.Awake();
    }
}
```

## 第二步

创建Controller：
```cs
namespace ETHotfix
{
    [HttpHandler(AppType.Gate, "/")]
    public class LoginController : AHttpHandler
    {
        //请求URL: /Login，默认为：HttpHandler中的Path + 方法名
        [Post]
        public object Login(Account account)    
        {
            return Ok("登陆成功！");
        }

        //请求URL: /GetAccountInfo
        [Get]
        public Account GetAccountInfo()
        {
            Account account = new Account();
            account.Pwd = 1111;
            return account;
        }
    }
}
```
> `account`参数自动从Post请求中的json注入。

> 返回对象，自动转换为`json`响应回客户端。转换用的是ET服务端中的JsonHelper类进行转换的。

## Get使用
```cs
//请求URL: /GetInfo
[Get("/GetInfo")]
public Account GetAccountInfo()
{
    Account account = new Account();
    account.Pwd = 1111;
    return account;
}

//请求URL: /GetInfo?name=张三
[Get("/GetInfo")]
public Account GetAccountInfo(string name) // name参数自动从url中注入
{
    Account account = new Account();
    account.Pwd = 1111;
    account.Name = name;

    return account;
}

//请求URL: /GetInfo?name=张三&age=10
[Get("/GetInfo")]
public Account GetAccountInfo(string name, int age) // name与age参数自动从url中注入
{
    Account account = new Account();
    account.Pwd = 1111;
    account.Name = name;
    account.Age = age;

    return account;
}
```

## Post使用
Post跟Get使用方式差不多，只是注入的是一个对象。
```cs
//请求URL: /Login，默认为：HttpHandler中的Path + 方法名
//请求body：{'Name':'张三', 'Age': '10'}
[Post]
public object Login(Account account)    // account 从请求body中注入
{
    Log.info(account.Name); // Name = 张三
    Log.info(account.Age); // Age = 10

    return Ok("登陆成功！");
}
```

## HttpListenerRequest 与 HttpListenerResponse 的注入
有时候会用到原始的Request和Response，只需要写在方法参数任意位置即可注入。
```cs
[Post]
public object Login(Account account, HttpListenerResponse resp)
{
    Log.info(account.Name);
    Log.info(account.Age);

    return Ok("登陆成功！");
}

[Post]
public object Login(Account account, HttpListenerRequest req)
{
    Log.info(account.Name);
    Log.info(account.Age);

    return Ok("登陆成功！");
}

[Post]
public object Login(Account account, HttpListenerRequest req, HttpListenerResponse resp)
{
    Log.info(account.Name);
    Log.info(account.Age);

    return Ok("登陆成功！");
}
```

## postBody 注入
为了方便拿到Post请求Body的数据：
```cs
[Post]
public object Login(Account account, string postBody) // 约定参数名称为postBody,只传string类型。本来是byte[]，有需求可以改。
{
    Log.info(account.Name);
    Log.info(account.Age);

    return Ok("登陆成功！");
}
```
> postBody里面就是请求上来的json字符串。

# 中间件的使用


## 用HttpComponent中Run方法来注册中间件
```cs
[ObjectSystem]
public class HttpComponentComponentAwakeSystem : AwakeSystem<HttpComponent>
{
    public override void Awake(HttpComponent self)
    {
        self.Run((context, next) =>
        {
            Log.Info("这是第一个中间件....." + context.Request.Url.AbsolutePath);

            return next(context);
        });


        self.Run("/t", (c, n) =>
        {
            Log.Info("这是第二个中间件 拦截 /t 请求，继续往后调用.....");

            // 可以对特定的URL做一些处理（批量拦截还未完成）。

            return n(c);
        });

        self.Run((c, n) =>
        {

            Log.Info("这是第三个中间件，不会往后继续调用了.....");

            return null; // 不往后执行了。相当于返回Task.CompletedTask。
        });
        self.Awake();
    }
}
```

## 用HttpComponent中Use<T>来注册中间件
```cs
/// <summary>
/// Mvc中间件
/// </summary>
public class MvcMiddleware
{
    private RequestDelegate _next; // 下一个中间件

    public MvcMiddleware(RequestDelegate next, HttpComponent httpComponent) // httpComponent 是注入进来的，不用就可以不写。
    {
        _next = next;
    }

    // 约定方法，由上一个中间件选择调用。
    public Task Invoke(HttpListenerContext context)
    {
        // 逻辑

        return _next(context); // 调用下一个中间件，也可以选择不调用了，这里跟Run差不多。
    }
}
```

# IOC 未完待续.....