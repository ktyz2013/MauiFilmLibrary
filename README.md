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

linq выражение отвечающий за поиск фильма 

```c#
public async Task<List<Movie>> SearchMoviesAsync(string searchQuery)
{
    var searchTerms = searchQuery.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    return await _dbContext.Movies
        .Include(m => m.Geners)
        .Include(m => m.MoviePersonRoles)
            .ThenInclude(mpr => mpr.Person)
        .Where(m => searchTerms.All(term =>
            m.Title.ToLower().Contains(term) ||
            m.MoviePersonRoles.Any(mpr => mpr.Person.PersonName.ToLower().Contains(term)) ||
            m.Geners.Any(g => g.GenreName.ToLower().Contains(term))))
        .ToListAsync();
}
```

Экаивалентен запросу к бд

```sql
SELECT DISTINCT m.*
FROM movie m
LEFT JOIN movie_person_role mpr ON m.movie_id = mpr.movie_id
LEFT JOIN person p ON mpr.person_id = p.person_id
LEFT JOIN genre_movie mg ON m.movie_id = mg.movie_id
LEFT JOIN genre g ON mg.gener_id = g.genre_id
WHERE (
    LOWER(m.title) LIKE '%matrix%' OR
    LOWER(p.person_name) LIKE '%matrix%' OR
    LOWER(g.genre_name) LIKE '%matrix%'
)
OR (
    LOWER(m.title) LIKE '%neo%' OR
    LOWER(p.person_name) LIKE '%neo%' OR
    LOWER(g.genre_name) LIKE '%neo%'
);
```

В этом запросе к бд параметры вводились в ручную т.к. Sqlite не поддерживает DECLARE 

## Порядок развертывания

### 1. Предварительные требования

- установка пакета .NET SDK 8.0. и visual studio
- установка SQLite для работы с бд.

### 2. Установка зависимостей

установка пакетов 

```bash
dotnet restore
```

##Тестирование 
```bash
dotnet test
```
