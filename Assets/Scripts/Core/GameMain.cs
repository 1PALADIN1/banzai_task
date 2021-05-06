using UnityEngine;
using Zenject;

namespace Core
{
	public class GameMain : MonoBehaviour
	{
		private DiContainer _diContainer;
		
		[Inject]
		public void Init(DiContainer container)
		{
			_diContainer = container;
		}
	}
}