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

## Önerilen CHROMAVOID Production Yapısı
Yeni oyun dosyaları şu kök altında toplanmalıdır:

`Assets/CHROMAVOID/`

Önerilen klasörler:

| Path | Amaç |
| --- | --- |
| `Assets/CHROMAVOID/Scenes/` | Ana gameplay, test ve debug scene'leri |
| `Assets/CHROMAVOID/Scripts/Core/` | GameManager, events, bootstrap |
| `Assets/CHROMAVOID/Scripts/Grid/` | Tile, GridManager, corruption sistemi |
| `Assets/CHROMAVOID/Scripts/Enemies/` | Enemy health, movement, damage, config binding |
| `Assets/CHROMAVOID/Scripts/Weapons/` | Raycast weapon, weapon controller, hit handling |
| `Assets/CHROMAVOID/Scripts/Waves/` | WaveManager, SpawnManager, SpawnPoint |
| `Assets/CHROMAVOID/Scripts/UI/` | HUD, score display, game over panel |
| `Assets/CHROMAVOID/Scripts/Audio/` | Audio trigger ve one-shot helpers |
| `Assets/CHROMAVOID/ScriptableObjects/` | Wave, enemy, weapon, tile config assetleri |
| `Assets/CHROMAVOID/Prefabs/` | Player, enemy, tile, manager ve UI prefabları |
| `Assets/CHROMAVOID/Materials/` | Production material assetleri |
| `Assets/CHROMAVOID/Shaders/` | Production Shader Graph assetleri |
| `Assets/CHROMAVOID/VFX/` | Particle system ve VFX prefabları |
| `Assets/CHROMAVOID/Audio/` | SFX ve ambience |

## Script Yerleşim Kuralları
- Starter Assets scriptleri doğrudan değiştirilmemeli; wrapper/adaptor gerekiyorsa `Assets/CHROMAVOID/Scripts/Player/` açılabilir.
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
- Production seviyesine gelen shader ve materyaller CHROMAVOID klasörlerine kopyalanmalı veya taşınmalıdır.
- Starter Assets klasörü vendor/third-party gibi düşünülmeli; oyuna özel dosyalar oraya eklenmemelidir.

