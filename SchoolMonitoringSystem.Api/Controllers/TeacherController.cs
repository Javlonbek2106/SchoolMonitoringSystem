using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolMonitoringSystem.Api.Filters;
using SchoolMonitoringSystem.Application.UseCases;
using X.PagedList;

namespace SchoolMonitoringSystem.Api.Controllers
{
    public class TeacherController : ApiBaseController
    {
        [HttpGet]
        public IActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost]
        [ActionModelValidationAttribute]
        public async Task<IActionResult> CreateTeacher(CreateTeacherCommand teacherCommand)
        {
            await Mediator.Send(teacherCommand);
            return RedirectToAction(nameof(GetAllTeachers));
        }
        [HttpGet]
        public async Task<IActionResult> GetByTeacherId(GetTeacherQuery teacherQuery)
        {
            return View(await Mediator.Send(teacherQuery));
        }

        [HttpGet]
        [LazyCache(2, 5)]
        [EnableRateLimiting("Token")]
        public async Task<IActionResult> GetAllTeachers(int page = 1)
        {
            ViewData["teachers"] = await Mediator.Send(new GetAllTeacherQuery());
            IPagedList<TeacherDto> query = (await Mediator
                .Send(new GetAllTeacherQuery()))
                .ToPagedList(page, 5);
            return View(query);
        }

        [HttpGet]
        public IActionResult UpdateTeacher(TeacherDto teacherDto)
        {
            return View(teacherDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeacher([FromForm] UpdateTeacherCommand teacherCommand)
        {
            var result = await Mediator.Send(teacherCommand);
            return RedirectToAction(nameof(GetAllTeachers));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeacher(DeleteTeacherCommand teacherCommand)
        {
            var result = await Mediator.Send(teacherCommand);
            return RedirectToAction(nameof(GetAllTeachers));
        }
    }
}
