using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace radiants.ReactiveTween
{
	public enum Easing
	{
		Linear,

		SinIn,
		SinOut,
		SinInOut,

		QuadOut,
		QuadIn,
		QuadInOut,

		CubicOut,
		CubicIn,
		CubicInOut,

		QuartOut,
		QuartIn,
		QuartInOut,

		QuintOut,
		QuintIn,
		QuintInOut,

		ExpoOut,
		ExpoIn,
		ExpoInOut,

		CircuOut,
		CircuIn,
		CircuInOut,

		BackOut,
		BackIn,
		BackInOut,

		ElasticOut,
		ElasticIn,
		ElasticInOut,

		BounceOut,
		BounceIn,
		BounceInOut
	}

	public static class EasingExt
	{
		private static readonly Dictionary<int, System.Func<float, float>> EaseFuncDict = new Dictionary<int, System.Func<float, float>>()
		{
			{(int)Easing.Linear, _f => _f },

			{ (int)Easing.SinIn, EasingFunctions.SinIn },
			{ (int)Easing.SinOut, EasingFunctions.SinOut },
			{ (int)Easing.SinInOut, EasingFunctions.InOut(EasingFunctions.SinIn, EasingFunctions.SinOut) },

			{ (int)Easing.QuadIn, EasingFunctions.QuadIn },
			{ (int)Easing.QuadOut, EasingFunctions.QuadOut },
			{ (int)Easing.QuadInOut, EasingFunctions.InOut(EasingFunctions.QuadIn, EasingFunctions.QuadOut) },

			{ (int)Easing.CubicIn, EasingFunctions.CubicIn },
			{ (int)Easing.CubicOut, EasingFunctions.CubicOut },
			{ (int)Easing.CubicInOut, EasingFunctions.InOut(EasingFunctions.CubicIn, EasingFunctions.CubicOut) },

			{ (int)Easing.QuartIn, EasingFunctions.QuartIn },
			{ (int)Easing.QuartOut, EasingFunctions.QuartOut },
			{ (int)Easing.QuartInOut, EasingFunctions.InOut(EasingFunctions.QuartIn, EasingFunctions.QuartOut) },

			{ (int)Easing.QuintIn, EasingFunctions.QuintIn },
			{ (int)Easing.QuintOut, EasingFunctions.QuintOut },
			{ (int)Easing.QuintInOut, EasingFunctions.InOut(EasingFunctions.QuintIn, EasingFunctions.QuintOut) },

			{ (int)Easing.ExpoIn, EasingFunctions.ExpoIn },
			{ (int)Easing.ExpoOut, EasingFunctions.ExpoOut },
			{ (int)Easing.ExpoInOut, EasingFunctions.InOut(EasingFunctions.ExpoIn, EasingFunctions.ExpoOut) },

			{ (int)Easing.CircuIn, EasingFunctions.CircuIn },
			{ (int)Easing.CircuOut, EasingFunctions.CircuOut },
			{ (int)Easing.CircuInOut, EasingFunctions.InOut(EasingFunctions.CircuIn, EasingFunctions.CircuOut) },

			{ (int)Easing.BackIn, EasingFunctions.BackIn },
			{ (int)Easing.BackOut, EasingFunctions.BackOut },
			{ (int)Easing.BackInOut, EasingFunctions.InOut(EasingFunctions.BackIn, EasingFunctions.BackOut) },

			{ (int)Easing.ElasticIn, EasingFunctions.ElasticIn },
			{ (int)Easing.ElasticOut, EasingFunctions.ElasticOut },
			{ (int)Easing.ElasticInOut, EasingFunctions.InOut(EasingFunctions.ElasticIn, EasingFunctions.ElasticOut) },

			{ (int)Easing.BounceIn, EasingFunctions.BounceIn },
			{ (int)Easing.BounceOut, EasingFunctions.BounceOut },
			{ (int)Easing.BounceInOut, EasingFunctions.InOut(EasingFunctions.BounceIn, EasingFunctions.BounceOut) },
		};

		public static float Ease(this Easing _ease, float _t)
		{
			return EaseFuncDict[(int)_ease](_t);
		}
		public static float EaseSaturate(this Easing _ease, float _t)
		{
			EasingFunctions.Saturate(ref _t);
			return EaseFuncDict[(int)_ease](_t);
		}

		#region Lerp Methods

		public static float Ease(this Easing _ease, float _from, float _to, float _t)
		{
			return _from + (_to - _from) * _ease.Ease(_t);
		}
		public static float EaseSaturate(this Easing _ease, float _from, float _to, float _t)
		{
			return _from + (_to - _from) * _ease.EaseSaturate(_t);
		}

		public static float EaseAngleDegree(this Easing _ease, float _from, float _to, float _t)
		{
			float delta = _to - _from;
			if (Mathf.Abs(delta) < 180f)
				return _from + delta * _ease.Ease(_t);

			if(_to > _from)
			{
				while (_to - _from > 180f)
					_from += 360f;
			}
			else
			{
				while (_from - _to > 180f)
					_from -= 360f;
			}
			return _from + (_to - _from) * _ease.Ease(_t);
		}

		public static float EaseAngleRadian(this Easing _ease, float _from, float _to, float _t)
		{
			float delta = _to - _from;
			if (Mathf.Abs(delta) < Mathf.PI)
				return _from + delta * _ease.Ease(_t);

			if (_to > _from)
			{
				while (_to - _from > Mathf.PI)
					_from += Mathf.PI * 2f;
			}
			else
			{
				while (_from - _to > Mathf.PI)
					_from -= Mathf.PI * 2f;
			}
			return _from + (_to - _from) * _ease.Ease(_t);
		}

		public static Vector2 Ease(this Easing _ease, Vector2 _from, Vector2 _to, float _t)
		{
			return Vector2.Lerp(_from, _to, _ease.Ease(_t));
		}
		public static Vector2 EaseSaturate(this Easing _ease, Vector2 _from, Vector2 _to, float _t)
		{
			return Vector2.Lerp(_from, _to, _ease.EaseSaturate(_t));
		}

		public static Vector3 Ease(this Easing _ease, Vector3 _from, Vector3 _to, float _t)
		{
			return Vector3.Lerp(_from, _to, _ease.Ease(_t));
		}
		public static Vector3 EaseSaturate(this Easing _ease, Vector3 _from, Vector3 _to, float _t)
		{
			return Vector3.Lerp(_from, _to, _ease.EaseSaturate(_t));
		}

		public static Vector4 Ease(this Easing _ease, Vector4 _from, Vector4 _to, float _t)
		{
			return Vector4.Lerp(_from, _to, _ease.Ease(_t));
		}
		public static Vector4 EaseSaturate(this Easing _ease, Vector4 _from, Vector4 _to, float _t)
		{
			return Vector4.Lerp(_from, _to, _ease.EaseSaturate(_t));
		}

		public static Quaternion Ease(this Easing _ease, Quaternion _from, Quaternion _to, float _t)
		{
			return Quaternion.Lerp(_from, _to, _ease.Ease(_t));
		}
		public static Quaternion EaseSaturate(this Easing _ease, Quaternion _from, Quaternion _to, float _t)
		{
			return Quaternion.Lerp(_from, _to, _ease.EaseSaturate(_t));
		}

		public static Color Ease(this Easing _ease, Color _from, Color _to, float _t)
		{
			return Color.Lerp(_from, _to, _ease.Ease(_t));
		}
		public static Color EaseSaturate(this Easing _ease, Color _from, Color _to, float _t)
		{
			return Color.Lerp(_from, _to, _ease.EaseSaturate(_t));
		}
		#endregion


	}




	public static class EasingFunctions
	{
		[MethodImpl(256)]
		public static void Saturate(ref float _value)
		{
			if (_value > 1f) _value = 1f;
			if (_value < 0f) _value = 0f;
		}

		[MethodImpl(256)]
		public static System.Func<float, float> InOut(System.Func<float, float> _in, System.Func<float, float> _out)
		{
			return _t =>
			{
				if (_t < 0.5f)
					return _in(_t * 2f) * 0.5f;
				else
					return _out((_t - 0.5f) * 2f) * 0.5f + 0.5f;
			};
		}


		[MethodImpl(256)]
		public static float QuadIn(float t)
		{
			return t * t;
		}
		[MethodImpl(256)]
		public static float QuadOut(float t)
		{
			return -t * (t - 2.0f);
		}

		[MethodImpl(256)]
		public static float CubicIn(float t)
		{
			return t * t * t;
		}
		[MethodImpl(256)]
		public static float CubicOut(float t)
		{
			t = t - 1f;
			return t * t * t + 1;
		}

		[MethodImpl(256)]
		public static float QuartIn(float t)
		{
			return t * t * t * t;
		}
		[MethodImpl(256)]
		public static float QuartOut(float t)
		{
			t = t - 1f;
			return t * t * t * t + 1;
		}

		[MethodImpl(256)]
		public static float QuintIn(float t)
		{
			return t * t * t * t * t;
		}
		[MethodImpl(256)]
		public static float QuintOut(float t)
		{
			t = t - 1f;
			return t * t * t * t * t + 1;
		}

		[MethodImpl(256)]
		public static float SinIn(float t)
		{
			return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
		}
		[MethodImpl(256)]
		public static float SinOut(float t)
		{
			return Mathf.Sin(t * Mathf.PI * 0.5f);
		}

		[MethodImpl(256)]
		public static float ExpoIn(float t)
		{
			//y=2^(10*(x-1))
			if (t == 0f) return 0f;

			return Mathf.Pow(2f, 10 * (t - 1));
		}
		[MethodImpl(256)]
		public static float ExpoOut(float t)
		{
			//y=-2^(-10*x) +1

			if (t == 1f) return 1f;

			return -Mathf.Pow(2f, (-10f * t)) + 1f;
		}

		[MethodImpl(256)]
		public static float CircuIn(float t)
		{
			//-(sqrt(1-x^2)-1)
			Saturate(ref t);
			return -Mathf.Sqrt(1 - t * t) + 1;
		}
		[MethodImpl(256)]
		public static float CircuOut(float t)
		{
			//sqrt(1-(x-1)^2)

			Saturate(ref t);
			t = t - 1f;
			return Mathf.Sqrt(1 - t * t);
		}

		private const float s = 1.70158f;
		[MethodImpl(256)]
		public static float BackIn(float t)
		{
			return t * t * ((s + 1) * t - s);
		}
		[MethodImpl(256)]
		public static float BackOut(float t)
		{
			t = t - 1f;
			return t * t * ((s + 1) * t + s) + 1f;
		}


		[MethodImpl(256)]
		public static float ElasticIn(float t)
		{
			//y=2^(10*(x-1)) * sin(6.5*π*x)
			return Mathf.Pow(2, 10 * (t - 1))
				* Mathf.Sin(6.5f * Mathf.PI * t);
		}
		[MethodImpl(256)]
		public static float ElasticOut(float t)
		{
			//y=2^(10*(-x)) * sin((x*6.5-0.5)*π) + 1
			return Mathf.Pow(2, -10 * t)
				* Mathf.Sin((t * 6.5f - 0.5f) * Mathf.PI) + 1f;
		}

		[MethodImpl(256)]
		public static float BounceOut(float t)
		{
			if(t < 1f/2.75f)
			{
				return 7.5625f * t * t;
			}
			else if(t < 2f / 2.75f)
			{
				t -= 1.5f / 2.75f;
				return 7.5625f * t * t + 0.75f;
			}
			else if(t < 2.5f/ 2.75f)
			{
				t -= 2.25f / 2.75f;
				return 7.5625f * t * t + 0.9375f;
			}
			else
			{
				t -= 2.625f / 2.75f;
				return 7.5625f * t * t + 0.984375f;
			}
		}
		[MethodImpl(256)]
		public static float BounceIn(float t)
		{
			return 1f - BounceOut(1f - t);
		}


	}

}