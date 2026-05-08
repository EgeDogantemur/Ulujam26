# CHROMAVOID - TODO Roadmap

## Summary
Bu roadmap jam build'ini önce çalışır core loop'a, sonra okunabilir/polished bir deneyime taşımak için katmanlandırılmıştır. Her task kısa açıklama, öncelik, dependency ve estimated complexity içerir.

## CRITICAL FOR JAM
| Task | Açıklama | Priority | Dependency | Complexity |
| --- | --- | --- | --- | --- |
| CHROMAVOID klasör yapısı | `Assets/_Project/` altında üretim klasörlerini aç | Done | Yok | Low |
| Ana gameplay scene | SampleScene yerine CHROMAVOID arena scene'i oluştur | Critical | Klasör yapısı | Medium |
| Player controller adaptasyonu | Starter Assets player'ı arena shooter input/kamera düzenine hazırla | Critical | Starter Assets | Medium |
| Tile prefab ve grid | Basit kare tile prefabı ve grid manager kur | In Progress | Ana scene | Medium |
| Tile state flow | Colored/Fading/Black state geçişlerini çalıştır | In Progress | Tile prefab ve grid | Medium |
| Corruption timer | Fanus countdown ile tile fading/black akışı | In Progress | Tile state flow | Medium |
| Basic weapon | Camera forward raycast silah ve hit detection ekle | In Progress | Player controller | Medium |
| Basic fanus enemy | EnemyContainer ile fanus lifecycle ve tile threat | In Progress | Ana scene, weapon | Medium |
| Spawn manager | Colored tile üstüne fanus spawn et | In Progress | Basic enemy | Medium |
| Wave manager | Basit wave başlangıç/bitiş döngüsü | In Progress | Spawn manager | Medium |
| Score manager | Kill ve wave bonus skorlarını hesapla | In Progress | Enemy death event | Low |
| Game over | Black tile ratio eşiğiyle oyun bitir | In Progress | Tile/player systems | Medium |
| Minimal HUD | Score, wave, health/safe tile göstergesi | Critical | Score/game state | Medium |

## IMPORTANT
| Task | Açıklama | Priority | Dependency | Complexity |
| --- | --- | --- | --- | --- |
| ScriptableObject configs | Wave, enemy, weapon ve tile tuning assetleri oluştur | High | Core loop | Medium |
| Object pooling | Enemy ve VFX spawn maliyetini azalt | High | Spawn/enemy sistemi | Medium |
| Tile visual polish | Warning pulse, corrupted material, black tile görünümü | High | Tile state flow | Medium |
| Fanus shader/material | Cam/fresnel hissi veren enemy görseli | High | Enemy prefab | Medium |
| Audio feedback | Shoot, hit, kill, tile warning, game over sesleri | High | Core loop | Medium |
| Post-process tuning | Bloom, vignette, chromatic aberration ayarları | High | Visual materials | Low |
| Restart flow | Game over sonrası scene restart veya retry | High | Game over | Low |
| Build validation | İlk playable Windows build testi | High | Core loop | Medium |

## NICE TO HAVE
| Task | Açıklama | Priority | Dependency | Complexity |
| --- | --- | --- | --- | --- |
| Combo sistemi | Ardışık kill'lerde skor çarpanı | Medium | Score manager | Medium |
| Kill VFX | Fanus kırılma particle ve shard hissi | Medium | Enemy death | Medium |
| Corruption spread pattern | Tam random yerine cluster/edge spread algoritması | Medium | Corruption timer | Medium |
| Wave clear pulse | Wave sonunda kısa renk geri kazanım efekti | Medium | Wave manager | Low |
| Camera shake | Shoot/hit/death/game over feedback | Medium | Weapon/enemy | Low |
| Multiple enemy variants | Seeder veya shield fanus gibi varyasyonlar | Medium | Enemy config | High |
| Local high score | Session skorunu sakla | Low | Score manager | Low |

## POST JAM IDEAS
| Task | Açıklama | Priority | Dependency | Complexity |
| --- | --- | --- | --- | --- |
| Perk selection | Wave clear sonrası perk seçimi | Post Jam | Wave manager, configs | High |
| Weapon upgrade system | Damage, fire rate, pierce, area effect upgrade'leri | Post Jam | Weapon config | High |
| Multiple arenas | Farklı layout ve corruption pattern'leri | Post Jam | Grid system | High |
| Boss waves | Özel fanus boss ve arena manipulation | Post Jam | Enemy variants | High |
| Meta progression | Kalıcı unlock veya challenge sistemi | Post Jam | Save system | High |
| Advanced shader pass | Fullscreen void distortion ve shader polish | Post Jam | URP pipeline | Medium |
| Tutorial flow | Oyuncuya tile states ve scoring öğretme | Post Jam | Stable core loop | Medium |
