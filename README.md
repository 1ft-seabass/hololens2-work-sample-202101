# hololens2-work-sample-202101

## バージョン情報

* Unity 2019.4.1f1
* Microsoft.MixedReality.Toolkit.Unity.Examples.2.4.0.unitypackage

## プロジェクト基礎設定手順

2021/01/13 現在、以下の手順を行っています。

[MRTK チュートリアルの概要 \- Mixed Reality \| Microsoft Docs](https://docs.microsoft.com/ja-jp/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-01)

> * 正しいツールがインストールされている構成済みの Windows 10 PC
> * Windows 10 SDK 10.0.18362.0 以降
> * 開発用に構成された HoloLens 2 デバイス
> * Unity 2019 LTS がインストールされ、ユニバーサル Windows プラットフォーム ビルド サポート モジュールが追加された Unity Hub

[プロジェクトの初期化と最初のアプリケーションの配置 \- Mixed Reality \| Microsoft Docs](https://docs.microsoft.com/ja-jp/windows/mixed-reality/develop/unity/tutorials/mr-learning-base-02)

現状、レガシ XR を使っているが、MRTK の新しいバージョンや、一部のチュートリアルなどで新しいXR (Open XR ?) を使うケースも出てきているので以後注視する。

2021/01/13 現在、このように言われている。

> 新しいシステムはこのチュートリアル シリーズに対して推奨される Unity および MRTK のバージョンと完全には互換性がないため、新しい XR プラグイン システムではなく、Unity の組み込みのレガシ XR を使用しています。 組み込みの XR は非推奨であることを示す警告が表示されても、無視してかまいません。

## Unity内シーン設定

* 場合によっては、Cube の色が茶色くくすんだ色で出るのでライティングの Auto Generate をオンにしてキレイに出す
  * 参考 [Unity \- デフォルトのキューブの色が黒くなってしまう｜teratail](https://teratail.com/questions/248387)

## 共同で進めやすいような設定

* 空の Game Object に同名のメインクラス（MainWorkSample.cs）を Add Compornent してそれを起点で追えるようにしてる
* ほかから呼ぶ場合、MainWorkSample で GameObject.Find で呼び出せるようにしている
* 良く使うクラスをインストールしてある
  * SettingFileManager
    * 設定ファイルを管理するもの
      * ビルドせず設定を変えれる
  * LogMessageText
    * Debug.Log や Debug.LogFormat で出すログを自動で出すウィンドウ
      * これがないと HoloLens 2 内で何が起きているか分からないので大事
* SettingFileManager の設定完了を OnComplete イベントでメインクラスに戻れるようにしている
  * メインクラスを追うと処理の流れが分かるようにしている
  * データロード系や重要クラス同士のやりとりはイベントでやりとりすると疎構造になりテストしやすそうな予感
* バージョンを管理して「あれこれ最新かな」を避ける試み
  * MainWorkSample.UnityAppVersion で、Unityアプリのバージョン
  * setting.json に json_data_version があって、JSONデータ構造のバージョン
  * どちらも手動なので更新忘れない

## 出来たら入れたい

* Debug.developerConsoleVisible = false; を追加して、エラーログを抑制したほうが良さそう
  * 参考 https://www.hiromukato.com/entry/2020/07/02/175932
* 診断バー（Diagnostics）を消す
  * 参考 https://www.fast-system.jp/unity-mrtk-v2-diagnostics-hidden/

## TIPS

* MiniJSON の型指定の癖を考慮するに、整数 int と小数点 float がありうるデータを管理した型判定が結構大変なので、管理上は文字列で数字を管理したほうがいい
