# CHROMAVOID Prototype Setup Notes

## Summary
Bu not, yeni eklenen başlangıç mimarisini Unity sahnesine hızlı bağlamak içindir. Kodlar Starter Assets'i silmez veya değiştirmez; `_Project` altında bağımsız prototype sistemleri olarak durur.

## Minimum Scene Wiring
1. Arena zeminini fiziksel tile objelerine böl.
2. Her tile objesine `Tile`, `TileVisualController`, collider ve renderer ekle.
3. Sahneye `GridManager`, `GameManager`, `ScoreManager`, `SpawnManager`, `WaveManager` ekle.
4. Player objesine `PlayerTileDetector`, camera/pivot objesine `FPSCameraAdapter`, weapon/camera objesine `SimpleRaycastWeapon` ekle.
5. `EnemyDefinition` assetleri oluştur: Spot, Spreader, Chain.
6. `SpawnManager` içine player transform, grid manager, enemy prefab ve enemy weight listesi bağla.
7. Fanus prefabına `EnemyContainer` ve collider ekle.
8. Debug HUD istenirse Canvas altındaki Text objesine `PrototypeDebugHUD` bağla.

## Placeholder Visuals
- Tile state görseli için önce material swap veya tint yeterlidir.
- Shader Graph hazır olduğunda `TileEffectData` içindeki property isimleri shader propertyleriyle eşleştir.
- Fanus için geçici sphere prefab kullanılabilir; prefab verilmezse `SpawnManager` runtime placeholder sphere üretir.

## Debug Hooks
- `GridManager`: tile register gizmos.
- `SpawnManager`: spawn radius gizmos ve `Debug/Spawn One`.
- `Tile`: state değiştiren context menu komutları.
- `EnemyContainer`: `Debug/Kill Enemy`.
- `GameManager`: `Debug/Force Game Over`.

