using System;
using Plugins.CloudCanards.Inspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CozyCatCafe.Scripts
{
	public class CustomerSpawner : MonoBehaviour
	{
		private static int _randomBase;
		private static int _spawnNo;
		
		[Required]
		public Food[] OrderList;
		
		[Required]
		public Seat[] Seats;
		public Customers CustomerPrefab;

		public float MinDuration = 1f;
		public float MaxDuration = 5f;

		private float _remainingDuration = 2f;

		public SpritePair[] Sprites;

		private void Update()
		{
			_remainingDuration -= Time.deltaTime;
			if (_remainingDuration < 0f)
			{
				_remainingDuration = Random.Range(MinDuration, MaxDuration);

				for (var i = 0; i < Seats.Length; i++)
				{
					if (Seats[i].Customer == null)
					{
						var obj = Instantiate(CustomerPrefab, transform.position, Quaternion.identity);
						obj.setSeat(Seats[i]);
						var sprite = Sprites[Random.Range(0, Sprites.Length)];
						obj.SetSprite(sprite.Sitting, sprite.WalkingBack);

						var index = _spawnNo;
						if (index >= OrderList.Length)
						{
							index = Random.Range(0, OrderList.Length);
							_spawnNo = 0;
							_randomBase = Random.Range(0, 100);
						}
						else
						{
							_spawnNo++;
						}

						obj.orderDish = OrderList[(index + _randomBase) % OrderList.Length];
						return;
					}
				}
			}
		}

		[Serializable]
		public struct SpritePair
		{
			public Sprite WalkingBack;
			public Sprite Sitting;
		}
	}
}