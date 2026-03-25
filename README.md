# Dental Office Kartoteka

Desktop patient record system for a dental office, built with Windows Forms and Microsoft Access.

## Overview

Dental Office Kartoteka is a .NET Framework WinForms application for managing:
- patients,
- visit history,
- treatment details,
- and tooth chart data per visit.

The app stores data in a local Access database file and includes PDF export features for patient lists, patient details, and visit reports.

## Main Features

- Patient management
  - Create, edit, and delete patients.
  - Store personal and contact data (name, date of birth, phone, city, address, notes).
  - Duplicate-check when adding new patients (name + surname + date of birth).

- Fast search and navigation
  - Search patients by first name and last name.
  - Select a patient to instantly load visit history.

- Visit management
  - Add new visits with date, service, doctor, and detailed notes.
  - Edit existing visits.
  - View visit details by double-clicking a visit row.

- Tooth chart support
  - Create tooth chart data while adding a visit.
  - Re-open and edit chart data for existing visits.
  - Tooth chart string is saved in the database per visit.

- PDF and export tools
  - Export patient list to PDF.
  - Export a single visit report to PDF.
  - Export complete patient visit history to PDF or TXT.
  - Export full patient profile data to PDF.

- Runtime safeguards
  - Single-instance app startup (prevents multiple app instances).
  - Startup check for required database file in output folder.

## Tech Stack

- Language: C#
- UI: Windows Forms (.NET Framework 4.7.2)
- Database: Microsoft Access (.accdb) via OleDb
- PDF generation: iText 9.x
- UI libraries:
  - Guna.UI2.WinForms
  - Siticone.Desktop.UI

## Project Structure

- `StomatoloskaOrdinacijaKartoteka.sln` - Visual Studio solution
- `StomatoloskaOrdinacijaKartoteka/` - main WinForms project
- `StomatoloskaOrdinacijaKartoteka/DataBase.accdb` - local database file
- `Setup/` - setup/deployment project
- `packages/` - NuGet packages folder

## Prerequisites

- Windows OS (WinForms .NET Framework application)
- Visual Studio 2019/2022 with .NET desktop development workload
- .NET Framework 4.7.2 targeting pack
- Microsoft Access Database Engine (ACE OLEDB provider)
  - The app uses provider: `Microsoft.ACE.OLEDB.16.0`

## Getting Started

1. Clone the repository.
2. Open `StomatoloskaOrdinacijaKartoteka.sln` in Visual Studio.
3. Restore NuGet packages (if not restored automatically).
4. Build the solution.
5. Run the main project `StomatoloskaOrdinacijaKartoteka`.

At startup, the app expects `DataBase.accdb` to exist in the executable folder.
The project is configured to copy it automatically to output.

## Database Notes

The application primarily uses two tables:
- `Osoba` (patient data)
- `Poseta` (visit data, linked to patient)

Visit records include a `RadjeniZubi` field used for tooth chart data.

## Important Notes

- This is a classic Windows desktop app and is not cross-platform.
- If startup fails with database/provider errors, verify that:
  - ACE OLEDB is installed,
  - architecture settings match your environment,
  - `DataBase.accdb` exists in the output directory.

## Future Improvements (Suggested)

- Add unit/integration tests for core data workflows.
- Add migration support from Access to SQL Server.
- Add structured logging and centralized error reporting.
- Improve input validation consistency across forms.

## License

No root license file is currently provided in this repository.
Please add a `LICENSE` file if you plan to distribute this project publicly.
