

-- Kullanıcı Tablosu Örnek Veriler

INSERT INTO kullaniciTable (kullanici_id, ad_soyad) VALUES
(1, 'Ali Veli'),
(2, 'Ayşe Yılmaz'),
(3, 'Ahmet Kaya');

------------------------------------------------------------------------------------------

-- Ülke Tablosu Örnek Veriler

INSERT INTO ulkeTable (ulke_id, ulke_adi) VALUES
(1, 'Türkiye'),
(2, 'Birleşik Krallık'),
(3, 'ABD');

-------------------------------------------------------------------------------------------

-- Tür Tablosu Örnek Veriler

INSERT INTO turTable (tur_id, tur_adi) VALUES
(1, 'Rock'),
(2, 'Pop'),
(3, 'Jazz');

-------------------------------------------------------------------------------------------

-- Festival Tablosu Örnek Veriler

INSERT INTO festivalTable (festival_adi, baslangic, bitis, konum) VALUES
('Rock Fest', '10:00', '22:00', 'Ankara'),
('Pop Fest', '12:00', '20:00', 'İstanbul'),
('Caz Fest', '14:00', '23:00', 'İzmir');

-------------------------------------------------------------------------------------------

-- Kullanıcı İletişim Tablosu Örnek Veriler

INSERT INTO kullanici_iletisimTable (iletisim_id, kullanici_id, iletisim_bilgisi, iletisim_tipi) VALUES
(1, 101, 'ali@gmail.com', 'Email'),
(2, 102, '555-1234', 'Telefon'),
(3, 103, '@ahmetkaya', 'Twitter');

-------------------------------------------------------------------------------------------

-- Kategori Tablosu Örnek Veriler

INSERT INTO kategoriTable (kategori_id, kategori_adi) VALUES
(1, 'VIP'),
(2, 'Standart'),
(3, 'Öğrenci');

-------------------------------------------------------------------------------------------

-- Sanatçı Tablosu Örnek Veriler

INSERT INTO sanatciTable (sanatci_id, sanatci_adi, ulke_id, tur_id) VALUES
(1, 'Metallica', 1, 1),
(2, 'Tarkan', 1, 2),
(3, 'Miles Davis', 2, 3);

-------------------------------------------------------------------------------------------------

-- Etkinlik Tablosu Örnek Veriler

INSERT INTO etkinlikTable (festival_id, etkinlik_adi, baslangisSaat, bitisSaat) VALUES
(1, 'Metallica Konseri', '10:30', '12:00'),
(2, 'Tarkan Sahne', '13:00', '15:00'),
(3, 'Miles Davis Performansı', '16:00', '18:00');

-------------------------------------------------------------------------------------------

-- Bilet Tablosu Örnek Veriler

INSERT INTO biletTable (festival_id, fiyat, kategori_id) VALUES
(1, 300, 1),
(2, 200, 2),
(3, 150, 3);

-------------------------------------------------------------------------------------------

-- Satış Tablosu Örnek Veriler

INSERT INTO satisTable (kullanici_id, bilet_id, satis_tarihi) VALUES
(1, 1, '2025-01-01'),
(2, 2, '2025-01-02'),
(1, 3, '2025-01-03');

--SATIŞ VERİSİ SİLME

DELETE FROM satisTable WHERE satis_id = 4


-------------------------------------------------------------------
--ÇALIŞTIRMA KOMUTU STORE PROCEDURE SATIŞ BİLGİLERİ GETİRME

exec sp_satisBilgisi

---------------------------------------------------------------------


--ÇALIŞTIRMA ÖRNEK KOD

exec sp_satisEkle 
    @kullanici_id = 101, 
    @bilet_id = 20003, 
    @tarih = '2025-01-04';

	--------------------------------------------------------------------
-- festival, konumu, başlangıç bitiş , etkinlik adı TEK TABLODA SELECT ETME   


SELECT 
    f.festival_adi AS FestivalAdi,
    f.konum AS FestivalKonumu,
    f.baslangic AS FestivalBaslangicTarihi,
    f.bitis AS FestivalBitisTarihi,
    e.etkinlik_adi AS EtkinlikAdi,
    e.baslangicSaat AS EtkinlikBaslangicSaati,
    e.bitisSaat AS EtkinlikBitisSaati
 
    
FROM 
    festivalTable f
JOIN 
    etkinlikTable e ON f.festival_id = e.festival_id
JOIN 
    sanatciTable s ON e.festival_id = s.sanatci_id
JOIN 
    ulkeTable u ON s.ulke_id = u.ulke_id
JOIN 
    turTable t ON s.tur_id = t.tur_id;

	--------------------------------------------------------------------------------

	-- SANATÇI BİLGİLERİNİ BİRLEŞTİRİP SELECT EDEN KOD

	SELECT 
    s.sanatci_adi AS SanatciAdi,
    u.ulke_adi AS UlkeAdi,
    t.tur_adi AS TurAdi
FROM 
    sanatciTable s
JOIN 
    ulkeTable u ON s.ulke_id = u.ulke_id
JOIN 
    turTable t ON s.tur_id = t.tur_id;

	----------------------------------------------------------------------------------

	--İLETİŞİM TİPİNE GÖRE KULLANICI SELECT EDEN STORE PROCEDURE

CREATE PROC sp_iletisimTipi
@tip varchar(50)
as
begin
select * from kullanici_iletisimTable where iletisim_tipi = @tip


end

-------------------------------------------------------------------------

exec sp_iletisimTipi @tip = 'E-posta';

-------------------------------------------------------------------------