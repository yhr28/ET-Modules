/**
demo事件。
**/

using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.InitSceneStart)]
	public class InitSceneStart_CreateLoginUI: AEvent
	{
		public override void Run()
		{
		    UI.Open(typeof(LoginSceneUI_FairyGUI));
		}
	}
}
