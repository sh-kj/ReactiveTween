using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;


namespace radiants.ReactiveTween
{
	public static partial class Fader
	{
		public static async UniTask Fade(float time,
			System.Action<float> onNext,
			System.Action onComplete = null,
			bool dispatchCompleteWhenCanceled = true,
			bool useUnscaledTime = false,
			CancellationToken cancellationToken = default,
			TimeScaleToken timeScaleToken = null)
		{
			float elapsedTime = 0f;
			while (elapsedTime < time)
			{
				float scaledTime = elapsedTime / time;
				onNext(scaledTime);

				await UniTask.Yield();

				float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
				if (timeScaleToken != null) deltaTime *= timeScaleToken.TimeScale;
				elapsedTime += deltaTime;

				if (cancellationToken.IsCancellationRequested)
				{
					if(dispatchCompleteWhenCanceled)
					{
						onNext(1f);
						onComplete?.Invoke();
					}
					return;
				}
			}
			onNext(1f);
			onComplete?.Invoke();
		}
	}


	public class TimeScaleToken
	{
		public float TimeScale { get; set; } = 1f;
	}
}