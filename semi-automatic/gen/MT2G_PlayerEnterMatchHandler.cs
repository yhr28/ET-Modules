/**
*
*
*
* **/

using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.XX)]
	public class MT2G_PlayerEnterMatchHandler : AMHandler<MT2G_PlayerEnterMatch>
	{
		protected override async void Run(MT2G_PlayerEnterMatch message)
		{
			 await Task.CompletedTask;
		}
		
	}//class_end
}