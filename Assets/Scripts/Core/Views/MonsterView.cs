using Core.Data.Monster;
using UnityEngine;

namespace Core.Views
{
	public sealed class MonsterView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		
		public MonsterType MonsterType { get; private set; }

		public void Init(IMonsterData monsterData)
		{
			MonsterType = monsterData.MonsterType;

			_spriteRenderer.sprite = monsterData.MonsterSprite;
		}
	}
}