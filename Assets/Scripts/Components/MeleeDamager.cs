using DG.Tweening;
using UnityEngine;

namespace TopDownShooter
{
	public class MeleeDamager : MonoBehaviour, IDamagerComponent
	{
		private void OnDestroy()
		{
			transform.DOKill();
		}


		public void Attack(Transform target, float damage, float delay)
		{
			transform.DOKill(true);
			transform.DOLocalRotate(new Vector3(0, -50, 0), 0.2f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
			{
				if (target.TryGetComponent(out HealthComponent hp))
				{
					hp.TakeDamage(damage);
				}
			});
		}
	}
}
