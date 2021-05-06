using Core.Data.Monster;
using Core.Views;

namespace Core.Logic.Monster
{
	public interface IMonsterFactory
	{
		MonsterView CreateMonster(MonsterType monsterType);
	}
}