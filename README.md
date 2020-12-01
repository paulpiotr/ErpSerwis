## ERP Serwis
### 1. Prosze o przygotowanie prostej aplikacji i przesłanie kodu oraz 
### 2. Zadanie polega na implementacji desktopowej aplikacji okienkowej w języku C#, która:
- Umożliwia dodanie do bazy informacji i studentach (Imię, Nazwisko, Numer indeksu, Data urodzenia)
- Umożliwia wyświetlenie informacji o studentach i wyszukanie studenta na podstawie Imienia, Nazwiska, Numeru indeksu, Daty urodzenia
- Umożliwia edytowanie informacji o studentach
- Umożliwia usunięcie studenta
- Umożliwia dodanie oceny dla studenta, który już został dodany do bazy (obsługa dodania oceny w nowym oknie)
- Może być użyte SQLite
- Jeśli wykorzystany jest MSSQL Server, to ConnectionString musi być w Properties oraz musi być dostarczony skrypt przygotowujący bazę
## Pobranie repozytorium z projektem
- Link z repozytorium znajduje sięna stronie: https://github.com/paulpiotr/ErpSerwis
- Link do repozytorium z wersjami binarnymi i instalatorem znajduje się na stronie: https://github.com/paulpiotr/ErpSerwis.WpfApp.Release
### Aby pobrać kody programu należy sklonować repozytorium .git:
```
git clone https://github.com/paulpiotr/ErpSerwis.git
```
### Projekt zawiera dołączone submoduły dlatego należy również je pobrać kożystając z polecenia:
```
git submodule sync --recursive && git submodule init && git submodule update && git submodule foreach git checkout master && git submodule foreach git pull origin master
```
### Skrypt SQL tworzący bazę danych Ms SQL
```
ALTER TABLE [esc].[StudentsGrades] DROP CONSTRAINT [FK_StudentsGrades_Students_StudentId]
GO
ALTER TABLE [esc].[StudentsGrades] DROP CONSTRAINT [DF__StudentsGrad__Id__23F3538A]
GO
ALTER TABLE [esc].[Students] DROP CONSTRAINT [DF__Students__Id__2116E6DF]
GO
/****** Object:  Table [esc].[StudentsGrades]    Script Date: 01.12.2020 14:13:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[esc].[StudentsGrades]') AND type in (N'U'))
DROP TABLE [esc].[StudentsGrades]
GO
/****** Object:  Table [esc].[Students]    Script Date: 01.12.2020 14:13:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[esc].[Students]') AND type in (N'U'))
DROP TABLE [esc].[Students]
GO
/****** Object:  Table [esc].[Students]    Script Date: 01.12.2020 14:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [esc].[Students](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Surname] [varchar](32) NOT NULL,
	[IndexNumber] [varchar](32) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [esc].[StudentsGrades]    Script Date: 01.12.2020 14:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [esc].[StudentsGrades](
	[Id] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[Grade] [money] NOT NULL,
 CONSTRAINT [PK_StudentsGrades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [esc].[Students] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [esc].[StudentsGrades] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [esc].[StudentsGrades]  WITH CHECK ADD  CONSTRAINT [FK_StudentsGrades_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [esc].[Students] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [esc].[StudentsGrades] CHECK CONSTRAINT [FK_StudentsGrades_Students_StudentId]
GO

```