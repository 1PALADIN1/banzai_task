using UnityEngine;

namespace Core.Data.Gun
{
	public interface IGunDatabase
	{
		GameObject ViewPrefab { get; }
		GunData[] Guns { get; }
	}
	
	[CreateAssetMenu(fileName = "GunDatabase", menuName = "Game/Data/GunDatabase")]
	public sealed class GunDatabase : ScriptableObject, IGunDatabase
	{
		[SerializeField] private GameObject _viewPrefab;
		[SerializeField] private GunData[] _guns;
		
		public GameObject ViewPrefab => _viewPrefab;
		public GunData[] Guns => _guns;
	}
}