## ProjectX

- Název projektu je zvolený náhodně, protože jsem ho původně neměl v plánu zveřejňovat, je jen pro moje potřeby, takže jsem nad názvem moc nepřemýšlel.
- Bude sloužit k získávání dat o akciích a ETF, k analyzování výsledků obchodování, k výpočtům složeného úročení a k zasílání upozornění na poklesy nebo růsty cen podle vlastního nastavení.
- Projekt je rozpracovaný, většina funkcionalit zatím chybí, ale průběžně přidávám nové prvky podle svých potřeb.
- Protože nemám žádný placený API token, musím některá data získávat jinak než prostřednictvím free tokenu a imporvizovat.  Jinak by byl kód napsán flexibilněji.

### MarketDataService
- Tato služba slouží k ukládání dat do databáze, na základě kterých se pak naplňují tabulky v ProjectX. Momentálně běží jako Worker a spouštím ji manuálně. Po nasazení bude nastavena tak, aby se spouštěla X krát denně přes Quartz.
- Zároveň služba porovnává nové nebo vyřazené akcie ze seznamů S&P 500, Nasdaq 100 a Dow Jones. Pokud dojde ke změně, obdržím e-mailové upozornění.
- Ze služby mě zajímají hlavně maximální historická data, týdenní data a denní data.
- V budoucnu na tato data naváže další služba, která bude sledovat například poklesy od nejvyšších maximálních hodnot nebo výrazné denní a týdenní poklesy podle nastavení uživatele.
-Worker postupně volá asynchronní metody SaveAssetsAsync, SaveAssetHistoricalAsync, SaveAssetHistoricalDataAsync a SendMailWithChangedAssetsAsync

#### SaveAssetsAsync
- Ukládá akcie do tabulky **Asset**, například název, symbol, sektor atd.
Seznamy Nasdaq 100 a Dow Jones získávám z financialmodelingprep.com, kde mám free token.
S&P 500 jsem musel získat z Wikipedie, protože ve free verzi financialmodelingprep.com není dostupný a je k dispozici pouze v placené verzi.
- V metodě **SaveAssetsAsync** se nachází **CheckAsset**, která kontroluje, zda byla nějaká akcie přidána nebo odebrána ze seznamu.Pokud byla přidána, uloží se do tabulky **NewAssets**.Pokud byla odebrána, uloží se do tabulky **ExcludedAssets** a zároveň se smaže z tabulky **Asset**s.

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
-SaveAssetHistoricalData je napsáno tak, že při prvním spuštění služby stáhne obrovské množství dat. Při dalším spuštění již stahuje pouze data od posledního spuštění, a díky optimalizaci je služba mnohem rychlejší
