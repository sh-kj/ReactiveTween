# Unity ReactiveTween

Fast, less-memory-alloc Tween system using UniTask.

## Requirement

- Unity 2019.3 or higher(can be compatible with older version)
- UniTask https://github.com/Cysharp/UniTask

## Install via git URL

You can add `https://github.com/sh-kj/ReactiveTween.git?path=Unity-UniTask/ReactiveTween` to Package Manager/Add package from git URL.

## Usage

have to `using Cysharp.Threading.Tasks;`

```
await Fader.Fade(0.5f, value => DoSomething(value));
```
`Fader.Fade` returns awaitable UniTask object.  

```
System.Threading.CancellationTokenSource source = new System.Threading.CancellationTokenSource();
var task = Fader.Fade(0.5f, value => DoSomething(value), () => DoComplete(), false, source.Token);
source.Cancel();
```
You can cancel task by CancellationToken.  
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