# Unity ReactiveTween

UniTaskを利用したシンプルかつ何でもできるTweenシステムです。
UniRx版も存在しますが、R3環境ではこちらを使用してください。

## 動作環境

- Unity 2019.3以上(UniTaskの動作環境と同様)
- UniTask https://github.com/Cysharp/UniTask

## git URLからのインストール

Package Managerに `https://github.com/sh-kj/ReactiveTween.git?path=Unity-UniTask/ReactiveTween` を、  
`Add package from git URL` で追加してください。

## 使用法

最もシンプルな利用法は以下です。  
0.5秒のあいだ、毎UpdateでDoSomething()に0f～1fと徐々に変化するfloatが渡され呼ばれます。  
最後には必ず`DoSomething(1f)`となります。
```
await Fader.Fade(0.5f, value => DoSomething(value));
```
`Fader.Fade` はUniTaskを返すので、フェード処理が進んでいる間await可能です。

### 途中でのキャンセル

```
System.Threading.CancellationTokenSource source = new System.Threading.CancellationTokenSource();
var task = Fader.Fade(0.5f, value => DoSomething(value), () => DoComplete(), true, false, source.Token);

source.Cancel();  //これで中途キャンセルが可能
```
非同期処理で一般的なCancellationTokenを使ってキャンセルできます。  
`dispatchCompleteWhenCanceled=true`にしておくとキャンセル時にも`OnComplete()`と`DoSomething(1f)`が呼ばれます。

### イージング

`Easing` enumと拡張メソッドを使うとイージング関数が呼べます。  
`using radiants.ReactiveTween;` としておき、以下のように使うと便利です。

```
Fader.Fade(0.5f, value => transform.position = Easing.QuadOut.Ease(
		new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), value));
```

### イージングの初期指定

`Fader.Ease`で最初からイージングを指定しておくこともできます。  
`onNext`で呼ばれる数値に既にイージングが掛かった状態になることに注意してください。

```
Fader.Ease(0.5f, Easing.QuadOut, value => transform.position = Vector3.Lerp(
		new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), value));
```

## 引数

### `float` time

フェードに掛かる秒数

### `System.Action<float>` onNext

フェード中毎Updateで呼ばれるラムダ。引数には0f～1fが入り、フェード中に徐々に変化していく

### `System.Action` onComplete

フェード終了後に呼ばれる。

### `bool` dispatchCompleteWhenCanceled

trueにしておくと、CancellationTokenで途中キャンセルされた場合でも `onNext(1f)` と `onComplete()` が呼ばれる。  
クリックで演出をスキップする場合などに便利

### `System.Action` onCanceled

dispatchCompleteWhenCanceledと選択となる。  
これを指定している場合、CancellationTokenで途中キャンセルされたときに `onCanceled()` が呼ばれる。  
この場合は`onNext(1f)`は呼ばれない。

### `bool` useUnscaledTime

trueにすると、`Time.deltaTime`ではなく`Time.unscaledDeltaTime`をフェード時間管理に使用するようになり、`Time.timeScale` の影響を受けなくなる。  
この場合でも`timeScaleToken`の影響は受ける。

### `CancellationToken` cancellationToken

処理を途中でキャンセルするためのトークン

### `TimeScaleToken` timeScaleToken

これを指定することで、独自にフェード処理の時間の進みを変化させられる。  
0を入れることでポーズが可能。マイナスも入力可能だが、処理が終わらなくなってしまうのに注意


## License

MIT