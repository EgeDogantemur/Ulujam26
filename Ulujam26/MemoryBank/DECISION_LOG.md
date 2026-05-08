# CHROMAVOID - Decision Log

## Summary
Bu dosya teknik ve design kararlarını tarih bazlı kaydeder. Her yeni büyük kararda `Decision`, `Reason`, `Tradeoff` ve `Future reconsideration` alanları eklenmelidir.

## 2026-05-08 - Third Person Starter Asset Temelinin Korunması
**Decision:** Projede mevcut Unity Starter Assets Third Person Controller korunacak ve jam için player hareket/kamera başlangıç noktası olarak kullanılacak.

**Reason:** Paket projede zaten mevcut. Hareket, input, kamera ve prefab altyapısı hızlı prototipleme sağlar. Sıfırdan controller yazmak jam riskini artırır.

**Tradeoff:** Tam FPS hissi için ek adaptasyon gerekir. Third person kökenli kamera/animasyon davranışları shooter hedefiyle birebir uyumlu olmayabilir.

**Future reconsideration:** Core loop çalıştıktan sonra kamera pivotu, player mesh visibility ve aim davranışı FPS'e yaklaştırılabilir veya custom controller'a geçilebilir.

## 2026-05-08 - Raycast Weapon Seçimi
**Decision:** İlk weapon sistemi projectile yerine raycast temelli kurulacak.

**Reason:** Fanus enemy'leri hızlı ve net vurmak için raycast daha kolay test edilir. Hit detection, skor ve VFX eventleri hızlı bağlanır.

**Tradeoff:** Fiziksel projectile hissi, travel time ve dodge tasarımı ilk sürümde yoktur.

**Future reconsideration:** Post-jam weapon çeşitlerinde projectile, beam veya area weapon eklenebilir.

## 2026-05-08 - Procedural Map Yerine Arena
**Decision:** İlk sürüm tek arena ve tile grid üzerinden ilerleyecek; gerçek procedural map yapılmayacak.

**Reason:** Jam için okunabilir, test edilebilir ve polish edilebilir bir alan gerekir. Tile kaybı teması küçük arenada daha güçlü hissedilir.

**Tradeoff:** İçerik çeşitliliği sınırlı kalır.

**Future reconsideration:** Grid sistemi oturduktan sonra farklı arena layout'ları veya procedural tile pattern'leri eklenebilir.

## 2026-05-08 - Tile State Sistemi
**Decision:** Arena zemini state tabanlı tile component'leriyle yönetilecek.

**Reason:** "Kayıp" temasının mekanik karşılığı zemin kaybıdır. State flow, görsel feedback ve gameplay etkilerini aynı modelde toplar.

**Tradeoff:** Tile sayısı arttıkça update ve material yönetimi performans riski yaratabilir.

**Future reconsideration:** MaterialPropertyBlock, chunking veya event-only update yaklaşımıyla ölçeklenebilir.

## 2026-05-08 - URP ve Shader Graph Yaklaşımı
**Decision:** Görsel dil URP + Shader Graph üzerine kurulacak.

**Reason:** Projede URP zaten mevcut. Tile dissolve, void edge glow, fanus glass ve post-process hedefleri Shader Graph ile hızlı üretilebilir.

**Tradeoff:** Transparent shader ve post-process ayarları performans/okunurluk riski yaratabilir.

**Future reconsideration:** Kritik shader'lar pahalı hale gelirse daha sade materyal varyantlarına veya custom optimized shader'a geçilebilir.

## 2026-05-08 - Event-Driven Gameplay İletişimi
**Decision:** Tile, enemy, wave, score ve UI sistemleri event'lerle haberleşecek.

**Reason:** Sistemlerin birbirini doğrudan çağırmasını azaltır ve AI assistant'ların yeni feature eklerken bağımlılıkları anlamasını kolaylaştırır.

**Tradeoff:** Fazla event zinciri debug etmeyi zorlaştırabilir.

**Future reconsideration:** Event sayısı artarsa merkezi event channel veya açık inspector referanslı mediator yapısı değerlendirilebilir.

## 2026-05-08 - Save/Load Jam Scope Dışı
**Decision:** İlk jam build'inde save/load yapılmayacak.

**Reason:** CHROMAVOID session bazlı score attack olarak tasarlanıyor. Save sistemi core loop için gerekli değil.

**Tradeoff:** High score ve progression kalıcı tutulmaz.

**Future reconsideration:** Post-jam local high score veya meta progression için JSON tabanlı küçük save sistemi eklenebilir.

## 2026-05-08 - `_Project` Klasörü Altında Prototype Mimari
**Decision:** CHROMAVOID'a özel yeni kodlar ve placeholder production yapısı `Assets/_Project/` altında oluşturuldu.

**Reason:** Kullanıcı bu klasör yapısını istedi ve Starter Assets'in vendor gibi korunması gerekiyor. `_Project`, oyun kodunu Unity Starter Assets paketinden net ayırır.

**Tradeoff:** Önceki MemoryBank önerisindeki `Assets/CHROMAVOID/` yapısı yerine `_Project` kullanılacak; dokümantasyon ve ilerideki AI oturumları bu gerçek yapıya göre davranmalı.

**Future reconsideration:** Proje jam sonrası ürünleşirse `_Project` adı korunabilir veya kontrollü asset migration ile `Assets/CHROMAVOID/` gibi daha markalı bir köke taşınabilir.

## 2026-05-08 - Sahnedeki Fiziksel Tile Grid Yaklaşımı
**Decision:** Grid sistemi procedural generation yapmadan sahnedeki `Tile` component'lerini kaydeden `GridManager` ile kuruldu.

**Reason:** Jam sürecinde debug edilebilirlik ve görsel-first iteration öncelikli. Her tile'ın sahnede fiziksel karşılığı olması state görselini ve spawn sorunlarını hızlı okumayı sağlar.

**Tradeoff:** Tile yerleşimi elle hazırlanmalı ve büyük arenalarda manuel bakım maliyeti artabilir.

**Future reconsideration:** Core loop stabil olduktan sonra editor utility ile tile dizme veya optional grid builder eklenebilir.

## 2026-05-08 - Fanus Düşmanının EnemyContainer Olarak Modellenmesi
**Decision:** Fanus prefabları kod tarafında `EnemyContainer` component'i ile temsil edilecek; Spot, Spreader ve Chain davranışı `EnemyDefinition` ScriptableObject üzerinden beslenecek.

**Reason:** Sanatsal fanus prefab çeşitliliği ile gameplay davranışını temiz ayırır. Aynı kod farklı cam/fanus görselleriyle kullanılabilir.

**Tradeoff:** İlk sürümde enemy davranışı basit lifecycle ve tile etkisiyle sınırlı; karmaşık AI yok.

**Future reconsideration:** Düşmanlar hareket/attack pattern kazandığında `EnemyContainer` yanında ayrı movement/behavior component'leri eklenebilir.

## 2026-05-08 - Shader Graph Hook'lu Placeholder Tile Visual
**Decision:** Tile visual sistemi önce material swap/tint ile çalışacak, aynı zamanda Shader Graph property isimlerine bağlanabilir `TileEffectData` hook'ları taşıyacak.

**Reason:** Visual-first prototype için fading ve black state hemen görünmeli. Shader Graph tamamlanmadan gameplay state akışı beklememeli.

**Tradeoff:** Placeholder tint, final atlas/shader görünümünü birebir temsil etmez.

**Future reconsideration:** Renk atlası ve 3 tonlu shader hazır olduğunda `TileEffectData` propertyleri production shader parametrelerine bağlanmalı.
