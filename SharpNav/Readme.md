#基于ET框架简易的SharpNav案例：(ET版本3.5)

SharpNav下载地址:https://github.com/Robmaister/SharpNav

编译注意，很多dll都是报警状态。更新dll即可，但特别注意：OpenTK.dll需要的是2.0.0版本。当然，如果SharpNav有更新。
估计这个OpenTK.dll也会有相应的更新，这个请大家自己留意就好。

先简单介绍一下SharpNav：
这个SharpNav是3D-NavMesh服务器导航的开源项目，经过本人的验证完全可以用在ET的服务端导航里。所以为了让大家能更快地能用起SharpNav,也
为了进一步完善ET框架的Module。故在这里给大家简单介绍一下SharpNav的案例。

##案例介绍：
1.可以使用Uniy3D自带的工具File--ExPort--Wavefront Obj导出*.obj文件;

2.也可以使用本人提供的ETSceneObj.cs脚本导出的*.obj文件;(推荐使用，因为这个可以多个对象导出)

3.将以上导出的*.obj复制到路径\SharpNav-master\Binaries\Examples\Debug文件夹内就可以了。

4.然后，将ExampleWindow.Drawing.cs里的LoadLevel()内的level=new ObjModel("nav_test.obj")的nav_test.obj名字改成你生成的*.obj。

5.在Debug模式下在SharpNav.Examples下的ObjModel.cs的norms.Add(tempNorm[n0])处打断点，当程序运行到此处时可以看到Vector3的值。这个值，它
就是SharpNav在服务端生成的地图具体所在的位置值。请记住这个值，下一步要用！！！！！如本人得到的值Vector3(-100,-7,7)

6.将地图的Vector3的值填入example工程里的，

             * GeneratePathfinding的
             * SVector3 c = new SVector3(-100, -7, 7);
             * SVector3 e = new SVector3(5, 5, 5); 
             
             *+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+* 
             * 及 GenerateCrowd() 里的 
             * SVector3 c = new SVector3(-100, -7, 7);
             * SVector3 e = new SVector3(5, 5, 5);                                  
             */
 (如果上面两个所填的Vector3的值差距太大，Agent就不能正常在界面里出现。如有Agent不出现的情况，请仔细检查这三处的值!!!!)
 
 7.以上准备工作都做好后，就可以运行SharpNav的example这个案例了。记得把example设置为启动项目。运行后，先点生成(Generate)导航。
 然后，点Agent界面内Count的-或+来增减Agent了。
 
 
 

对SharpNav更详细的介绍请点击：https://github.com/Robmaister/SharpNav

框架作者：小木（QQ:29858488）
