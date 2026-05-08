# CHROMAVOID - Code Rules

## Summary
Bu dosya CHROMAVOID için C# ve Unity çalışma kurallarını belirler. Amaç, jam hızını kaybetmeden AI assistant'ların aynı mimari dili sürdürmesini ve projeyi büyütürken sistemleri birbirine düğümlememesini sağlamaktır.

## Naming Conventions
| Öğe | Kural | Örnek |
| --- | --- | --- |
| Class | PascalCase | `WaveManager` |
| Method | PascalCase | `StartWave()` |
| Public property | PascalCase | `CurrentWaveIndex` |
| Private field | `_camelCase` | `_currentHealth` |
| Serialized private field | `[SerializeField] private` + `_camelCase` | `[SerializeField] private float _moveSpeed;` |
| Constant | PascalCase veya ALL_CAPS değil, C# stilinde | `MaxActiveEnemies` |
| Event | `On` prefix | `OnEnemyKilled` |
| ScriptableObject asset | Type + purpose | `WaveConfig_EarlyGame` |

## Folder Conventions
Yeni CHROMAVOID kodları Starter Assets içine yazılmamalıdır. Aktif üretim kökü:

`Assets/_Project/`

Alt klasör önerisi:

- `Scripts/Core`
- `Scripts/Grid`
- `Scripts/Enemies`
- `Scripts/Weapons`
- `Scripts/Waves`
- `Scripts/UI`
- `Scripts/Audio`
- `ScriptableObjects`
- `Prefabs`
- `Materials`
- `Shaders`
- `Scenes`
- `VFX`

## Script Responsibility Rules
- Bir script tek ana sorumluluk taşımalıdır.
- `Tile` sadece kendi state'ini ve local feedback'ini yönetmelidir.
- `GridManager` tile seçimi değil, tile kayıt/query sorumluluğu taşımalıdır.
- `TileCorruptionController` corruption algoritmasını yönetmelidir.
- `WaveManager` enemy instantiate etmemeli; spawn niyetini `SpawnManager`'a bırakmalıdır.
- `ScoreManager` enemy referanslarını dolaşmamalı; kill event'lerini dinlemelidir.

## SOLID Yaklaşımı
Jam projesinde aşırı soyutlama yapılmayacaktır, fakat şu prensipler korunacaktır:

- Single Responsibility: manager'lar büyüyüp her şeyi yapmayacak.
- Open/Closed: enemy ve wave config'leri yeni data ile genişleyebilmeli.
- Dependency Inversion: yüksek seviye sistemler prefab internals'a doğrudan bağımlı olmamalı.
- Interface kullanımı sadece gerçek esneklik sağladığında eklenmeli.

## Event Usage Kuralları
- Cross-system iletişimde event tercih edilir.
- Event payload küçük ve net olmalıdır.
- Event unsubscribe unutulmamalıdır.
- Unity lifecycle içinde `OnEnable` subscribe, `OnDisable` unsubscribe pattern'i kullanılmalıdır.
- Event zincirleri debug edilemez hale gelirse merkezi event hub yerine doğrudan inspector referansları değerlendirilebilir.

## Inspector Exposure Kuralları
- Tuning değerleri `[SerializeField] private` olmalıdır.
- Public field kullanılmamalıdır.
- Inspector'da anlamlı header/tooltip sadece karmaşık ayarlarda eklenmelidir.
- Runtime state debug için `[SerializeField] private` kullanılabilir, fakat production logic buna bağlı kalmamalıdır.

## Avoid Hardcoding Rules
Hardcode edilebilir:

- Jam prototipinde geçici debug değerleri.
- Çok kısa ömürlü test sabitleri.

Hardcode edilmemelidir:

- Wave enemy sayıları.
- Weapon damage/fire rate.
- Tile state süreleri.
- Score katsayıları.
- Layer mask isimleri.

Bu değerler config veya serialized field olarak taşınmalıdır.

## Debug Logging Rules
- Kalıcı gameplay flow içinde spam `Debug.Log` bırakılmamalıdır.
- Önemli state geçişleri için geçici log kullanılabilir.
- Log mesajları sistem prefix'i taşımalıdır: `[Wave]`, `[Tile]`, `[Spawn]`.
- Warning ve error logları gerçek aksiyon gerektiren durumlar için ayrılmalıdır.

## Null Safety Rules
- Serialized dependency'ler `Awake` veya `OnValidate` içinde kontrol edilmelidir.
- Optional dependency ise isim ve tooltip ile açık belirtilmelidir.
- Runtime `NullReferenceException` yerine erken ve anlamlı error log tercih edilir.
- Scene referansları mümkünse bootstrap veya manager üzerinden atanmalıdır.

## Script Size Limits
- 250 satır üstüne çıkan scriptler gözden geçirilmelidir.
- 400 satır üstü jam dışında refactor adayıdır.
- Büyük switch/if blokları config veya state sınıfına taşınmalıdır.

## Region / Comment Policy
- `#region` sadece Unity event methods / public API / private helpers gibi çok net ayrımlarda kullanılabilir.
- Her method'a yorum yazılmaz.
- Karmaşık algoritmalarda kısa niyet yorumu eklenir.
- Yorumlar kodun ne yaptığını değil, neden öyle yaptığını anlatmalıdır.

## ScriptableObject Usage Rules
- Tuning verisi ScriptableObject'e taşınmalıdır.
- Runtime mutable state ScriptableObject içinde tutulmamalıdır.
- Config asset isimleri açık olmalıdır.
- SO referansları prefab ve manager inspector'larından atanmalıdır.

## Prefab Dependency Kuralları
- Prefablar mümkün olduğunca kendi local dependency'lerini taşımalıdır.
- Manager prefab içindeki child path'e string ile erişmemelidir.
- Enemy prefabı health, movement ve visual child referanslarını kendi component'lerinde tutmalıdır.
- Player prefabı weapon mount/camera reference gibi kritik noktaları açık serialized field ile belirtmelidir.

## Jam Speed vs Clean Architecture
Jam için hedef, "çalışan ama anlaşılır" sistemdir. Aşırı soyutlama, generic framework ve erken optimizasyondan kaçınılır. Fakat şu üç şeyden ödün verilmemelidir:

1. Gameplay değerleri kod içine gömülmemeli.
2. Sistemler birbirini rastgele `Find` ile aramamalı.
3. AI assistant'ların devam ettirebilmesi için dosya/sınıf sorumlulukları net kalmalı.
