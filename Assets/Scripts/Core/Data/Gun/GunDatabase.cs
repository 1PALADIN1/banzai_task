using UnityEngine;

namespace Core.Data.Gun
{
	public interface IGunDatabase
	{
		IGunData[] Guns { get; }
	}
	
	[CreateAssetMenu(fileName = "GunDatabase", menuName = "Game/Data/GunDatabase")]
	public sealed class GunDatabase : ScriptableObject, IGunDatabase
	{
		[SerializeField] private GunData[] _guns;
		
		public IGunData[] Guns => _guns;
	}
}