using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;


namespace radiants.ReactiveTween
{
	public static partial class Fader
	{
		/// <summary>
		/// ReactiveTween's Fade method.
		/// </summary>
		/// <param name="time">Fading time</param>
		/// <param name="onNext">Main lambda</param>
		/// <param name="onComplete"></param>
		/// <param name="dispatchCompleteWhenCanceled">If true, it dispatches onComplete() and onNext(1f) when fading is canceled by cancellationToken.</param>
		/// <param name="useUnscaledTime"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="timeScaleToken">You can set independent TimeScale for fading. it multiplied to Time.timeScale when useUnscaledTime is true.</param>
		/// <returns>UniTask for await</returns>
		public static async UniTask Fade(float time,
			System.Action<float> onNext,
			System.Action onComplete = null,
			bool dispatchCompleteWhenCanceled = true,
			bool useUnscaledTime = false,
			CancellationToken cancellationToken = default,
			TimeScaleToken timeScaleToken = null)
		{
			await EaseMain(time, Easing.Linear, onNext, onComplete, null, dispatchCompleteWhenCanceled, useUnscaledTime, cancellationToken, timeScaleToken);
		}

		/// <summary>
		/// ReactiveTween's Fade method.
		/// </summary>
		/// <param name="time">Fading time</param>
		/// <param name="onNext">Main lambda</param>
		/// <param name="onCanceled"></param>
		/// <param name="onComplete"></param>
		/// <param name="useUnscaledTime"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="timeScaleToken">You can set independent TimeScale for fading. it multiplied to Time.timeScale when useUnscaledTime is true.</param>
		/// <returns></returns>
		public static async UniTask Fade(float time,
			System.Action<float> onNext,
			System.Action onCanceled,
			System.Action onComplete = null,
			bool useUnscaledTime = false,
			CancellationToken cancellationToken = default,
			TimeScaleToken timeScaleToken = null)
		{
			await EaseMain(time, Easing.Linear, onNext, onComplete, onCanceled, false, useUnscaledTime, cancellationToken, timeScaleToken);
		}

		/// <summary>
		/// Fade method with Easing.
		/// </summary>
		/// <param name="time">Fading time</param>
		/// <param name="easing">Easing type</param>
		/// <param name="onNext">Main lambda</param>
		/// <param name="onComplete"></param>
		/// <param name="dispatchCompleteWhenCanceled">If true, it dispatches onComplete() and onNext(1f) when fading is canceled by cancellationToken.</param>
		/// <param name="useUnscaledTime"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="timeScaleToken">You can set independent TimeScale for fading. it multiplied to Time.timeScale when useUnscaledTime is true.</param>
		/// <returns></returns>
		public static async UniTask Ease(float time, Easing easing,
			System.Action<float> onNext,
			System.Action onComplete = null,
			bool dispatchCompleteWhenCanceled = true,
			bool useUnscaledTime = false,
			CancellationToken cancellationToken = default,
			TimeScaleToken timeScaleToken = null)
		{
			await EaseMain(time, easing, onNext, onComplete, null, dispatchCompleteWhenCanceled, useUnscaledTime, cancellationToken, timeScaleToken);
		}

		/// <summary>
		/// Fade method with Easing.
		/// </summary>
		/// <param name="time">Fading time</param>
		/// <param name="easing">Easing type</param>
		/// <param name="onNext">Main lambda</param>
		/// <param name="onCanceled"></param>
		/// <param name="onComplete"></param>
		/// <param name="useUnscaledTime"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="timeScaleToken">You can set independent TimeScale for fading. it multiplied to Time.timeScale when useUnscaledTime is true.</param>
		/// <returns></returns>
		public static async UniTask Ease(float time, Easing easing,
			System.Action<float> onNext,
			System.Action onCanceled,
			System.Action onComplete = null,
			bool useUnscaledTime = false,
			CancellationToken cancellationToken = default,
			TimeScaleToken timeScaleToken = null)
		{
			await EaseMain(time, easing, onNext, onComplete, onCanceled, false, useUnscaledTime, cancellationToken, timeScaleToken);
		}

		private static async UniTask EaseMain(float time, Easing easing,
			System.Action<float> onNext,
			System.Action onComplete,
			System.Action onCanceled,
			bool dispatchCompleteWhenCanceled,
			bool useUnscaledTime,
			CancellationToken cancellationToken,
			TimeScaleToken timeScaleToken)
		{
			float elapsedTime = 0f;
			while (elapsedTime < time)
			{
				float scaledTime = elapsedTime / time;
				onNext(easing.Ease(scaledTime));

				await UniTask.Yield();

				float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
				if (timeScaleToken != null) deltaTime *= timeScaleToken.TimeScale;
				elapsedTime += deltaTime;

				if (cancellationToken.IsCancellationRequested)
				{
					if (dispatchCompleteWhenCanceled)
					{
						onNext(1f);
						onComplete?.Invoke();
					}
					else
					{
						onCanceled?.Invoke();
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