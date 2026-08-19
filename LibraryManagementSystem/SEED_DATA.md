# Data Seeding Documentation

This repository includes a pre-configured data seeding setup via the `SeedData()` method in the `Library` service. The seed data populates the system with initial books, members, and active borrowing records. This provides a ready-to-test environment for evaluation without manual data entry.

---

## 📚 1. Books Seed Data

All seeded books are domain-relevant to **Computer Science & Software Engineering**:

| ID | Title | Author(s) | Genre | Publication Year | Initial Status |
|---|---|---|---|---|---|
| **1** | Clean Code | Robert C. Martin | Software Engineering | 2008 | Borrowed |
| **2** | Design Patterns | Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides | Software Architecture | 1994 | Borrowed |
| **3** | The Pragmatic Programmer | Andrew Hunt, David Thomas | Software Engineering | 1999 | Borrowed |
| **4** | Introduction to Algorithms | Thomas H. Cormen | Computer Science | 2009 | Borrowed |
| **5** | CLR via C# | Jeffrey Richter | Programming Languages | 2012 | Available |
| **6** | Refactoring | Martin Fowler | Software Engineering | 1999 | Available |

---

## 👤 2. Members Seed Data

The seeded members cover both regular and premium membership tiers:

| ID | Full Name | Email Address | Membership Tier | Borrowing Limit | Loan Period |
|---|---|---|---|---|---|
| **1** | Ahmed Mahmoud | `ahmed.mahmoud@example.com` | Regular Member | 10 Books | 14 Days |
| **2** | Mohamed Mostafa | `mohamed.m@example.com` | Regular Member | 10 Books | 14 Days |
| **3** | Mariam Ibrahim | `mariam.ibrahim@example.com` | Regular Member | 10 Books | 14 Days |
| **4** | Sara Hassan | `sara.hassan@example.com` | **Premium Member** | 10 Books | 30 Days |
| **5** | Omar Khalid | `omar.khalid@example.com` | **Premium Member** | 10 Books | 30 Days |

---

## 📋 3. Borrowing Records & Edge Case Scenarios

The initial borrow records are pre-configured to test overdue calculation logic across different membership tiers:

| Record ID | Member Name | Book Title | Borrow Date | Days Passed | Tier Limit | Overdue Status |
|---|---|---|---|---|---|---|
| **1** | Ahmed Mahmoud *(Regular)* | Clean Code | 5 days ago | 5 days | 14 Days | 🟢 **Active / On Time** |
| **2** | Mohamed Mostafa *(Regular)* | Design Patterns | 20 days ago | 20 days | 14 Days | 🔴 **Overdue** (+6 days late) |
| **3** | Sara Hassan *(Premium)* | The Pragmatic Programmer | 20 days ago | 20 days | 30 Days | 🟢 **Active / On Time** |
| **4** | Omar Khalid *(Premium)* | Introduction to Algorithms | 40 days ago | 40 days | 30 Days | 🔴 **Overdue** (+10 days late) |

---

## 🛠️ 4. C# Implementation Snippet

Below is the structured implementation used inside the `Library` service:

```csharp
public void SeedData()
{
    // 1. Seed Computer Science Books
    var booksToSeed = new (string Title, string Author, string Genre, int Year)[]
    {
        ("Clean Code", "Robert C. Martin", "Software Engineering", 2008),
        ("Design Patterns", "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides", "Software Architecture", 1994),
        ("The Pragmatic Programmer", "Andrew Hunt, David Thomas", "Software Engineering", 1999),
        ("Introduction to Algorithms", "Thomas H. Cormen", "Computer Science", 2009),
        ("CLR via C#", "Jeffrey Richter", "Programming Languages", 2012),
        ("Refactoring", "Martin Fowler", "Software Engineering", 1999)
    };

    foreach (var b in booksToSeed)
    {
        AddBook(b.Title, b.Author, b.Genre, b.Year);
    }

    // 2. Seed Members (Regular & Premium)
    var membersToSeed = new (string Name, string Email, bool IsPremium)[]
    {
        ("Ahmed Mahmoud", "ahmed.mahmoud@example.com", false),
        ("Mohamed Mostafa", "mohamed.m@example.com", false),
        ("Mariam Ibrahim", "mariam.ibrahim@example.com", false),
        ("Sara Hassan", "sara.hassan@example.com", true),
        ("Omar Khalid", "omar.khalid@example.com", true)
    };

    foreach (var m in membersToSeed)
    {
        RegisterMember(m.Name, m.Email, m.IsPremium);
    }

    // 3. Seed Borrowing Records to test Overdue Calculations
    var borrowsToSeed = new (int MemberId, int BookId, DateTime BorrowDate)[]
    {
        (1, 1, DateTime.Now.AddDays(-5)),  // Regular member - On time
        (2, 2, DateTime.Now.AddDays(-20)), // Regular member - Overdue (> 14 days)
        (4, 3, DateTime.Now.AddDays(-20)), // Premium member - On time (< 30 days)
        (5, 4, DateTime.Now.AddDays(-40))  // Premium member - Overdue (> 30 days)
    };

    foreach (var br in borrowsToSeed)
    {
        BorrowBook(br.MemberId, br.BookId, br.BorrowDate);
    }
}
```

---

## 🧪 5. Suggested Test Menu Options for Evaluation

When testing the application via the console interface, the following menu features can be verified immediately using the seeded dataset:

1. **Option 6 (Display Available Books):** Shows `CLR via C#` and `Refactoring` as available.
2. **Option 8 (Late Return Report):** Displays overdue items for **Mohamed Mostafa** and **Omar Khalid**.
3. **Option 5 (Search Catalog):** Search for `"Clean"` or `"Sara"` to test search matching across books and members.
4. **Option 7 (Member Borrow History):** Input `Member ID: 4` or `Member ID: 2` to verify individual history records.
