using UnityEngine;
using Zenject;

namespace Core.Logic.Monster
{
	public class MonsterSpawner : MonoBehaviour
	{
		private IMonsterFactory _monsterFactory;
		
		[Inject]
		public void Init(IMonsterFactory monsterFactory)
		{
			_monsterFactory = monsterFactory;
		}
	}
}