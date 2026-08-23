# EditorOnly Switch

Adds a toggle to every row in the Unity **Hierarchy** window that switches a
GameObject's tag between `EditorOnly` and `Untagged`.

Objects tagged `EditorOnly` are stripped from builds (together with their
children), which makes the tag handy for debug objects and editor-only gimmicks.
By default you can only change it one object at a time from the Inspector's Tag
dropdown, and there is no way to see which objects are `EditorOnly` at a glance.
This package solves both problems.

Unity の **Hierarchy** の各行に、GameObject のタグを `EditorOnly` と `Untagged`
で切り替えるトグルを追加します。`EditorOnly` タグの付いたオブジェクトは（子ごと）
ビルドから除外されるため、デバッグ用オブジェクトやエディタ専用ギミックの管理に便利
です。標準では Inspector の Tag ドロップダウンから 1 つずつしか変更できず、どれが
`EditorOnly` かを一覧で把握することもできません。このパッケージはその両方を解決します。

## Features / 機能

- A checkbox on the right edge of each Hierarchy row: click to switch
  `EditorOnly` &harr; `Untagged`.
- An eye icon next to it enables/disables the GameObject (open = active,
  closed = inactive), so the two controls are easy to tell apart.
- `EditorOnly` rows are tinted so their state is visible at a glance.
- Each control can be enabled/disabled independently, and its tint (EditorOnly
  only) and horizontal offset from the right edge are configurable in
  **Edit > Preferences > EditorOnly Switch**.
- Multi-select supported: toggling one selected object applies to all selected
  objects at once.
- Full Undo support (`Ctrl+Z` / `Cmd+Z`).

---

- Hierarchy の各行の右端にチェックボックスを表示。クリックで `EditorOnly` ⇔
  `Untagged` を切り替え。
- その隣の目玉アイコンで GameObject の有効/無効を切り替え（開=有効、閉=無効）。
  2 つのコントロールを見た目で区別できます。
- `EditorOnly` の行は着色され、状態を一目で確認できます。
- コントロールごとに使用の有無を切り替え可能。着色（EditorOnly のみ）と右端からの
  オフセットは **Edit > Preferences > EditorOnly Switch** で設定できます。
- 複数選択に対応。選択中の 1 つを切り替えると選択全体に適用されます。
- Undo に対応（`Ctrl+Z` / `Cmd+Z`）。

## Usage / 使い方

1. Open the Hierarchy window.
2. Each row shows two controls at its right edge:
   - The rightmost checkbox switches the tag: On &rarr; `EditorOnly` (row
     tinted), Off &rarr; `Untagged`.
   - The eye icon to its left enables/disables the GameObject (open = active,
     closed = inactive).
3. Select multiple objects and toggle one of them to apply the change to the
   entire selection.

Open **Edit > Preferences > EditorOnly Switch** to configure each control
separately:

- **EditorOnly (checkbox)**: Enable, Row Tint, Right Offset.
- **Active (eye icon)**: Enable, Right Offset.

Disabling a control hides it from the Hierarchy. **Right Offset** controls how
far each control sits from the row's right edge. Use **Reset to Default** to
restore everything.

---

1. Hierarchy ウィンドウを開きます。
2. 各行の右端に 2 つのコントロールが表示されます:
   - 右端のチェックボックスでタグを切り替え。ON → `EditorOnly`（行が着色）、
     OFF → `Untagged`。
   - その左の目玉アイコンで GameObject の有効/無効を切り替え（開=有効、閉=無効）。
3. 複数選択した状態でどれか 1 つを切り替えると、選択全体に適用されます。

**Edit > Preferences > EditorOnly Switch** で各コントロールを個別に設定できます:

- **EditorOnly (checkbox)**: 使用の有無 / 行の着色 / 右端オフセット
- **Active (eye icon)**: 使用の有無 / 右端オフセット

使用の有無を OFF にするとそのコントロールは Hierarchy から非表示になります。
**Right Offset** は右端からの距離、**Reset to Default** で全設定を初期化します。

## Notes / 注意

- `EditorOnly` strips the object **and all of its children** from builds.
- Off always resets the tag to `Untagged`; a previously assigned custom tag is
  not restored.

---

- `EditorOnly` はそのオブジェクト**と全ての子**をビルドから除外します。
- OFF は常にタグを `Untagged` に戻します。以前に設定していた任意のタグは復元されません。

## Requirements / 要件

- Unity 2020.3 or later. / Unity 2020.3 以降。

## License / ライセンス

MIT. See [LICENSE.md](LICENSE.md).
