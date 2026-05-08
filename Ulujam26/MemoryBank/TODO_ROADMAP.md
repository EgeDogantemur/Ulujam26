# CHROMAVOID - TODO Roadmap

## Summary
Bu roadmap jam build'ini önce çalışır core loop'a, sonra okunabilir/polished bir deneyime taşımak için katmanlandırılmıştır. Her task kısa açıklama, öncelik, dependency ve estimated complexity içerir.

## CRITICAL FOR JAM
| Task | Açıklama | Priority | Dependency | Complexity |
| --- | --- | --- | --- | --- |
| CHROMAVOID klasör yapısı | `Assets/CHROMAVOID/` altında üretim klasörlerini aç | Critical | Yok | Low |
| Ana gameplay scene | SampleScene yerine CHROMAVOID arena scene'i oluştur | Critical | Klasör yapısı | Medium |
| Player controller adaptasyonu | Starter Assets player'ı arena shooter input/kamera düzenine hazırla | Critical | Starter Assets | Medium |
| Tile prefab ve grid | Basit kare tile prefabı ve grid manager kur | Critical | Ana scene | Medium |
| Tile state flow | Healthy/Warning/Corrupted/Black state geçişlerini çalıştır | Critical | Tile prefab ve grid | Medium |
| Corruption timer | Belirli aralıklarla tile bozan controller ekle | Critical | Tile state flow | Medium |
| Basic weapon | Camera forward raycast silah ve hit detection ekle | Critical | Player controller | Medium |
| Basic fanus enemy | Health, movement, damage ve death event içeren düşman | Critical | Ana scene, weapon | Medium |
| Spawn manager | Spawn point'lerden enemy üret | Critical | Basic enemy | Medium |
| Wave manager | Basit wave başlangıç/bitiş döngüsü | Critical | Spawn manager | Medium |
| Score manager | Kill ve wave bonus skorlarını hesapla | Critical | Enemy death event | Low |
| Game over | Health veya safe tile eşiğiyle oyun bitir | Critical | Tile/player systems | Medium |
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

