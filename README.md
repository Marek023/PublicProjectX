## ProjectX

- Název projektu je zvolený náhodně, protože jsem ho původně neměl v plánu zveřejňovat, je jen pro moje potřeby, takže jsem nad názvem moc nepřemýšlel.
- Bude sloužit k získávání dat o akciích a ETF, k analyzování výsledků obchodování, k výpočtům složeného úročení a k zasílání upozornění na poklesy nebo růsty cen podle vlastního nastavení.
- Projekt je rozpracovaný, většina funkcionalit zatím chybí, ale průběžně přidávám nové prvky podle svých potřeb.
- Protože nemám žádný placený API token, musím některá data získávat jinak než prostřednictvím free tokenu a imporvizovat.  Jinak by byl kód napsán flexibilněji.

- ---------------------------------

### MarketDataService
- Tato služba slouží k ukládání dat do databáze, na základě kterých se pak naplňují tabulky v ProjectX. Momentálně běží jako Worker a spouštím ji manuálně. Po nasazení bude nastavena tak, aby se spouštěla X krát denně přes Quartz.
- Zároveň služba porovnává nové nebo vyřazené akcie ze seznamů S&P 500, Nasdaq 100 a Dow Jones. Pokud dojde ke změně, obdržím e-mailové upozornění.
- Ze služby mě zajímají hlavně maximální historická data, týdenní data a denní data.
- V budoucnu na tato data naváže další služba, která bude sledovat například poklesy od nejvyšších maximálních hodnot nebo výrazné denní a týdenní poklesy podle nastavení uživatele.
- Worker postupně volá asynchronní metody SaveAssetsAsync, SaveAssetHistoricalAsync, SaveAssetHistoricalDataAsync a SendMailWithChangedAssetsAsync

#### SaveAssetsAsync
- Ukládá akcie do tabulky **Asset**, například název, symbol, sektor atd.
Seznamy Nasdaq 100 a Dow Jones získávám z financialmodelingprep.com, kde mám free token.
S&P 500 jsem musel získat z Wikipedie, protože ve free verzi financialmodelingprep.com není dostupný a je k dispozici pouze v placené verzi.
- V metodě **SaveAssetsAsync** se nachází **CheckAsset**, která kontroluje, zda byla nějaká akcie přidána nebo odebrána ze seznamu. Pokud byla přidána, uloží se do tabulky **NewAssets**.Pokud byla odebrána, uloží se do tabulky **ExcludedAssets** a zároveň se smaže z tabulky **Assets**.

#### SaveAssetHistoricalAsync
- Ukládá podrobnější data o akciích do tabulky **AssetHistorical**, například datum přidání do seznamu Nasdaq nebo datum vyřazení ze seznamu.
I když mám samostatné tabulky pro přidání a vyřazení akcií, ty nejsou historické jako tato tabulka, která uchovává kompletní záznam změn v čase.

#### SaveAssetHistoricalDataAsync
- Ukládá historická data do tabulky **AssetHistoricalData**, která patří mezi nejdůležitější tabulky. Obsahuje informace o otevírací a zavírací ceně trhu, změnách cen a dalších klíčových údajích.
Při prvním uložení se stahují data od roku 1990, aby bylo možné získat nejvyšší historická data ze všech seznamů.
Protože jeden dotaz vrací velké množství dat, musel jsem provádět requesty po jednom symbolu, protože free token neumožňuje hromadné dotazy.
- V metodě **SaveAssetHistoricalDataAsync** se dále nacházejí metody **UpdateHistoricalDataAsync** a **OptimizeHistoricalData**.
- **UpdateHistoricalDataAsync** aktualizuje seznam historických dat tím, že získá poslední záznam v tabulce AssetHistoricalData a následně aktualizuje tabulky od tohoto posledního záznamu po aktuální data. Jelikož se jedná o kratší časový úsek (méně než rok), free token mi umožňuje stahovat akcie po pěti najednou.
Proto je update prováděn jinak než předchozí ukládání, které bylo realizováno po jednom symbolu.
- **OptimizeHistoricalData** Jelikož jsem stahoval data od roku 1990, měl jsem v databázi více než 4 000 000 řádků. Abych zlepšil výkon, provedl jsem optimalizaci, při které jsem si po získání všech dat vybral pouze ta, která opravdu potřebuji.
Zaměřil jsem se na nejvyšší historickou cenu a poslední týden, zbytek dat jsem odstranil. Díky tomu jsem redukci z 4 milionů řádků zúžil na přibližně 5 000.
- SaveAssetHistoricalData je napsáno tak, že při prvním spuštění služby stáhne obrovské množství dat. Při dalším spuštění již stahuje pouze data od posledního spuštění, a díky optimalizaci je služba mnohem rychlejší

#### SendMailWithChangedAssetsAsync
- Kontroluje tabulky **NewAssets** a **ExcludedAssets**. Pokud je hodnota ve sloupci **NotificationCreated** false, vytvoří obsah e-mailu a uloží ho do tabulky **AssetNotificationQueue**.
Následně se vytvoří záznam ve vazební tabulce mezi **AssetNotificationQueue** a uživatelem a dojde k odeslání e-mailu. Pokud je e-mail úspěšně odeslán, nastaví se ve vazební tabulce **NotificationSent** na true.
- Na konci probíhá ještě kontrola pomocí **CheckingSentEmailsAsync**, která se provádí pouze v případě, že v tabulkách nejsou žádné změny. Tato metoda ověřuje, zda byly všechny e-maily ve vazební tabulce skutečně odeslány. Slouží jako záložní mechanismus, pokud při odesílání došlo k chybě, zajistí, že chybějící e-maily budou odeslány dodatečně při dalším spuštění.

- ---------------------------------

### MarketIndexAnalyzerService
- Byl vytvořen pro získávání dat z ETF. Protože jsem pomocí free tokenu nemohl získat data z indexu Nasdaq 100 tak je stahuji z jiného zdroje.
- Zajímalo mě, jaký je rozdíl v procentuálním zisku mezi pravidelným investováním a investováním při propadech na reálných datech. Mám například nastaveno, že nákup probíhá při každém propadu o 3 % od nejvyšší historické hodnoty a zároveň si mohu určit hranici, při které se investovaná částka zdvojnásobí. 
- **SaveHistoricalDataFromFinexAsync** Stahuje pouze index iShares Nasdaq 100 a ukládá data do databáze. Dále je využívám v **CalculationOfSlumps** pro výpočet hodnot. Celý výpočet je implementován v metodě **Calculation**, která není rozdělena do menších funkcí, protože slouží pouze pro mé potřeby i když správně by měla být strukturována lépe.

- ---------------------------------

### ProjectX
- Záložky S&P 500, Nasdaq 100 a Dow Jones fungují stejně, liší se pouze zobrazovanými daty, která lze libovolně řadit a vyhledávat.
- Záložka Statistiky vznikla, protože na platformě XTB, kde obchoduji, nejsou statistiky přehledně zpracované, chybí například možnost zobrazit procentuální zhodnocení za měsíc nebo rok. Zobrazení dat v tabulce je již dokončeno, ale import dat zatím není hotový.
- V ProjectuX ve složce Services se nachází kalkulačky rozdělené do tří typů:
  - Klasická kalkulačka složeného úročení
  - Kalkulačka složeného úročení na reálných datech
  - Kalkulačka s propady, která je využívána i v předchozí službě
  
    Tyto kalkulačky budou v budoucnu sloužit jako další záložka pro získávání podrobnějších informací, Stejně jako v MarketIndexAnalyzerService nejsou kalkulačky rozdělené do menších funkcí, protože slouží pouze pro mé potřeby. V budoucnu plánuju jejich přepracování.
- V budoucnu přidám další záložku, kde si budu moct nastavit různé typy akcií nebo ETF a definovat limity pro upozornění na jejich pokles či růst. Součástí bude také napojení na kalkulačku, která bude sledovat propady ETF indexu podle mnou nastaveného plánu. Většina brokerů totiž umožňuje hlídání pouze konkrétní hodnoty, ale ne procentuálního poklesu, takže díky tomu nebudu muset denně kontrolovat situaci od nejvyšších bodů ručně.

- ---------------------------------

### Závěr

- Pořád se považuji za juniora, takže je tam hodně co zlepšovat, od pojmenování metod a funkcí až po celkovou strukturu kódu. Navíc je celý projekt stále rozpracovaný a primárně slouží pro mé vlastní potřeby.

