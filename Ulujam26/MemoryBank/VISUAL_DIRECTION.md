# CHROMAVOID - Visual Direction

## Summary
CHROMAVOID'un görsel dili, canlı chroma renklerin siyah void tarafından yutulması üzerine kuruludur. Oyuncu başlangıçta okunabilir ve enerjik bir arena görür; wave ilerledikçe renkler kaybolur, tile'lar kararmaya başlar ve fanus düşmanları cam/enerji kırılmalarıyla sahneye tehdit katar.

## Renk Paleti
Ana kontrast: **yüksek doygunluklu renkler vs. siyah void**.

| Rol | Renk Hissi | Kullanım |
| --- | --- | --- |
| Healthy tile | Cyan, magenta, lime, elektrik mavi | Güvenli alan ve skor enerjisi |
| Warning tile | Sarı/turuncu pulse | Yaklaşan tehlike |
| Corrupted tile | Mor, koyu kırmızı, siyaha kayan gradient | Tehlikeli ama geçilebilir alan |
| Black tile | Mat siyah, düşük mor edge glow | Kaybolmuş alan |
| Enemy/fanus | Cam beyazı, cyan edge, içte void core | Tehdit ve hedef okunurluğu |
| UI | Beyaz/cyan metin, kırmızı danger accent | Net HUD |

## Void / Black Corruption Estetiği
Void estetiği sadece siyah yüzey olmamalıdır. İyi görünüm için:

- Tile kenarlarında ince renkli emission kalıntısı.
- İç yüzeyde yavaş hareket eden noise.
- Corruption ilerlerken damar/çatlak hissi.
- Black state'te ışığı emen mat görünüm.
- Yakın çevrede hafif post-process yoğunluğu.

## Shader Hedefleri
URP Shader Graph ile hedeflenen shader aileleri:

- Tile state shader: renk, emission, noise mask, dissolve/fade parameter.
- Black void shader: koyu yüzey, edge glow, world-space noise.
- Fanus glass shader: fresnel, transparency, crack mask, inner core emission.
- Fullscreen void shader: ekran kenarı corruption ve chromatic distortion.

## Tile Fading Görünümü
Tile state geçişleri aniden material swap gibi görünmemelidir. Hedef:

1. Healthy renk solmaya başlar.
2. Warning pulse artar.
3. Noise mask yüzeyi yer yer yer.
4. Corrupted damarlar belirir.
5. Black state yüzeyi kaplar.

Jam için material swap kabul edilebilir, fakat geçişte lerp/dissolve efekti önceliklidir.

## Black Tile Görünümü
Black tile, oyuncunun kaçınması gereken en net görsel olmalıdır.

Özellikler:

- Neredeyse tamamen siyah.
- Kenarlarda ince chroma glow.
- Merkezde hafif hareketli noise.
- Üzerinde durulduğunda player feedback: vignette, distortion, damage tick.

## Fanus Görünümü
Fanus düşmanları cam küre veya cam kapsül hissinde olmalıdır:

- Dış kabuk transparan/fresnel.
- İçte siyah veya mor void core.
- Hasar aldığında çatlak emission.
- Ölümde cam kırığı parçacıkları.
- Spawn sırasında kısa süreli şişme/pulse.

## Cam Shader Fikirleri
- Fresnel rim: hedef okunurluğu sağlar.
- Alpha clipping değil, transparent blend tercih edilir.
- Crack mask texture ile hasar feedback'i verilebilir.
- Inner core ayrı mesh/material olabilir.
- Ölüm öncesi emissive flicker kullanılabilir.

## UI Direction
UI minimal ve okunur olmalıdır:

- Crosshair sade.
- Skor ve wave üst köşelerde.
- Safe tile/void pressure göstergesi kısa bir bar veya yüzde olabilir.
- Game over ekranında score, wave, time survived.
- UI, oyun sistemlerini açıklayan uzun metinler taşımamalı; feedback ile öğretmeli.

## Post Process Hedefleri
URP volume üzerinden:

- Bloom: chroma emission'ı güçlendirmek için.
- Chromatic Aberration: void yoğunluğu veya damage anında artar.
- Vignette: danger ve black tile feedback.
- Color Adjustments: wave clear veya low health için renk değişimi.

Efektler okunurluğu bozmayacak seviyede tutulmalıdır.

## Lighting Yaklaşımı
- Arena düşük ambient ve güçlü emissive yüzeylerle okunmalı.
- Düşmanlar kendi edge glow'uyla seçilmeli.
- Directional light sade tutulabilir.
- Realtime shadow maliyeti kontrol edilmeli.
- Tile state renkleri aydınlatmadan bağımsız okunabilmeli.

## Atmosfer Hedefi
Atmosfer: "renkli bir simülasyonun siyah boşluk tarafından yutulması".

Oyuncu şunları hissetmeli:

- Başlangıçta enerji ve kontrol.
- Orta wave'lerde sıkışma.
- Son wave'lerde kaotik ama okunabilir panik.
- Her kill'de kısa bir renk kazanımı.

## VFX Fikirleri
- Enemy death glass burst.
- Hit spark ve küçük chroma shard.
- Tile warning pulse ring.
- Corruption spread trail.
- Wave start arena pulse.
- Game over void collapse.
- Score combo pop-up.

## Future Polish Ideas
- Procedural crack mask blending.
- Tile state'e bağlı dynamic light.
- Fanus kırık mesh varyasyonları.
- Audio reactive emission.
- Camera shake presetleri.
- Fullscreen void tendril efekti.
- Custom skybox veya void dome.

