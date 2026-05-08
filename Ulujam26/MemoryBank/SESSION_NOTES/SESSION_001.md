# CHROMAVOID - Session 001

## Summary
Bu ilk session, CHROMAVOID için MemoryBank sistemini başlatır ve proje henüz gameplay implementasyonuna geçmeden önce temel design/teknik kararları kayıt altına alır. Mevcut repo Unity `6000.3.11f1`, URP `17.3.0`, Input System `1.19.0` ve Starter Assets Third Person temeline sahiptir.

## Initial Architecture Decisions
- CHROMAVOID single-player arena survival shooter olarak ele alınacak.
- "Kayıp" teması tile/zemin kaybı, renk kaybı ve kontrol kaybı üzerinden oynanacak.
- Starter Assets Third Person Controller hızlı hareket/input temeli olarak korunacak.
- İlk weapon sistemi raycast tabanlı olacak.
- Tile system state-driven olacak: Healthy, Warning, Corrupted, Black.
- Wave, enemy, tile ve score sistemleri event-driven iletişim kuracak.
- URP + Shader Graph görsel üretim için ana yol olacak.

## Current Implementation Goals
İlk milestone hedefi çalışan core loop:

1. CHROMAVOID production klasör yapısını oluştur.
2. Ana arena scene'i hazırla.
3. Player hareketini ve kamera yönünü oynanabilir hale getir.
4. Basit tile grid oluştur.
5. Tile state transition ve corruption timer ekle.
6. Raycast weapon ile fanus enemy öldür.
7. Wave spawn ve score sistemi bağla.
8. Game over ve minimal HUD ekle.

## Risks
| Risk | Etki | Azaltma |
| --- | --- | --- |
| FPS adaptasyonu gereğinden uzun sürebilir | Core loop gecikir | İlk sürümde Third Person hareketini koru |
| Shader polish gameplay'i geciktirebilir | Jam build eksik kalır | Önce material swap, sonra Shader Graph polish |
| Tile sistemi fazla karmaşıklaşabilir | Debug zorlaşır | İlk state flow basit enum/timer olsun |
| Enemy AI kapsamı büyüyebilir | Spawn/wave gecikir | İlk enemy doğrudan player chase yapsın |
| Event sistemi aşırı soyutlanabilir | Hız düşer | Basit C# event/action ile başla |

## Assumptions
- Oyun adı CHROMAVOID olarak kalacak.
- Jam teması "Kayıp".
- Hedef platform ilk aşamada PC/Windows.
- Multiplayer ve save/load jam scope dışında.
- Object pooling ilk prototipte şart değil ama mimari buna açık tutulacak.
- Mevcut shader denemeleri production görsel yön için referans olarak kullanılacak.

## First Milestone
Milestone adı: **Playable Void Loop**

Başarı kriterleri:

- Oyuncu arenada hareket edebilir.
- Tile'lar warning/corrupted/black state'e geçer.
- Enemy spawn olur ve oyuncuya yaklaşır.
- Oyuncu raycast weapon ile enemy öldürür.
- Skor artar.
- Wave ilerler.
- Oyuncu ölür veya güvenli tile eşiği bitince game over olur.

## Blockers
- CHROMAVOID'a özel production klasörleri henüz yok.
- Ana gameplay scene'i henüz yok.
- Fanus enemy visual/prefab yok.
- Tile visual shader/material production hali yok.
- Game UI yok.

## Next Session Priorities
1. `Assets/CHROMAVOID/` klasör yapısını kur.
2. `CHROMAVOID_Arena_Main.unity` scene'ini oluştur.
3. Tile prefab + GridManager ilk pass implementasyonu.
4. Player prefab adaptasyon stratejisini netleştir.
5. Raycast weapon prototipini ekle.
6. Basic fanus enemy placeholder prefabını bağla.

