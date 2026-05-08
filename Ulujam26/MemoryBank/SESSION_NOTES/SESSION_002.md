# CHROMAVOID - Session 002

## Summary
Bu session'da CHROMAVOID için ilk çalıştırılabilir gameplay prototype mimarisi eklendi. Starter Assets korunarak, oyuna özel sistemler `Assets/_Project/` altında modüler Unity component'leri olarak oluşturuldu.

## Implemented Systems
- `GameEvents`: Tile, enemy, wave, score, black tile ve game over event hook'ları.
- `GridManager`: Sahnedeki fiziksel tile objelerini kaydeder, world/grid sorgusu yapar, colored tile listesi verir.
- `Tile`, `TileState`, `TileVisualController`: Colored/Fading/Black state flow, countdown, restore ve Shader Graph property hook'ları.
- `EnemyContainer`, `EnemyType`, `EnemyDefinition`: Spot, Spreader, Chain fanus konsepti, havadan soft landing, tile threat ve kill/expire lifecycle.
- `SpawnManager`: Oyuncuya çok yakın/uzak olmayan colored tile seçimi, komşu fanus engeli, Inspector ağırlıkları ve wave scaling.
- `WaveManager`: Sonsuz wave loop, spawn hedefi ve wave complete eventleri.
- `ScoreManager`: Kill, rescued tile, survival ve wave skorları.
- `SimpleRaycastWeapon`: Crosshair merkezinden raycast, fanus hit/kill.
- `FPSCameraAdapter`: Starter Assets'i bozmadan bağımsız FPS look adapter.
- `PlayerTileDetector`: Black tile giriş/çıkış eventleri.
- `PrototypeDebugHUD`: Geçici UI text debug göstergesi.

## Current Implementation Goals
1. Unity sahnesinde tile prefablarını ve manager objelerini bağla.
2. Spot, Spreader ve Chain `EnemyDefinition` assetlerini oluştur.
3. Placeholder fanus prefabına `EnemyContainer` ve collider ekle.
4. Tile material/tint ayarları için `TileEffectData` asseti oluştur.
5. Play Mode'da spawn, countdown, kill restore ve game over akışını test et.

## Risks
| Risk | Etki | Azaltma |
| --- | --- | --- |
| Scene wiring yapılmadan sistemler çalışmaz | İlk test gecikir | `Assets/_Project/SETUP_NOTES.md` takip edilmeli |
| Tile collider/layer ayarları eksik kalabilir | PlayerTileDetector tile bulamaz | GridManager logical lookup ve raycast probe birlikte kullanıyor |
| Shader Graph property isimleri uyuşmayabilir | Visual feedback beklenen gibi görünmez | `TileEffectData` üzerinden property isimleri Inspector'dan değiştirilebilir |
| Spawn candidate kalmayabilir | Wave takılabilir | SpawnManager failed attempt limitiyle wave spawn budget'ını kapatır |

## Assumptions
- Proje New Input System ile çalışıyor.
- Tile grid procedural üretilmeyecek; sahnede fiziksel tile objeleri olacak.
- İlk fanus prefabı placeholder olabilir.
- FPS adaptasyonu, Starter Assets controller kodunu değiştirmeden camera/weapon katmanında yapılacak.

## Next Session Priorities
1. Unity içinde `_Project` prefab/material/config assetlerini oluştur.
2. Ana arena scene wiring yap.
3. Play Mode compile ve runtime hatalarını düzelt.
4. Tile visual shader/material bağlantısını üretim görseline yaklaştır.
5. Fanus prefabını cam/transparan görsele taşı.

