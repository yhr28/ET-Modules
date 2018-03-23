using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.InitSceneStart)]
	public class InitSceneStart_CreateLoginUI: AEvent
	{
		public override void Run()
		{
            FairyGUI.Open(typeof(LoginSceneUI_FairyGUI));
		}
	}
}
