# CHROMAVOID - Technical Architecture

## Summary
CHROMAVOID şu anda Unity `6000.3.11f1` projesi olarak kuruludur. URP `17.3.0`, Input System `1.19.0`, Starter Assets Third Person paketi, SampleScene ve Shader Graph/material denemeleri mevcuttur. Gameplay sistemleri henüz implement edilmemiştir; bu doküman önerilen mimariyi pre-implementation snapshot olarak kaydeder.

## Mevcut Teknik Durum
| Alan | Durum |
| --- | --- |
| Unity | `6000.3.11f1` |
| Render Pipeline | URP `17.3.0` |
| Input | Input System `1.19.0` |
| Starter Assets | ThirdPersonController ve Mobile input paketleri mevcut |
| Scene | `Assets/Scenes/SampleScene.unity` mevcut |
| Shader denemeleri | `Assets/_AshAndPause/shadermat/` altında Shader Graph ve materyaller mevcut |
| Gameplay kodu | Henüz yok |

## URP Kullanımı
URP, CHROMAVOID için doğru temel olarak kabul edilir:

- Shader Graph ile tile fade, void corruption ve fanus glass shader üretilebilir.
- Post-process ile chromatic aberration, bloom, vignette ve color grading kullanılabilir.
- Jam için performans/kalite dengesi Built-in'e göre daha kontrollüdür.

URP ayarları `Assets/Settings/` altında PC/Mobile renderer asset'leriyle kuruludur. Jam hedefi PC build ise öncelik PC renderer üzerinde olmalıdır.

## Starter Assets Yapısı
Projede Unity Starter Assets Third Person Controller bulunmaktadır:

- `Assets/StarterAssets/ThirdPersonController/Scripts/ThirdPersonController.cs`
- `Assets/StarterAssets/InputSystem/StarterAssetsInputs.cs`
- Starter prefabları ve Playground scene'i.

İlk aşamada bu paket doğrudan silinmemelidir. Hareket, kamera, input ve player prefab üretimi için hızlı başlangıç sağlar.

## FPS Adaptasyonu Yaklaşımı
Hedef FPS hissiyse iki aşamalı yaklaşım önerilir:

1. Jam için Third Person Controller hareketini koruyup kamera ve weapon raycast'i oyuncunun bakış yönüne bağlamak.
2. Zaman kalırsa kamera pivotunu FPS'e yaklaştırmak, mesh visibility ve crosshair davranışını ayarlamak.

Tam custom FPS controller yazmak jam için risklidir. Starter Assets üzerinden kontrollü adaptasyon tercih edilir.

## Event-Driven Architecture Yaklaşımı
Gameplay sistemleri manager'lar arasında doğrudan referans zinciri kurmamalıdır. Ana sistem event'leri:

- `OnTileStateChanged`
- `OnEnemySpawned`
- `OnEnemyKilled`
- `OnWaveStarted`
- `OnWaveCompleted`
- `OnPlayerDamaged`
- `OnScoreChanged`
- `OnGameOver`

Jam için C# event/action yeterlidir. Daha sonra ScriptableObject event channel yaklaşımına geçilebilir.

## Grid Sistemi Mantığı
Grid sistemi scene başlangıcında tile referanslarını toplar veya procedural olarak üretir.

Önerilen temel:

- `GridManager`: tile kayıtları, grid coordinate lookup, safe tile sayımı.
- `Tile`: state, material/VFX hook, damage/collision davranışı.
- `TileState`: enum olarak başlar, büyürse ScriptableObject config'e taşınır.
- `TileCorruptionController`: corruption seçim algoritması ve zamanlaması.

Grid, world position ile logical coordinate arasında net dönüşüm sağlamalıdır.

## Tile State Flow
Temel flow:

1. `TileCorruptionController` hedef tile seçer.
2. Tile `Healthy -> Warning` geçişi yapar.
3. Warning süresi bitince `Corrupted` olur.
4. Corrupted süre veya wave katsayısıyla `Black` olur.
5. Her geçiş event yayınlar.
6. Visual layer material parameter veya prefab state aracılığıyla güncellenir.

State geçişleri tile içinde validasyon yapmalıdır; manager sadece niyet bildirir.

## Enemy Lifecycle
Enemy lifecycle:

1. Pool veya instantiate ile spawn.
2. Config atanır.
3. Spawn VFX ve kısa activation delay.
4. Player tracking başlar.
5. Damage alır.
6. Death event yayınlar.
7. Score, wave ve pool sistemleri event'e tepki verir.
8. Enemy deactivate edilip pool'a döner.

Enemy ölümünde doğrudan score manager çağırmak yerine event tercih edilir.

## Spawn Pipeline
Spawn sistemi şu bileşenlerden oluşmalıdır:

- `SpawnPoint`: scene'de konum ve opsiyonel tag/weight taşır.
- `SpawnManager`: aktif wave'e göre spawn tick çalıştırır.
- `WaveConfig`: enemy prefab/config, count, interval ve difficulty değerleri.
- `EnemyFactory` veya `EnemyPool`: instantiate/pool sorumluluğu.

Spawn noktaları oyuncuya yakınlık, aktif enemy sayısı ve wave state'e göre filtrelenmelidir.

## Shader Graph Entegrasyon Planı
Mevcut shader denemeleri `Assets/_AshAndPause/shadermat/` altında tutuluyor. Jam üretimi için shader asset'leri daha sonra CHROMAVOID klasör yapısına taşınmalıdır.

Hedef shader'lar:

- Tile Healthy/Warning/Corrupted/Black material varyasyonları.
- Fanus glass shader: transparent, fresnel, crack mask, emission edge.
- Fullscreen void effect: game over veya corruption yüksekken artan post-process.

Shader parameter isimleri script'ten kontrol edilecekse constant olarak tutulmalıdır.

## ScriptableObject Kullanım Amacı
ScriptableObject'ler tuning verisini koddan ayırmak için kullanılmalıdır:

- `WaveConfig`
- `EnemyConfig`
- `WeaponConfig`
- `TileStateConfig`
- `DifficultyConfig`
- `ScoreConfig`

Jam hızında her şeyi ScriptableObject yapmak şart değildir; fakat wave/enemy/weapon gibi sık ayarlanacak değerler için erken kurulması yararlıdır.

## Manager Sistemleri
Önerilen manager seti:

| Manager | Sorumluluk |
| --- | --- |
| GameManager | Game state, start/end/restart |
| GridManager | Tile registry ve query |
| TileCorruptionController | Corruption timing ve tile seçimi |
| WaveManager | Wave progression |
| SpawnManager | Enemy spawn orchestration |
| ScoreManager | Score ve combo |
| UIManager | HUD ve game over görünümü |
| AudioManager | One-shot ve ambience |

Manager'lar mümkün olduğunca scene-level olmalı, prefab içi gizli singleton bağımlılığı azaltılmalıdır.

## Future Scalability Notes
- Event channel yaklaşımına geçilebilir.
- Tile corruption algoritması strategy pattern ile değiştirilebilir.
- Enemy behavior için state machine veya behavior tree eklenebilir.
- Wave config listeleri endless scaling fonksiyonlarıyla genişletilebilir.
- Local high score için küçük JSON save sistemi post-jam eklenebilir.

## Performance Considerations
- Enemy spawn/despawn için object pooling planlanmalı.
- Tile material değişimleri için shared material mutasyonundan kaçınılmalı; MaterialPropertyBlock tercih edilmeli.
- Sürekli `FindObjectOfType` kullanılmamalı.
- Tile state update'leri her frame değil, event/timer bazlı olmalı.
- Physics raycast layer mask ile sınırlandırılmalı.
- Post-process efektleri düşük/orta ayar presetleriyle test edilmeli.

## Object Pooling Planı
Jam ilk implementasyonda instantiate ile başlanabilir, fakat mimari pool'a geçişi engellememelidir.

Pool adayları:

- Enemy prefabları.
- Hit impact VFX.
- Enemy death VFX.
- Floating score text.
- Projectile varsa projectile prefabları.

## Save / Load
Jam scope için save/load gerekli değildir. Oyun session bazlı skor denemesi olarak ele alınır. Post-jam için local high score saklama eklenebilir.

## Multiplayer
Multiplayer düşünülmemektedir. Projede `com.unity.multiplayer.center` paketi mevcut olsa da CHROMAVOID jam scope single-player olmalıdır. Multiplayer, tile state senkronizasyonu ve spawn determinism nedeniyle bu tasarım için yüksek risklidir.

