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
            ////var model = await _repository.GetAll();

            //var model = await _movieService.GetMovies();
            //return View(model);

            //return View(await _movieContext.Movies.ToArrayAsync());
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

            ////////////////////////////////////////////
            // работало в предыдущем задании, на репозиторий
            // 			
            //if (id == null || await _repository.GetAll() == null)
            //{
            //    return NotFound();
            //}

            //var movie = await _repository.GetById((int)id);

            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return View(movie);

            ////////////////////////////////////////////

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var movie = await _movieContext.Movies.FirstOrDefaultAsync(item => item.Id == id);

            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return View(movie);
        }

        // GET запрос для отображения формы создания нового фильма
        [HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.ListMovies = new SelectList(await _movieService.GetMovies(), "Id", "Title");
        //    return View();
        //}
        public IActionResult Create()
        {
            return View();
        }


        // POST запрос для создания нового фильма в списке        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(MovieDTO movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateMovie(movie);
                return View("~/Views/Movie/Index.cshtml", await _movieService.GetMovies());
            }
            return View(movie);
        }


        //public async Task<IActionResult> Create([Bind("Id,Title,Director,Genre,ReleaseYear,Description")] MovieDTO movie, IFormFile uploadedFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _movieService.CreateMovie(movie);
        //        return View("~/Views/Player/Index.cshtml", await _movieService.GetMovies());
        //    }

        //    ViewBag.ListTeams = new SelectList(await _movieService.GetMovies(), "Id", "Name");
        //    return View(movie);


        //    //if (ModelState.IsValid)
        //    //{
        //    //	if (uploadedFile != null && uploadedFile.Length > 0)
        //    //	{
        //    //		movie.PosterPath = await UploadPicture(uploadedFile);
        //    //	}

        //    //	// Добавляем фильм в репозиторий
        //    //	await _repository.Create(movie);

        //    //	// Сохраняем изменения в базе данных
        //    //	await _repository.Save();

        //    //	// Перенаправляем на страницу списка фильмов
        //    //	return RedirectToAction(nameof(Index));
        //    //}

        //    //// Если модель не валидна, возвращаем представление с текущими данными фильма
        //    //return View(movie);
        //}

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

            //if (ModelState.IsValid)
            //{
            //    await _movieService.UpdateMovie(movie);
            //    return View("~/Views/Movie/Index.cshtml", await _movieService.GetMovies());
            //}
            //return View(movie);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

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
        
        ///////////////////////////////////////////////////////////////////////////////////////////

        // Вспомогательный метод для проверки существования фильма
        private async Task<bool> MovieExists(int id)
        {
            var movie = await _movieService.GetMovie(id);
            return movie != null;

            //List<Movie> list = await _repository.GetAll();
            //return (list?.Any(m => m.Id == id)).GetValueOrDefault();

            //return _movieContext.Movies.Any(item => item.Id == id);
        }        
    }
}
