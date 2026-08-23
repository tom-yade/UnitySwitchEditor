# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

日本語版は [CHANGELOG.ja.md](CHANGELOG.ja.md) を参照してください。

## [0.1.0] - 2026-08-23

### Added

- Hierarchy row toggle to switch a GameObject's tag between `EditorOnly` and
  `Untagged`.
- Hierarchy row eye icon to enable/disable a GameObject (active state),
  visually distinct from the EditorOnly checkbox.
- Tinted background for `EditorOnly` rows, with a configurable color in
  Edit > Preferences > EditorOnly Switch.
- Independent settings per control (EditorOnly / Active): an enable flag and a
  horizontal offset from the row's right edge for each.
- Multi-select support: toggling a selected object applies to the whole
  selection.
- Undo support.
