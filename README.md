# Fibretel – ASP.NET projekt

## Popis

Tento projekt byl mým **prvním webem vytvořeným pro konkrétní fyzickou osobu / organizaci**. Cílem bylo navrhnout moderní a responzivní webovou prezentaci s přehledem firemních služeb, administrátorským rozhraním a základními veřejnými stránkami jako **Domů**, **O nás**, a **Kontakt**.

Web obsahuje:
- **Hlavní stránky (Domů, O nás, Kontakt)** – připravené jako šablony s placeholdery pro text a fotografie.
- **Administrátorské rozhraní** – slouží pro správu poptávek a přidávání nových služeb.
- **Dvouúrovňovou správu přístupu**:
  - *Nadřízený administrátor* – může spravovat ostatní účty a logy.
  - *Podřízený administrátor* – má omezený přístup pouze k obsahu.
- **Mail servis** pro odesílání potvrzení nebo notifikací pomocí `System.Net.Mail`.

## Funkce

- Responzivní rozložení (HTML/CSS/JS)
- Zabezpečené přihlašování a role-based přístup (přes atributy v ASP.NET)
- Záznam logů a administrátorských akcí
- Interní správa služeb a příchozích poptávek
- Testování e-mailového odesílání přes **Papercut SMTP** na `localhost:25`

## Databáze

- **Systém**: MySQL (spuštěno lokálně přes XAMPP)
- **Ukládání dat**: tabulky pro uživatele, služby, žádosti a logy
- V relaci `Requests → Services` není použit cizí klíč (pouze string), což bylo zpětně rozpoznané jako nedostatek

## Autentizace

- **Přesměrování nepřihlášených uživatelů** na přihlašovací stránku
- **Role-based restrikce**: kontrola, zda je uživatel nadřízený administrátor
- V případě neautorizovaného přístupu je uživatel automaticky odhlášen

## E-mailový servis

- Testováno přes **Papercut SMTP**
- Odesílání e-mailů bez hesla (fiktivní uživatelské adresy, bez SSL)
- Použití `System.Net.Mail` knihovny od Microsoftu

## Technologie

- **Frontend**: HTML, CSS, JavaScript (responzivní design)
- **Backend**: C# – ASP.NET
- **Databáze**: MySQL (lokálně přes XAMPP)
- **Mail server**: Papercut SMTP
- **Nástroje**: GitHub Desktop (Push, Pull, Fetch, Reset, Revert)

## Stav projektu

Projekt se nakonec **nedostal do produkce**, ale slouží jako **funkční šablona** a praktická ukázka schopností práce s ASP.NET, databázemi, autentizací a e-mailovým rozhraním.

> Projekt mi zároveň pomohl prohloubit znalosti v oblasti návrhu rozhraní, správě oprávnění a komunikace mezi vrstvami aplikace.
