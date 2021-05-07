using Core.Data.Monster;
using UnityEngine;
using Zenject;

namespace Core.Logic.Monster
{
	public class MonsterSpawner : MonoBehaviour
	{
		[SerializeField] private int _maxMonsterSize = 10;
		
		private IMonsterFactory _monsterFactory;
		private int _monsterSize;
		
		[Inject]
		public void Init(IMonsterFactory monsterFactory)
		{
			_monsterFactory = monsterFactory;
		}

		private void CreateMonster()
		{
			if (_monsterSize >= _maxMonsterSize)
				return;

			var monster = _monsterFactory.CreateMonster(MonsterType.Orc);
			_monsterSize++;
		}
		
		//TODO: debug
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
				CreateMonster();
		}
	}
}