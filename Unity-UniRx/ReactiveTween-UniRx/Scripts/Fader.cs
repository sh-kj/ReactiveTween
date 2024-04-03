using System.Collections;
using System;
using System.Threading;
using UnityEngine;
using UniRx;

namespace radiants.ReactiveTween
{
	public static partial class Fader
	{
		public static IObservable<float> Fade(float time, bool useUnscaledTime = false)
		{
			return Observable.FromMicroCoroutine<float>(
				(_observer, _cancellation) => FadeMicroCoroutine(_observer, _cancellation, time, useUnscaledTime));
		}


		private static IEnumerator FadeMicroCoroutine(IObserver<float> observer, CancellationToken cancellation,
			float time, bool useUnscaledTime)
		{
			float elapsedTime = 0f;
			while (elapsedTime < time)
			{
				float scaledTime = elapsedTime / time;
				observer.OnNext(scaledTime);
				yield return null;

				elapsedTime += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

				if (cancellation.IsCancellationRequested)
				{
					observer.OnCompleted();
					yield break;
				}
			}
			observer.OnNext(1f);
			observer.OnCompleted();
		}
	}
}
