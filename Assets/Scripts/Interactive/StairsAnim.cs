using DG.Tweening;
using UnityEngine;

namespace TopDownShooter
{
	public class StairsAnim : MonoBehaviour
	{
		public void Anim()
		{
			transform.DOLocalRotate(new Vector3(-90, 0, 90), 1f);
		}
	}
}