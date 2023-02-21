# Rekrutacja_Dataedo

Dzień dobry. 
Dodaję informację odnośnie projektu oraz krótkie omówienie wad kodu, który otrzymałęm od "kolegi". 

Wady:
- Ignorowanie zasady S z SOLID. Zasada S (single responsibility) mówi nam o tym, że klasa oraz metody muszą robić jak najmniej rzeczy. W przypadku danego kodu - jedna metoda ImportAndPrintData. 
Jak polepszyć? Rozbić na metody, które robią tylko jedną rzecz => przykład: metoda, która tylko printuje informację lub pobiera zawartość pliku.

- Ignorowanie zasady D z SOLID. Zasada D (Dependency reversion) mówi o tym, że musimy dziedziczyć od klasy abstracji (interfejsów lub abstrakcyjnych klas).
Jak polepszyć? Stworzyć z ImportedObjectBaseClass interfejs i zrobić dziedzycienie z klasy ImportedObject po tym interfejsie.

- Brak sprawdzenia argumentów metod na null-e czy długość list. Takie zachowanie może spowodować takie problemy jak : NullReferenceException lub IndexOutOfRangeException.

- Brak strukturyzacji kodu. Klasy  ImportedObject oraz ImportedObjectBaseClass znajdują się w pliku DataReader, co powoduje to, że inny programista będzie długo szukam gdzie te klasy się znajdują.
Jak polepszyć? Przenieść te klasy do osobnego folderu, co polepszy czytelność kodu.

Podsumowując : największą wadą kolegi jest ignorowanie zasad SOLID i brak sprawdzenia argumentów metod.

P.S Jeżeli widzicie Państwo sposób na ulepszenie mojego kodu - zapraszam na priv :)
