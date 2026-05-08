# CHROMAVOID - File Structure

## Summary
Bu dosya mevcut Unity proje yapısını ve CHROMAVOID için önerilen production klasör düzenini açıklar. Amaç, yeni AI coding session'larında dosyaların nereye eklenmesi gerektiğini tartışmasız hale getirmektir.

## Mevcut Kök Yapı
| Path | Amaç / Durum |
| --- | --- |
| `Assets/` | Unity asset kökü |
| `Packages/` | Unity package manifest ve lock dosyaları |
| `ProjectSettings/` | Unity proje ayarları |
| `Library/` | Unity cache, versiyona alınmamalı |
| `Temp/` | Unity geçici dosyaları, versiyona alınmamalı |
| `Logs/` | Editor logları, versiyona alınmamalı |
| `UserSettings/` | Kullanıcı/editor ayarları |
| `MemoryBank/` | AI context ve proje dokümantasyonu |

## Mevcut Assets Yapısı
| Path | Amaç / Durum |
| --- | --- |
| `Assets/Scenes/` | `SampleScene.unity` mevcut |
| `Assets/Settings/` | URP renderer, pipeline ve volume profile assetleri |
| `Assets/StarterAssets/` | Unity Starter Assets Third Person, Mobile input ve environment paketleri |
| `Assets/TutorialInfo/` | Unity tutorial/readme assetleri |
| `Assets/_AshAndPause/shadermat/` | Shader Graph ve material denemeleri |
| `Assets/InputSystem_Actions.inputactions` | Input actions asset |

## Mevcut CHROMAVOID Production Yapısı
Yeni oyun dosyaları kullanıcı isteği doğrultusunda şu kök altında toplanmıştır:

`Assets/_Project/`

Önerilen klasörler:

| Path | Amaç |
| --- | --- |
| `Assets/_Project/Scenes/` | Ana gameplay, test ve debug scene'leri |
| `Assets/_Project/Scripts/Core/` | GameManager, GameEvents, ScoreManager |
| `Assets/_Project/Scripts/Grid/` | GridManager |
| `Assets/_Project/Scripts/Tiles/` | Tile, TileState, TileVisualController |
| `Assets/_Project/Scripts/Enemies/` | EnemyContainer, EnemyType |
| `Assets/_Project/Scripts/Spawning/` | SpawnManager |
| `Assets/_Project/Scripts/Waves/` | WaveManager |
| `Assets/_Project/Scripts/Player/` | FPSCameraAdapter, PlayerTileDetector |
| `Assets/_Project/Scripts/Weapons/` | SimpleRaycastWeapon |
| `Assets/_Project/Scripts/UI/` | PrototypeDebugHUD |
| `Assets/_Project/Scripts/ScriptableObjects/` | GridSettings, TileEffectData, EnemyDefinition |
| `Assets/_Project/ScriptableObjects/` | Config asset instance'ları |
| `Assets/_Project/Prefabs/` | Player, enemy, tile ve weapon prefabları |
| `Assets/_Project/Materials/` | Production/placeholder material assetleri |

## Script Yerleşim Kuralları
- Starter Assets scriptleri doğrudan değiştirilmemeli; adapter scriptleri `Assets/_Project/Scripts/Player/` altında tutulmalıdır.
- Gameplay manager'lar `Scripts/Core` veya ilgili subsystem klasörüne konulmalı.
- Tile ile ilgili her script `Scripts/Grid` altında olmalı.
- Enemy varyasyonları `Scripts/Enemies` altında tutulmalı.
- UI scriptleri gameplay sistemlerinden event ile veri almalı.

## Prefab Organizasyonu
Önerilen prefab alt klasörleri:

- `Prefabs/Player`
- `Prefabs/Enemies`
- `Prefabs/Tiles`
- `Prefabs/Managers`
- `Prefabs/UI`
- `Prefabs/VFX`

Prefab naming örnekleri:

- `Player_CHROMAVOID.prefab`
- `Enemy_Fanus_Basic.prefab`
- `Tile_Arena_01.prefab`
- `Manager_Gameplay.prefab`
- `UI_HUD.prefab`
- `VFX_FanusBreak.prefab`

## ScriptableObject Naming Examples
- `WaveConfig_001.asset`
- `WaveConfig_EndlessBase.asset`
- `EnemyConfig_FanusBasic.asset`
- `WeaponConfig_ChromaRay.asset`
- `TileStateConfig_Default.asset`
- `ScoreConfig_Default.asset`

## Scene Naming Examples
- `CHROMAVOID_Arena_Main.unity`
- `CHROMAVOID_Arena_TestGrid.unity`
- `CHROMAVOID_WeaponTest.unity`
- `CHROMAVOID_ShaderPreview.unity`

## Asset Migration Notes
- `_AshAndPause/shadermat` altındaki shader/material dosyaları silinmemeli; production shader hazırlanırken referans olarak incelenmelidir.
- Production seviyesine gelen shader ve materyaller `_Project` klasörlerine kopyalanmalı veya taşınmalıdır.
- Starter Assets klasörü vendor/third-party gibi düşünülmeli; oyuna özel dosyalar oraya eklenmemelidir.
