# Book Manager – Fullstack Probetest

**Datum:** 3. Dezember 2025

---

## Kontext / Fachliche Aufgabe

Erstelle eine kleine Anwendung **„Book Manager"**, mit der Bücher verwaltet werden können.

Jedes Book hat mindestens folgende Eigenschaften:
- `id` (number)
- `title` (string)
- `author` (string)
- `publishedDate` (date/string im Format YYYY-MM-DD)
- `price` (number)
- `isAvailable` (boolean)

---

## Backend (.NET Minimal API)

### Anforderungen

- **.NET 8 Minimal-API-Projekt** ohne Datenbank
- **Statische In-Memory-Liste** von `Book` in einem Service (Repository-ähnliche Klasse)
- CORS aktivieren für Kommunikation mit Angular Frontend

### Zu implementierende Endpunkte

| Methode | Route | Funktion |
|---------|-------|----------|
| GET | `/api/books` | Alle Bücher abrufen |
| GET | `/api/books/{id}` | Einzelnes Buch abrufen |
| POST | `/api/books` | Neues Buch anlegen |
| PUT | `/api/books/{id}` | Buch aktualisieren |
| DELETE | `/api/books/{id}` | Buch löschen |

### Implementierungshinweise

- Erstelle ein `Book` Record/Class mit den genannten Properties (inkl. `publishedDate`)
- Implementiere einen `BookService` oder `BookRepository`, der eine statische `List<Book>` verwaltet
- In `Program.cs` alle Routen mit Map-Methoden registrieren
- Jeder Endpunkt soll IResult oder das Objekt direkt zurückgeben
- Error-Handling: 404 bei nicht vorhandenem Buch, 400 bei ungültigen Daten
- Testdaten mit verschiedenen Veröffentlichungsdaten vorbefüllen (z.B. 2020, 2023, 2025)

---

## Frontend (Angular)

### Projektstruktur

```
src/app/
├── models/
│   └── book.model.ts
├── services/
│   └── book.service.ts
├── components/
│   ├── books-overview/
│   ├── book-detail/
│   ├── book-form/
│   └── book-list/
├── app.routes.ts
├── app.config.ts
└── app.component.ts
```

### 1. Book Model

Erstelle `src/app/models/book.model.ts`:

- Interface `Book` mit id, title, author, publishedDate (string oder Date), price, isAvailable
- Optionales Interface `CreateBookDto` für POST (ohne id)

**Beispiel:**
```typescript
export interface Book {
  id: number;
  title: string;
  author: string;
  publishedDate: string; // Format: YYYY-MM-DD
  price: number;
  isAvailable: boolean;
}

export type CreateBookDto = Omit<Book, 'id'>;
```

### 2. BookService

Erstelle `src/app/services/book.service.ts`:

- Nutze `HttpClient` und `provideHttpClient()` in `app.config.ts`
- Implementiere Methoden: `getAll()`, `getById(id)`, `create(book)`, `update(book)`, `delete(id)`
- Alle Methoden geben `Observable<...>` zurück
- Nutze `environment.apiUrl` aus der environment-Datei

### 3. Environment-Konfiguration

Erstelle oder aktualisiere `src/environments/environment.ts`:

```typescript
export const environment = {
  apiUrl: 'http://localhost:5000/api'
};
```

### 4. Routing

Konfiguriere `src/app/app.routes.ts` mit folgenden Routen:

| Route | Komponente | Funktion |
|-------|-----------|----------|
| `/books` | BooksOverviewComponent | Übersicht mit Tabelle |
| `/books/add` | BookFormComponent | Neues Buch anlegen |
| `/books/:id` | BookDetailComponent | Detailansicht |
| `/books/:id/edit` | BookFormComponent | Buch bearbeiten |

---

## Komponenten-Anforderungen

### BooksOverviewComponent

**Datenbindung:**
- Signal `isLoading = signal<boolean>(false)` für Ladestatus
- Signal `books = signal<Book[]>([])` für die Bücherliste
- Fehlerbehandlung mit Signal oder Property

**Template:**
- HTML-Tabelle mit `@for (book of books(); track book.id)` Loop
- Spalten: ID, Title, Author, Published Date (formatiert, z.B. DD.MM.YYYY), Price, isAvailable, Actions
- Action-Buttons in jeder Zeile:
  - **Details** → `routerLink="/books/{{book.id}}"`
  - **Bearbeiten** → `routerLink="/books/{{book.id}}/edit"`
  - **Löschen** → `(click)="deleteBook(book.id)"` (mit Bestätigung empfohlen)
- Button zum Hinzufügen: `routerLink="/books/add"`

**Funktionalität:**
- `ngOnInit()`: Alle Bücher laden via `bookService.getAll()`
- `deleteBook(id)`: Buch löschen, dann Liste neu laden
- Published Date in einer lesbaren Form anzeigen (z.B. mit Angular `date` Pipe)

---

### BookDetailComponent

**Datenbindung:**
- `bookId = signal<number>(0)`
- `book = signal<Book | null>(null)`
- `isLoading = signal<boolean>(false)`

**Lifecycle:**
- Route-Parameter `id` auslesen mit `ActivatedRoute.snapshot.paramMap.get('id')`
- `bookService.getById(id)` aufrufen und Result in Signal speichern

**Template:**
- Buchdaten übersichtlich anzeigen (Title, Author, Published Date, Price, isAvailable)
- Published Date formatieren (z.B. mit `date` Pipe: `{{ book().publishedDate | date:'dd.MM.yyyy' }}`)
- Back-Button zur Overview: `routerLink="/books"`
- Edit-Button: `routerLink="/books/{{bookId()}}/edit"`

---

### BookFormComponent

**Verwendung:**
- Wird sowohl für Add (`/books/add`) als auch für Edit (`/books/:id/edit`) genutzt
- Beim Edit: Published Date aus bestehenden Daten laden und anzeigen

**Reactive Forms (empfohlen):**

- `FormBuilder` injizieren
- FormGroup mit Controls: title, author, publishedDate (Datepicker oder Textfeld mit Format YYYY-MM-DD), price, isAvailable
- Validierung:
  - title required
  - author required
  - publishedDate required und gültiges Datumsformat
  - price required + min(0.01)
  - publishedDate darf nicht in der Zukunft liegen
- Submit ruft `bookService.create()` oder `bookService.update()` auf

**Alternative: Template Driven Forms:**

- `[(ngModel)]="..."`-Bindings (wie im Cheatsheet)
- `model<T>()` für Two-Way-Binding mit Signals
- Einfachere Implementierung für diese Größe

**Template:**
- Formularfelder für alle Eigenschaften
- Datepicker oder Textfeld für publishedDate (mit Validierung)
- `[disabled]="!form.valid"` auf Submit-Button
- Success-Message nach Save
- „Zurück" Button (oder Abbrechen-Button)
- Bei Edit: vorhandene Daten laden und Formular füllen
- Published Date in Eingabefeld im Format YYYY-MM-DD anzeigen

---

## Cheatsheet-Themen zur Integration

| Thema | Umsetzung |
|-------|-----------|
| **Routing** | Path-Parameter `:id`, Navigation mit `router.navigate()`, `routerLink` |
| **HTTP-Client** | `bookService` mit GET, POST, PUT, DELETE |
| **Forms** | Reactive Forms (FormBuilder) oder Template Driven Forms mit Validierung |
| **Signals** | `isLoading`, `books`, `selectedBook`, `bookId` für reaktiven State |
| **Directives** | `@for` für Tabelle, `@if` für Conditional Rendering |
| **Observables** | Subscribe in Komponenten, Error-Handling mit `catchError()` |
| **Template Syntax** | Event Binding `(click)`, Property Binding `[routerLink]`, Two-Way `[(ngModel)]` |
| **Date Formatting** | `date` Pipe für Datumsanzeige (z.B. `date:'dd.MM.yyyy'`) |

---

## Implementierungs-Checkliste

### Backend

- [ ] .NET 8 Projekt erstellt
- [ ] `Book` Model mit publishedDate-Property definiert
- [ ] `BookService`/`BookRepository` mit In-Memory-Liste
- [ ] Alle 5 Endpunkte in `Program.cs` implementiert
- [ ] CORS aktiviert
- [ ] Testdaten mit verschiedenen Daten vorbefüllt
- [ ] Mit Postman/Thunder Client getestet (auch Datumswerte überprüft)

### Frontend

- [ ] Angular Projekt mit standalone Components
- [ ] `Book` Model und `BookService` erstellt
- [ ] Environment-Konfiguration gesetzt
- [ ] Routing konfiguriert
- [ ] **BooksOverviewComponent**: Tabelle mit publishedDate-Spalte, Delete, Loading-State
- [ ] **BookDetailComponent**: Detail-View mit Route-Parameter und formatiertem Datum
- [ ] **BookFormComponent**: Formular mit Datums-Input und Validierung (Add + Edit)
- [ ] Signals für `isLoading`, `books`, `bookId` genutzt
- [ ] Navigation zwischen Komponenten funktioniert
- [ ] HTTP-Requests funktionieren (Backend läuft)
- [ ] Error-Handling implementiert
- [ ] Datumsformatierung konsistent (z.B. DD.MM.YYYY im UI, YYYY-MM-DD bei API)

---

## Bewertungskriterien

| Kriterium | Punkte |
|-----------|--------|
| Backend-Endpunkte vollständig und funktionsfähig | 20 |
| Angular Service mit HTTP-Requests | 20 |
| Routing mit Path-Parametern | 15 |
| Overview mit Tabelle und Delete | 15 |
| Detail- und Form-Komponenten | 15 |
| Signals und reaktiver State | 10 |
| Validierung, Error-Handling und Datumsbehandlung | 5 |
| **Summe** | **100** |

---

## Zusätzliche Hinweise

- Starte Backend mit `dotnet run` (Port 5000 oder anpassen in environment.ts)
- Starte Frontend mit `ng serve` (Port 4200)
- Browser Console auf Fehler prüfen (CORS, 404, etc.)
- Testdaten können hardcodiert in der Backend-Liste vorbefüllt werden, z.B.:
  ```csharp
  new Book(1, "Clean Code", "Robert C. Martin", "2008-08-01", 45.99m, true),
  new Book(2, "The Pragmatic Programmer", "Andrew Hunt", "1999-10-30", 49.99m, true),
  new Book(3, "Design Patterns", "Gang of Four", "1994-10-31", 54.99m, false)
  ```
- Für Datumpicker im Frontend kann man auch ein einfaches HTML5 `<input type="date">` verwenden
- Datumsformat konsistent halten: Backend speichert ISO 8601 (YYYY-MM-DD), Frontend zeigt benutzerfreundlich
- Benutzer-Feedback (z.B. „Buch gelöscht") kann über Angular Signals oder Service realisiert werden
