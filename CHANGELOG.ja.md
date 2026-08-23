# 変更履歴

このパッケージの主な変更点を記録します。

フォーマットは [Keep a Changelog](https://keepachangelog.com/ja/1.0.0/) に準拠し、
バージョニングは [セマンティック バージョニング](https://semver.org/lang/ja/) に従います。

An English version is available in [CHANGELOG.md](CHANGELOG.md).

## [0.1.0] - 2026-08-23

### 追加

- Hierarchy の各行に、GameObject のタグを `EditorOnly` と `Untagged` で
  切り替えるトグルを追加。
- GameObject の有効/無効（アクティブ状態）を切り替える目玉アイコンを追加。
  EditorOnly のチェックボックスと見た目で区別できる。
- `EditorOnly` の行を着色して表示。色は Edit > Preferences > EditorOnly Switch
  で変更可能。
- コントロールごと（EditorOnly / Active）の独立した設定。使用の有無と、行の右端
  からの横方向オフセットをそれぞれ設定できる。
- 複数選択に対応。選択中の 1 つを切り替えると選択全体に適用される。
- Undo に対応。
