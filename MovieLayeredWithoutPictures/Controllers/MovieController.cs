using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieLayeredWithoutPictures.BLL.DTO;
using MovieLayeredWithoutPictures.BLL.Interfaces;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace MovieLayeredWithoutPictures.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET запрос для отображения всех фильмов в списке
        public async Task<IActionResult> Index()
        {
            return View(await _movieService.GetMovies());
        }

        // GET запрос для отображения деталей конкретного выбранного фильма
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                MovieDTO movie = await _movieService.GetMovie((int)id);
                return View(movie);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET запрос для отображения формы создания нового фильма
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST запрос для создания нового фильма в списке        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieDTO movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateMovie(movie);
                return View("~/Views/Movie/Index.cshtml", await _movieService.GetMovies());
            }

            return View(movie);
        }

        // GET запрос на отображение формы редактирования фильма
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                MovieDTO movie = await _movieService.GetMovie((int)id);
                return View(movie);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // GET запрос на редактирования фильма
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieDTO movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _movieService.UpdateMovie(movie);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(movie);
        }

        // GET запрос формы для удаления фильма
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                MovieDTO movie = await _movieService.GetMovie((int)id);
                return View(movie);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST запрос для удаления фильма
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteMovie(id);
            return View("~/Views/Movie/Index.cshtml", await _movieService.GetMovies());
        }

        // Вспомогательный метод для проверки существования фильма
        private async Task<bool> MovieExists(int id)
        {
            var movie = await _movieService.GetMovie(id);
            return movie != null;
        }
    }
}
