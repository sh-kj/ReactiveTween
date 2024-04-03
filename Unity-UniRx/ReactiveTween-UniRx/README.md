# ReactiveTween for UniRx

Fast, less-memory-alloc Tween system using UniRx and Microcoroutine.

if you are using R3, you can use UniTask version. https://github.com/sh-kj/ReactiveTween.git?path=Unity-UniTask/ReactiveTween

## Requirement

- Unity 2017 or higher(can be compatible with older version)
- UniRx https://github.com/neuecc/UniRx

## Install via git URL

You can add `https://github.com/sh-kj/ReactiveTween.git?path=Unity-UniRx/ReactiveTween-UniRx` to Package Manager/Add package from git URL.

## Usage

have to `using UniRx;`

```
Fader.Fade(0.5f)
	.Subscribe(value => DoSomething(value));
```
After Subscribe, `Fader.Fade` dispatches `value` every frame until 0.5(argument value) second.  
`value` interpolates 0 to 1 while argument second, so you can do anything using normalized time.

```
var fade = Fader.Fade(2.0f)
	.Subscribe(value => DoSomething(value), () => DoComplete());

fade.Dispose();
```

You can cancel stream to `Dispose()`.  
`onCompleted` action will be called when it canceled too.

## Easing

`Easing` contains some easing equations to use, like this;

```
Fader.Fade(0.5f)
	.Subscribe(value => transform.position = Easing.QuadIn.Ease(
		new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), value));
```

## License

MIT