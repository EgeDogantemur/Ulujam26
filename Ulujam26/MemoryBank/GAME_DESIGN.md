# CHROMAVOID - Game Design

## Summary
CHROMAVOID, oyuncuya kısa süreli güç fantezisi verirken zeminin ve renklerin kaybolmasıyla sürekli baskı oluşturan bir arena survival shooter tasarımıdır. Tasarımın kalbi grid/tile state sistemi, fanus düşmanları, wave progression ve risk/reward kararlarıdır.

## Fantasy ve Power Fantasy
Oyuncu, void tarafından yutulan bir chroma arena içinde kalan son aktif varlık gibi konumlanır. Power fantasy, "karanlığı vurarak geri iten hızlı ve çevik savaşçı" hissidir. Ancak bu güç sınırsız değildir; arena her wave'de daha fazla kaybolur.

Hedef his:

- Hızlı aim ve hareketle kontrol kazanma.
- Düşman patladığında net görsel/işitsel ödül.
- Zeminin kaybıyla gelen panik.
- Son saniyede güvenli tile'a kaçma.

## Arena Yapısı
Arena, küçük ve okunabilir tutulmalıdır. İlk jam sürümü için tek düzlemli kare/dikdörtgen grid önerilir.

Temel beklentiler:

- Oyuncu tüm önemli tehditleri kısa kamera dönüşleriyle okuyabilmeli.
- Spawn noktaları arena kenarlarında veya köşe/çevre halkasında bulunmalı.
- Merkez alan başlangıçta güvenli hissettirmeli, fakat wave ilerledikçe risklenmeli.
- Arena dışına düşme yerine black tile veya void damage ana tehlike olmalı.

## Grid / Tile Sistemi
Grid, oyunun ana pressure kaynağıdır. Tile sistemi hem görsel hem mekanik state taşır.

Önerilen tile state'leri:

| State | Anlam | Mekanik Etki | Görsel |
| --- | --- | --- | --- |
| Healthy | Güvenli ve renkli tile | Normal hareket | Parlak chroma yüzey |
| Warning | Yakında bozulacak tile | Oyuncuya kaçış zamanı verir | Flicker, renk solması |
| Corrupted | Tehlikeli tile | Damage, slow veya score penalty adayı | Siyah damarlar, düşük emission |
| Black | Kaybolmuş tile | Ölümcül veya çok yüksek damage | Mat siyah/void, kenar glow |
| Locked/Inactive | Spawn veya arena sınırı için özel tile | Üzerine çıkılamaz ya da sistem dışı | Ayrık materyal |

## Tile State Sistemi
Tile state geçişleri deterministic ve okunabilir olmalıdır:

1. Healthy tile seçilir.
2. Warning state'e geçer ve kısa süre görsel uyarı verir.
3. Corrupted state'e geçer.
4. Süre veya wave etkisiyle Black state'e dönüşür.
5. Jam sonrası cleanse/restore sistemi gelirse Black dışındaki bazı state'ler geri döndürülebilir.

Her state değişimi event yayınlamalıdır. Böylece UI, VFX, skor ve difficulty sistemleri tile script'ine doğrudan bağlanmaz.

## Enemy / Fanus Sistemi
"Fanus" düşmanları cam/enerji küresi hissi veren, void corruption taşıyan varlıklar olarak tasarlanır. Fanus fikri, oyuncunun renkli dünyasını kapatan veya hapseden düşman metaforunu destekler.

İlk enemy davranışı:

- Spawn olur.
- Oyuncuya doğru hareket eder.
- Yaklaştığında tile corruption tetikler veya oyuncuya hasar verir.
- Vurulduğunda kırılır/patlar.
- Ölüm event'i skor ve wave sistemine bilgi verir.

## Spawn Mantığı
Spawn sistemi basit ve yönetilebilir olmalıdır:

- Spawn noktaları scene içinde elle yerleştirilebilir.
- Wave config, hangi düşmanın kaç adet ve hangi aralıklarla geleceğini belirler.
- Spawn noktası oyuncuya çok yakınsa geçici olarak atlanır.
- Spawn edilen enemy object pooling'e uygun olmalıdır.

## Wave Progression
Wave progression hedefi, oyuncuya ritim vermektir.

Jam önerisi:

- Wave 1: düşük enemy sayısı, corruption yavaş.
- Wave 2-3: spawn aralığı kısalır, warning süresi azalır.
- Wave 4+: enemy sayısı artar, black tile oranı hızlanır.
- Endless mod mantığı: wave index arttıkça katsayılar büyür.

## Skor Sistemi
Skor, oyuncuyu risk almaya teşvik etmelidir.

Skor kaynakları:

- Enemy kill.
- Consecutive kill/combo.
- Wave clear bonus.
- Safe tile üzerinde kalma veya black tile yakınında kill alma gibi risk bonusları.

Skor cezaları:

- Damage alma.
- Corrupted tile üzerinde fazla kalma.
- Çok uzun süre düşman öldürmeme.

## Game Over Condition
İlk sürüm için net game over koşulları:

- Oyuncu health değeri sıfıra iner.
- Güvenli tile sayısı minimum eşik altına iner.
- Oyuncu Black tile üzerinde belirli süreden fazla kalır veya anında ölür.

## Oyuncu Davranış Hedefleri
Oyuncunun şu davranışlara yönelmesi istenir:

- Sürekli pozisyon değiştirme.
- Spawn yönlerini takip etme.
- Tile warning görselini önemseme.
- Enemy öldürme ve güvenli alan koruma arasında öncelik seçme.
- Skor için riskli alanlara kısa süreli girip çıkma.

## Risk / Reward Tasarımı
Risk ve ödül basit, okunabilir ve hızlı olmalıdır.

| Risk | Reward |
| --- | --- |
| Corrupted tile yakınında savaşmak | Daha yüksek skor çarpanı |
| Enemy'yi geç öldürmek | Combo veya multi-kill fırsatı |
| Merkezde kalmak | Daha iyi görüş ve aim alanı |
| Kenara kaçmak | Daha güvenli tile ama spawn'a yakınlık riski |

## Karar Verme Baskısı
Oyuncuya aynı anda üç karar baskısı verilmelidir:

- Nerede durmalıyım?
- Önce hangi düşmanı öldürmeliyim?
- Skor için risk almalı mıyım?

Bu baskı UI açıklamasıyla değil, sistemlerin doğal çakışmasıyla yaratılmalıdır.

## Future Upgrade Ideas
- Dash cooldown düşürme.
- Black tile üzerinde kısa süre hayatta kalma.
- Kill sonrası yakındaki tile'ı cleanse etme.
- Fanus kırıldığında alan hasarı.
- Combo süresini uzatma.
- Weapon pierce veya ricochet.

## Planned Buffs / Perks
| Perk | Etki | Design Notu |
| --- | --- | --- |
| Chroma Surge | Kısa süre fire rate artışı | Power spike yaratır |
| Void Step | Dash sonrası kısa invulnerability | Riskli kaçışları ödüllendirir |
| Glassbreaker | Fanus düşmanlarına ekstra hasar | Enemy fantasy ile uyumlu |
| Reclaim Pulse | Kill sonrası küçük tile restore alanı | Kayıp temasına karşı hamle |
| Last Color | Health düşükken skor çarpanı | Comeback hissi |

## Potential Enemy Ideas
- Basic Fanus: oyuncuya yürür, temas hasarı verir.
- Seeder Fanus: geçtiği tile'ları warning/corrupted yapar.
- Splitter Fanus: ölünce küçük parçalara ayrılır.
- Shield Fanus: belirli yönden gelen hasarı azaltır.
- Anchor Fanus: yakındaki tile'ları daha hızlı karartır.

## Juice / Game Feel Fikirleri
- Enemy kırılmasında cam çatlama sesi.
- Kill anında kısa hit stop.
- Tile warning sırasında pulse emission.
- Black tile kenarında chromatic aberration artışı.
- Low health sırasında vignette ve audio filter.
- Wave clear anında kısa renk geri dönüş efekti.
- Combo sırasında UI sayı animasyonu.

