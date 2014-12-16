using UnityEngine;
using System.Collections;

namespace Scripts
{
	public class PlatformMover : MonoBehaviour
	{
		private void Awake()
		{
			rigidbody2D.velocity = new Vector2(-25, 0);
		}
	}
}