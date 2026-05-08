# CHROMAVOID - Gameplay Systems

## Summary
Bu doküman CHROMAVOID'un planlanan gameplay sistemlerini hem design hem teknik akış olarak açıklar. Sistemler henüz implement edilmemiştir; bu dosya ilk production pass için karar rehberi olarak kullanılmalıdır.

## Tile System
Tile sistemi oyunun ana mekaniğidir.

Design:

- Oyuncunun güvenli hareket alanı tile'larla tanımlanır.
- Tile'lar kayboldukça baskı artar.
- Warning state oyuncuya adil tepki süresi verir.

Teknik:

- Her tile bir `Tile` component'i taşır.
- Tile state enum ile başlar: `Healthy`, `Warning`, `Corrupted`, `Black`.
- Tile state değişimi event yayınlar.
- Görsel feedback material parameter veya prefab child VFX ile tetiklenir.

## Enemy System
Enemy sistemi fanus konseptine dayanır.

Design:

- Enemy'ler oyuncunun hem health'ini hem arena güvenliğini tehdit eder.
- Fanus kırılması oyunun en tatmin edici anlarından biri olmalıdır.

Teknik:

- `EnemyHealth`, `EnemyMovement`, `EnemyDamageDealer` gibi ayrık component'ler önerilir.
- `EnemyConfig` damage, speed, health, score value taşır.
- Ölümde `OnEnemyKilled` event'i yayınlanır.

## Spawn System
Design:

- Spawn noktaları oyuncuyu çevreler ama haksız doğrudan üstüne spawn yapmaz.
- Wave ilerledikçe aralık azalır ve aktif enemy sayısı artar.

Teknik:

- Scene içinde `SpawnPoint` component'leri bulunur.
- `SpawnManager`, `WaveManager` tarafından verilen wave data ile enemy üretir.
- Pool'a hazır factory fonksiyonu kullanılmalıdır.

## Wave System
Design:

- Wave'ler oyuncuya ritim ve hedef verir.
- Her wave difficulty'yi belirgin ama kontrol edilebilir artırır.

Teknik:

- `WaveConfig` listesi ile başlamak yeterlidir.
- Wave bitiş koşulu: hedef enemy sayısı öldü ve aktif enemy kalmadı.
- Endless için wave index'e göre scaling multiplier hesaplanabilir.

## Weapon System
Design:

- Jam için tek ana silah: hızlı, net, raycast temelli.
- Silah, fanus kırma hissini taşımalı.

Teknik:

- `WeaponController` input'u dinler.
- `RaycastWeapon` camera forward üzerinden raycast atar.
- Layer mask ile enemy ve environment ayrılır.
- Hit event'i VFX/audio sistemine bilgi verir.

## Detection System
Design:

- Enemy oyuncuyu karmaşık stealth yerine doğrudan algılar.
- Arena survival için net chase davranışı yeterlidir.

Teknik:

- İlk sürümde distance check ve direct movement yeterlidir.
- NavMesh kullanılacaksa arena tile değişimleriyle uyumu ayrıca test edilmelidir.
- Tile tabanlı pathfinding jam için gereksiz risklidir.

## Difficulty Scaling
Difficulty üç eksende artmalıdır:

- Enemy sayısı ve spawn sıklığı.
- Tile corruption hızı.
- Warning süresinin kısalması.

Scaling değerleri kodda sabitlenmemeli, config veya curve ile ayarlanmalıdır.

## Corruption Spreading
Design:

- Corruption oyuncuya "arena benden alınıyor" hissi vermelidir.
- Tamamen rastgele seçim haksız hissettirebilir; pattern okunabilir olmalıdır.

Teknik:

- İlk sürümde random safe tile seçimi + oyuncuya yakın tile koruması.
- Daha sonra enemy kaynaklı spreading veya cluster growth eklenebilir.
- Corruption tick global timer ile çalışmalıdır.

## Black Tile Effects
Design:

- Black tile kaybedilmiş alan demektir.
- Oyuncu oraya basmanın ölümcül olduğunu hızlıca öğrenmelidir.

Teknik:

- Trigger veya collision üzerinden player contact algılanır.
- Etki seçenekleri: instant kill, high DPS, slow + damage.
- Jam için high DPS daha adil olabilir; game feel'e göre instant kill seçilebilir.

## Score System
Design:

- Skor, sadece hayatta kalmayı değil cesur oynamayı ödüllendirmelidir.

Teknik:

- `ScoreManager`, kill ve wave event'lerini dinler.
- Combo timer opsiyonel ama değerli bir polish alanıdır.
- UI score değişimini event ile alır.

## Future Perk System
Perk sistemi jam sonrası veya jam sonu polish olarak eklenebilir.

Akış:

1. Wave clear.
2. Oyuncuya 2-3 perk seçeneği.
3. Seçilen perk runtime modifier olarak uygulanır.

Perkler ScriptableObject olmalıdır. Runtime state player veya perk manager içinde tutulmalıdır.

## Future Weapon Upgrade System
Upgrade fikirleri:

- Fire rate artışı.
- Damage artışı.
- Pierce.
- Fanus kırılınca area damage.
- Corrupted tile üzerinde kill alınca cleanse pulse.

Upgrade sistemi perk sistemiyle aynı altyapıyı paylaşabilir.

