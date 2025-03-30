# MauiFilmLibrary

## Описание функционала

**MauiFilmLibrary** - это приложение для поиска фильмов из библиотеки. Основные функции включают:

- Поиск фильмов по названию, имени актеров и жанров.
- Просмотр подробной информации о фильмах.

### Пользовательский интерфейс

Приложение состоит из нескольких экранов:
- **Главный экран** - Поиск фильмов и просмотр списка фильмов по кретерию.
- **Страница фильма** - Страница с деталими выбранного фильма.

## Состав проекта

* MauiFilmLibrary - Основное Maui решение 
* MauiFilmLibrary.Models - Библиотека классов обеспечивающая поддержку Entity framework
* MovieServiceTests - Решение содержащая моки и тесты для приложения

## Sql запросы

linq выражение отвечающее за поиск фильма (поиск фильма по id для дальнейшего открытия во view)

```c#
var movieData = await _dbContext.Movies
    .AsNoTracking()
    .Where(m => m.MovieId == movieId)
    .Select(m => new
    {
        m.MovieId,
        m.Title,
        m.Description,
        m.ReleaseYear,
        m.TitleImg,
        Genres = m.GenreMovies
            .Select(gm => gm.Genre.GenreName)
            .ToList(),
        Persons = m.MoviePersonRoles
            .Select(mp => new
            {
                mp.Person.PersonName,
                mp.Role.RoleName
            })
            .ToList()
    })
    .FirstOrDefaultAsync();
```

Экаивалентен запросу к бд

```sql
SELECT
    m.movie_id,
    m.Title,
    m.Description,
    m.release_year,
    m.title_img,
    (SELECT GROUP_CONCAT(g.genre_name)
     FROM genre_movie gm
     JOIN genre g ON gm.gener_id  = g.genre_id
     WHERE gm.movie_id = m.movie_id) AS Genres,
    (SELECT GROUP_CONCAT( CONCAT( p.person_name, ' (', r.role_name,')'))
     FROM movie_person_role mpr
     JOIN person p ON mpr.person_id = p.person_id
     JOIN role r ON mpr.role_id = r.role_id
     WHERE mpr.movie_id = m.movie_id) AS Persons
FROM movie m
WHERE m.movie_id = 1;
```
linq выражение отвечающие за нахождения фильмов по названию, имени актеров и жанру
```c#
var moviesData = await _dbContext.Movies
    .AsNoTracking()
    .Include(m => m.GenreMovies)
        .ThenInclude(gm => gm.Genre)
    .Include(m => m.MoviePersonRoles)
        .ThenInclude(mpr => mpr.Person)
    .Include(m => m.MoviePersonRoles)
        .ThenInclude(mpr => mpr.Role)
    .Where(m => searchTerms.Any(term =>
        EF.Functions.Like(m.Title, "%" + term + "%") ||
        m.MoviePersonRoles.Any(mpr => EF.Functions.Like(mpr.Person.PersonName, "%" + term + "%")) ||
        m.GenreMovies.Any(gm => EF.Functions.Like(gm.Genre.GenreName, "%" + term + "%"))
    ))
    .Take(limit)
    .Select(m => new
    {
        m.MovieId,
        m.Title,
        Genres = m.GenreMovies
            .Select(gm => gm.Genre.GenreName)
            .ToList(),
        Persons = m.MoviePersonRoles
            .Select(p => new
            {
                p.Person.PersonName,
                RoleName = p.Role.RoleName
            })
            .ToList()
    })
    .ToListAsync();
```
Экаивалентен запросу к бд

```sql
SELECT m.movie_id, m.title,
GROUP_CONCAT(g.genre_name) AS Genres,
GROUP_CONCAT(p.person_name || ' (' || r.role_name || ')') AS Persons
FROM movie m
LEFT JOIN movie_person_role mpr ON m.movie_id = mpr.movie_id
LEFT JOIN person p ON mpr.person_id = p.person_id
LEFT JOIN role r ON mpr.role_id = r.role_id
LEFT JOIN genre_movie gm ON m.movie_id = gm.movie_id
LEFT JOIN genre g ON gm.gener_id  = g.genre_id
WHERE LOWER(m.title) LIKE '%matrix%'
GROUP BY m.movie_id, m.title
LIMIT 1;
```

## Порядок развертывания

### 1. Предварительные требования

- установка пакета .NET SDK 8.0. и visual studio
- установка SQLite для работы с бд.

### 2. Установка зависимостей

установка пакетов 

```bash
dotnet restore
```

## Тестирование 
```bash
dotnet test
```
