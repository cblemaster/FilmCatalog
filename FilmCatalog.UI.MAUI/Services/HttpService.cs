﻿using FilmCatalog.UI.MAUI.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace FilmCatalog.UI.MAUI.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;
        private const string BASE_URI = "https://localhost:7157";

        public HttpService() => _client = new HttpClient
        {
            BaseAddress = new Uri(BASE_URI)
        };

        public async void AssignActorsToFilmAsync(int filmId, ICollection<DisplayActor> actors)
        {
            if (actors is null || !actors.Any() || filmId < 1) { return; }

            StringContent content = new(JsonSerializer.Serialize(actors));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PutAsync($"/film/{filmId}/assignactors", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async void AssignCategoriesToFilmAsync(int filmId, ICollection<DisplayCategory> categories)
        {
            if (categories is null || !categories.Any() || filmId < 1) { return; }

            StringContent content = new(JsonSerializer.Serialize(categories));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PutAsync($"/film/{filmId}/assigncategories", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayActor?> CreateActorAsync(CreateActor actor)
        {
            if (!actor.Validate().IsValid) { return null; }

            StringContent content = new(JsonSerializer.Serialize(actor));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync("/actor", content);
                response.EnsureSuccessStatusCode();
                return await GetActorAsync(await response.Content.ReadFromJsonAsync<DisplayActor?>() is DisplayActor newActor ? newActor.ActorId : 0);
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayCategory?> CreateCategoryAsync(CreateCategory category)
        {
            if (!category.Validate().IsValid) { return null; }

            StringContent content = new(JsonSerializer.Serialize(category));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync("/category", content);
                response.EnsureSuccessStatusCode();
                return await GetCategoryAsync(await response.Content.ReadFromJsonAsync<DisplayCategory>() is DisplayCategory newCategory ? newCategory.CategoryId : 0);
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayDirector?> CreateDirectorAsync(CreateDirector director)
        {
            if (!director.Validate().IsValid) { return null; }

            StringContent content = new(JsonSerializer.Serialize(director));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync("/director", content);
                response.EnsureSuccessStatusCode();
                return await GetDirectorAsync(await response.Content.ReadFromJsonAsync<DisplayDirector>() is DisplayDirector newDirector ? newDirector.DirectorId : 0);
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayFilm?> CreateFilmAsync(CreateFilm film)
        {
            if (!film.Validate().IsValid) { return null; }

            StringContent content = new(JsonSerializer.Serialize(film));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync("/film", content);
                response.EnsureSuccessStatusCode();
                return await GetFilmAsync(await response.Content.ReadFromJsonAsync<DisplayFilm>() is DisplayFilm newFilm ? newFilm.FilmId : 0);
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayFormat?> CreateFormatAsync(CreateFormat format)
        {
            if (!format.Validate().IsValid) { return null; }

            StringContent content = new(JsonSerializer.Serialize(format));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PostAsync("/format", content);
                response.EnsureSuccessStatusCode();
                return await GetFormatAsync(await response.Content.ReadFromJsonAsync<DisplayFormat>() is DisplayFormat newFormat ? newFormat.FormatId : 0);
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteActorAsync(int actorId)
        {
            if (actorId < 1) { return; }

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"/actor/{actorId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            if (categoryId < 1) { return; }

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"/category/{categoryId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteDirectorAsync(int directorId)
        {
            if (directorId < 1) { return; }

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"/director/{directorId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async void DeleteFilmAsync(int filmId)
        {
            if (filmId < 1) { return; }

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"/film/{filmId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteFormatAsync(int formatId)
        {
            if (formatId < 1) { return; }

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"/format/{formatId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayActor> GetActorAsync(int actorId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"/actor/{actorId}");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? await response.Content.ReadFromJsonAsync<DisplayActor>() ?? DisplayActor.NotFound
                    : DisplayActor.NotFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayActor>> GetActorsAsync()
        {
            try
            {
                ReadOnlyCollection<DisplayActor> noneFound = new List<DisplayActor>() { DisplayActor.NotFound }.AsReadOnly();

                HttpResponseMessage response = await _client.GetAsync("/actor");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    ReadOnlyCollection<DisplayActor?>? actors = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayActor>().ToBlockingEnumerable().ToList().AsReadOnly();
                    return actors is null || !actors.Any(a => a?.GetType() == typeof(DisplayActor)) ? noneFound : actors!;
                }
                return noneFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayCategory> GetCategoryAsync(int categoryId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"/category/{categoryId}");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? await response.Content.ReadFromJsonAsync<DisplayCategory>() ?? DisplayCategory.NotFound
                    : DisplayCategory.NotFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayCategory>> GetCategoriesAsync()
        {
            try
            {
                ReadOnlyCollection<DisplayCategory> noneFound = new List<DisplayCategory>() { DisplayCategory.NotFound }.AsReadOnly();

                HttpResponseMessage response = await _client.GetAsync("/category");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    ReadOnlyCollection<DisplayCategory?>? categories = response.Content.ReadFromJsonAsAsyncEnumerable< DisplayCategory>().ToBlockingEnumerable().ToList().AsReadOnly();
                    return categories is null || !categories.Any(a => a?.GetType() == typeof(DisplayCategory)) ? noneFound : categories!;
                }
                return noneFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayDirector> GetDirectorAsync(int directorId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"/director/{directorId}");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? await response.Content.ReadFromJsonAsync<DisplayDirector>() ?? DisplayDirector.NotFound
                    : DisplayDirector.NotFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayDirector>> GetDirectorsAsync()
        {
            try
            {
                ReadOnlyCollection<DisplayDirector> noneFound = new List<DisplayDirector>() { DisplayDirector.NotFound }.AsReadOnly();

                HttpResponseMessage response = await _client.GetAsync("/director");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    ReadOnlyCollection<DisplayDirector?>? directors = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayDirector>().ToBlockingEnumerable().ToList().AsReadOnly();
                    return directors is null || !directors.Any(a => a?.GetType() == typeof(DisplayDirector)) ? noneFound : directors!;
                }
                return noneFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayFilm> GetFilmAsync(int filmId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"/film/{filmId}");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? await response.Content.ReadFromJsonAsync<DisplayFilm>() ?? DisplayFilm.NotFound
                    : DisplayFilm.NotFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayFilm>> GetFilmsAsync()
        {
            try
            {
                ReadOnlyCollection<DisplayFilm> noneFound = new List<DisplayFilm>() { DisplayFilm.NotFound }.AsReadOnly();

                HttpResponseMessage response = await _client.GetAsync("/film");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    ReadOnlyCollection<DisplayFilm?>? films = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm>().ToBlockingEnumerable().ToList().AsReadOnly();
                    return films is null || !films.Any(a => a?.GetType() == typeof(DisplayFilm)) ? noneFound : films!;
                }
                return noneFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayFilm?>> GetFavoriteFilmsAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("/film/favorite");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm?>().ToBlockingEnumerable().ToList().AsReadOnly() ?? new List<DisplayFilm?> { DisplayFilm.NotFound }.AsReadOnly()
                    : new List<DisplayFilm?> { DisplayFilm.NotFound }.AsReadOnly();
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayFilm?>> GetRareFilmsAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("/film/rare");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm?>().ToBlockingEnumerable().ToList().AsReadOnly() ?? new List<DisplayFilm?> { DisplayFilm.NotFound }.AsReadOnly()
                    : new List<DisplayFilm?> { DisplayFilm.NotFound }.AsReadOnly();
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayFilm?>> GetFivestarFilmsAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("/film/fivestar");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm?>().ToBlockingEnumerable().ToList().AsReadOnly() ?? new List<DisplayFilm?> { DisplayFilm.NotFound }.AsReadOnly()
                    : new List<DisplayFilm?> { DisplayFilm.NotFound }.AsReadOnly();
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByActorAsync()
        {
            //ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>> returnThisOnError = Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, actorGroup) => actorGroup.ToList().AsReadOnly()).ToList().AsReadOnly();

            try
            {
                HttpResponseMessage response = await _client.GetAsync("/film");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    IEnumerable<DisplayFilm?> films = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm>().ToBlockingEnumerable();
                    return films.GroupBy(f => f?.Actors.Select(a => a.Name), (key, actorGroup) => actorGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
                else
                {
                    return Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, actorGroup) => actorGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByCategoryAsync()
        {
            //ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>> returnThisOnError = Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, categoryGroup) => categoryGroup.ToList().AsReadOnly()).ToList().AsReadOnly();

            try
            {
                HttpResponseMessage response = await _client.GetAsync("/film");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    IEnumerable<DisplayFilm?> films = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm>().ToBlockingEnumerable();
                    return films.GroupBy(f => f?.Categories.Select(c => c.CategoryName), (key, categoryGroup) => categoryGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
                else
                {
                    return Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, categoryGroup) => categoryGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByDirectorAsync()
        {
            //ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>> returnThisOnError =  Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, directorGroup) => directorGroup.ToList().AsReadOnly()).ToList().AsReadOnly();

            try
            {
                HttpResponseMessage response = await _client.GetAsync("/film");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    IEnumerable<DisplayFilm?> films = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm>().ToBlockingEnumerable();
                    return films.GroupBy(f => f?.Director is null ? "No director specified" : f.Director.Name, (key, directorGroup) => directorGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
                else
                {
                    return Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, directorGroup) => directorGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByFormatAsync()
        {
            //ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>> returnThisOnError = Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, formatGroup) => formatGroup.ToList().AsReadOnly()).ToList().AsReadOnly();

            try
            {
                HttpResponseMessage response = await _client.GetAsync("/film");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    IEnumerable<DisplayFilm?> films = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFilm>().ToBlockingEnumerable();
                    return films.GroupBy(f => f?.Format.FormatName, (key, formatGroup) => formatGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
                else
                {
                    return Enumerable.GroupBy(Enumerable.Empty<DisplayFilm?>(), f => string.Empty, (key, formatGroup) => formatGroup.ToList().AsReadOnly()).ToList().AsReadOnly();
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<DisplayFormat> GetFormatAsync(int formatId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"/format/{formatId}");
                return response.IsSuccessStatusCode && response.Content is not null
                    ? await response.Content.ReadFromJsonAsync<DisplayFormat>() ?? DisplayFormat.NotFound
                    : DisplayFormat.NotFound;
            }
            catch (Exception) { throw; }
        }

        public async Task<ReadOnlyCollection<DisplayFormat>> GetFormatsAsync()
        {
            try
            {
                ReadOnlyCollection<DisplayFormat> noneFound = new List<DisplayFormat>() { DisplayFormat.NotFound }.AsReadOnly();

                HttpResponseMessage response = await _client.GetAsync("/format");
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    ReadOnlyCollection<DisplayFormat?>? formats = response.Content.ReadFromJsonAsAsyncEnumerable<DisplayFormat>().ToBlockingEnumerable().ToList().AsReadOnly();
                    return formats is null || !formats.Any(a => a?.GetType() == typeof(DisplayFormat)) ? noneFound : formats!;
                }
                return noneFound;
            }
            catch (Exception) { throw; }
        }

        public async void RemoveActorsFromFilmAsync(int filmId, ICollection<DisplayActor> actors)
        {
            if (actors is null || !actors.Any() || filmId < 1) { return; }

            StringContent content = new(JsonSerializer.Serialize(actors));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PutAsync($"/film/{filmId}/removeactors", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async void RemoveCategoriesFromFilmAsync(int filmId, ICollection<DisplayCategory> categories)
        {
            if (categories is null || !categories.Any() || filmId < 1) { return; }

            StringContent content = new(JsonSerializer.Serialize(categories));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PutAsync($"/film/{filmId}/removecategories", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task RenameActorAsync(int actorId, RenameActor actor)
        {
            if (!actor.Validate().IsValid) { return; }

            StringContent content = new(JsonSerializer.Serialize(actor));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PutAsync($"/actor/{actorId}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task RenameDirectorAsync(int directorId, RenameDirector director)
        {
            if (!director.Validate().IsValid) { return; }

            StringContent content = new(JsonSerializer.Serialize(director));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PutAsync($"/director/{directorId}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateFilmAsync(int filmId, UpdateFilm film)
        {
            if (!film.Validate().IsValid) { return; }

            StringContent content = new(JsonSerializer.Serialize(film));
            content.Headers.ContentType = new("application/json");

            try
            {
                HttpResponseMessage response = await _client.PutAsync($"/film/{filmId}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception) { throw; }
        }
    }
}
