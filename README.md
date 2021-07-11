# Calculate-Due-Date

A 'DifferentCalculators' nevű osztály 'CalculateDueDate' metódusa két paramétert vár: egy dátumot (ezt string értékként, és dd/MM/yyy hh:mmAM - vagy PM az AM helyett - formában, 
tehát pl. "01/07/2021 09:15AM", míg második paraméterként egy számértéket, ami időbeli óramennyiséget jelent.
Ha pl. egy webfejlesztő cégnél dolgozunk, és valamelyik ügyfél jelent a cég számára egy weboldalbeli problémát, akkor utána mi rögzíthetjük a 'CalculateDueDate' függvény
első paramétereként, hogy mikortól kelljen elkezdeni foglalkozni az adott problémával (azaz a dátumot, mégpedig a fentebb említett formátumban), második paraméterként pedig 
az óraszámot, amennyi időbe szerintünk a probléma javítása telni fog, és a függvény vissza fogja adni a konkrét dátumot (azaz a határidőt), amikorra el kell készülnie a 
javításnak. Fontos kritérium, hogy az első paraméterként megadott dátum csak munkaidőre vonatkozhat (a munkaidő 09:00 -tól 17:00 -ig értendő és csak hétköznapokra, és 12 órás 
rendszert használva, tehát a hh:mm után szóköz nélkül írt AM / PM jelzi a délelőttöt / délutánt. A 'CalculateDueDate' függvény tehát ezek figyelembevételével kalkulálja a 
határidőt. A programhoz írtam Unit Test -eket is.
