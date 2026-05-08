# CHROMAVOID - Project Overview

## Summary
CHROMAVOID, "Kayıp" temasını fiziksel alan kaybı, renk kaybı ve kontrol kaybı üzerinden işleyen arena tabanlı bir Unity jam oyunudur. Oyuncu, giderek bozulan grid/tile zeminde hayatta kalırken fanus benzeri düşmanları yok eder, void tarafından karartılan alanları yönetir ve wave bazlı skor baskısı altında karar verir.

Bu dosya, AI coding assistant'ların projeyi yeni oturumlarda hızlıca anlaması için oyunun temel niyetini, teknik hedeflerini ve scope sınırlarını özetler.

## Oyun Kısa Özeti
CHROMAVOID'da oyuncu, renk ve zemin bütünlüğünü kaybeden bir arenaya bırakılır. Arena tile'lardan oluşur; tile'lar zamanla veya düşman etkisiyle sağlıklı durumdan bozulmuş, kararmış ve tehlikeli durumlara geçer. Oyuncu hem düşmanları temizlemeye hem de güvenli hareket alanını korumaya çalışır.

Ana deneyim: hızlı, okunabilir, baskılı ve görsel olarak güçlü bir survival shooter.

## Jam Teması
Tema: **Kayıp**

Temanın oyundaki karşılıkları:

| Tema Yorumu | Oyun Karşılığı |
| --- | --- |
| Alan kaybı | Tile'ların void/black duruma geçmesi ve güvenli zeminlerin azalması |
| Renk kaybı | Arena renklerinin bozulup siyah boşluk estetiğine dönüşmesi |
| Kontrol kaybı | Spawn yoğunluğu ve tile corruption nedeniyle hareket seçeneklerinin daralması |
| Zaman kaybı | Wave ilerledikçe oyuncunun karar süresinin azalması |

## Oyun Türü
- Unity tabanlı 3D arena survival shooter.
- Jam scope için single-player score attack.
- Başlangıç altyapısı Third Person Starter Assets üzerinden kuruludur.
- Hedef adaptasyon FPS veya omuz üstü shooter hissine yaklaşmaktır.

## Core Gameplay Loop
1. Oyuncu arena içinde spawn olur.
2. Wave başlar ve fanus/enemy prefabları belirli spawn noktalarından gelir.
3. Oyuncu düşmanları raycast temelli silahla yok eder.
4. Yok edilen düşmanlar skor, geçici tempo rahatlaması veya ileride buff fırsatı sağlar.
5. Arena tile'ları zamanla veya enemy etkisiyle corrupted/black state'e geçer.
6. Oyuncu güvenli tile arar, pozisyon değiştirir ve karar baskısı altında savaşır.
7. Güvenli alan biterse, oyuncu ölürse veya corruption eşiği aşılırsa oyun biter.
8. Skor, wave ve hayatta kalma süresi final sonuç olarak gösterilir.

## Oyuncu Deneyimi Hedefi
- Oyuncu güçlü hissetmeli, fakat arena sürekli daraldığı için asla tamamen rahatlamamalı.
- Her wave "daha fazla skor için risk alayım mı, yoksa güvenli pozisyona mı geçeyim?" sorusunu üretmeli.
- Görsel feedback, tile state değişimlerini anında okunur yapmalı.
- Düşmanlar basit ama baskı kuran davranışlara sahip olmalı.
- Jam build'i kısa sürede anlaşılabilir, tekrar oynanabilir ve skor kovalanabilir olmalı.

## Kayıp Temasının İşlenişi
CHROMAVOID için "kayıp" sadece hikaye etiketi değil, ana sistem davranışıdır. Oyuncu her saniye oynanabilir alanını, renkli dünyayı ve güven hissini kaybeder. Tile'lar siyaha döndükçe arena görsel ve mekanik olarak eksilir. Oyuncu düşman öldürerek geçici kontrol kazanır, fakat wave ilerledikçe kayıp geri döner.

## Teknik Hedefler
- Unity `6000.3.11f1` üzerinde stabil jam build.
- URP `17.3.0` ile Shader Graph destekli tile/void görselliği.
- Input System `1.19.0` ve Starter Assets hareket altyapısından faydalanma.
- Event-driven manager yapısı ile gameplay sistemleri arasında gevşek bağlılık.
- ScriptableObject tabanlı config kullanımı.
- Object pooling'e uygun enemy/spawn mimarisi.
- Basit, hızlı test edilebilir arena scene'i.

## Scope Sınırları
Jam için hedeflenen kapsam:

- Tek arena.
- Tek ana silah.
- Az sayıda düşman tipi.
- Basit wave progression.
- Basit skor sistemi.
- Tile state ve corruption sistemi.
- Minimal UI: skor, wave, health/remaining safe tiles, game over.

Jam dışında bırakılanlar:

- Save/load.
- Multiplayer.
- Uzun campaign/progression.
- Geniş procedural map.
- Karmaşık inventory.
- Çok katmanlı quest veya hikaye sistemi.

## Jam Sonrası Geliştirilebilir Sistemler
- Perk ve upgrade sistemi.
- Farklı arena layout'ları.
- Birden fazla weapon archetype.
- Enemy family ve boss wave'ler.
- Tile restore/cleanse mekanikleri.
- Meta progression.
- Daha gelişmiş post-process, camera shake ve VFX polish.
- Leaderboard veya local high score.
- Tutorial/onboarding flow.

