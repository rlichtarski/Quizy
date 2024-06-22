# Quiz Konsolowa Aplikacja w C# z .NET

## Opis
Aplikacja konsolowa do przeprowadzania quizów z informatyki, zaprojektowana w C# z wykorzystaniem .NET Core. 
Umożliwia użytkownikom rozwiązywanie quizów, rejestrację i logowanie, a wyniki są zapisywane w bazie danych PostgreSQL.

## Struktura Projektu
```
QuizApp/
│
├── Program.cs
├── QuizManager.cs
├── DatabaseManager.cs
├── UserInterface.cs
├── Models/
│ ├── User.cs
│ ├── Question.cs
│
├── UML_Quizy.png
└── README.md
```

## Inicjalizacja Bazy Danych
Aby zainicjalizować bazę danych, wykonaj następującą komendę:

```
CREATE DATABASE quizzes_db
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LOCALE_PROVIDER = 'libc'
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
```

## Podsumowanie
Wnioski z Projektu

1. **Zrozumienie OOP**: Projekt pozwala na pogłębienie wiedzy na temat zasad programowania obiektowego (OOP), stosując je w praktycznym scenariuszu.

2. **Praktyczne zastosowanie C# i .NET**: Użytkownicy mogą zobaczyć, jak efektywnie używać C# i .NET w tworzeniu aplikacji konsolowych, które są zarówno funkcjonalne, jak i łatwe do rozwijania.

3. **Interakcja z bazą danych**: Projekt umożliwia naukę podstaw interakcji z systemami baz danych za pomocą SQL i Npgsql, koncentrując się na operacjach CRUD.

4. **Zarządzanie stanem aplikacji**: Uczestnicy projektu mogą zrozumieć, jak zarządzać stanem użytkownika i danych w aplikacji konsolowej.

5. **Praca z danymi wejściowymi użytkownika**: Projekt demonstruje, jak obsługiwać wejście użytkownika w sposób bezpieczny i efektywny, w tym walidację danych wejściowych i obsługę błędów.


