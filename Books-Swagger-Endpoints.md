# Books API — Swagger Endpoints

Task 1: Extend & Enhance the Books API — **Requirements met.**  
Below are the **Swagger endpoints** for the Books API (in-memory store; no database).

---

## Base path: `/Books`

| Method | Endpoint | Summary | Responses |
|--------|----------|---------|-----------|
| **GET** | `/Books` | Get all books | 200 OK → `List<BookModel>` |
| **GET** | `/Books/{id}` | Get a single book by id | 200 OK → `BookModel` · 404 Not Found |
| **GET** | `/Books/category/{category}` | Get books by category (case-insensitive) | 200 OK → `List<BookModel>` (empty list if none) |
| **POST** | `/Books` | Create a book | 200 OK → `BookModel` · 400 Bad Request (duplicate title or invalid category) |
| **PUT** | `/Books/{id}` | Update book Title and Category | 204 No Content · 400 Bad Request · 404 Not Found |
| **DELETE** | `/Books/{id}` | Delete a book (requires auth) | 204 No Content · 404 Not Found |

---

## Schemas (Swagger)

### BookModel
| Property | Type |
|----------|------|
| `id` | integer |
| `title` | string |
| `category` | string |

### BookRequest (body for POST / PUT)
| Property | Type |
|----------|------|
| `title` | string |
| `category` | string |

**Allowed categories:** `Novel`, `Science`, `History` (case-insensitive).

---

## Requirements checklist (Task 1)

1. **Get Single Book** — Returns book by matching id; 404 if not found. Uses `FirstOrDefault` in `InMemoryBookStore.GetById`.
2. **PUT /books/{id}** — Accepts `BookRequest`; updates Title and Category; 404 if not found; 400 if invalid category; 204 on success; Id unchanged.
3. **GET /books/category/{category}** — Returns books in category; case-insensitive; empty list if none.
4. **POST /books** — Duplicate title (case-insensitive) → 400 Bad Request.

---

## Loan system — Base path: `/Loans`

| Method | Endpoint | Summary | Responses |
|--------|----------|---------|-----------|
| **GET** | `/Loans` | Get all loans (active and returned) | 200 OK → `List<LoanModel>` |
| **GET** | `/Loans/{id}` | Get a single loan by id | 200 OK → `LoanModel` · 404 Not Found |
| **POST** | `/Loans` | Loan a book (create loan) | 200 OK → `LoanModel` · 400 Bad Request (book already on loan) · 404 Not Found (book not found) |
| **POST** | `/Loans/{id}/return` | Return a book (mark loan as returned) | 204 No Content · 400 Bad Request (already returned) · 404 Not Found |

### LoanModel
| Property | Type |
|----------|------|
| `id` | integer |
| `bookId` | integer |
| `borrowerName` | string |
| `loanedAt` | string (date-time) |
| `returnedAt` | string (date-time) or null (null = still on loan) |

### LoanRequest (body for POST /Loans)
| Property | Type |
|----------|------|
| `bookId` | integer |
| `borrowerName` | string |

---

To try the API in Swagger UI: run the project and open **http://localhost:5180/swagger** (or the URL from your launch profile). Books and **Loans** will both appear in Swagger.
