using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolMonitoringSystem.Api.Filters;
using SchoolMonitoringSystem.Application;
using SchoolMonitoringSystem.Application.UseCases;
using X.PagedList;

namespace SchoolMonitoringSystem.Api.Controllers
{
    public class GradeController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> CreateGrade()
        {
            var students = await Mediator.Send(new GetAllStudentQuery());
            var subjects = await Mediator.Send(new GetAllSubjectQuery());
            ViewData["subjects"] = subjects;
            ViewData["students"] = students;
            return View();
        }
        [HttpPost]
        [ActionModelValidationAttribute]
        public async Task<IActionResult> CreateGrade(CreateGradeCommand gradeCommand)
        {
            var result = await Mediator.Send(gradeCommand);
            return RedirectToAction(nameof(GetAllGrades));
        }
        [HttpGet]
        //[LazyCache(5, 10)]
        public async Task<IActionResult> GetAllGrades(int page = 1)
        {
            ViewData["students"] = await Mediator.Send(new GetAllStudentQuery());
            ViewData["subjects"] = await Mediator.Send(new GetAllSubjectQuery());
            IPagedList<GradeDto> query = (await Mediator
                .Send(new GetAllGradeQuery()))
                .ToPagedList(page, 10);
            return View(query);
        }
        [HttpGet]
        public IActionResult UpdateGrade(GradeDto gradeDto)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGrade([FromForm] UpdateGradeCommand gradeCommand)
        {
            var result = await Mediator.Send(gradeCommand);
            return RedirectToAction(nameof(GetAllGrades));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGrade(DeleteGradeCommand gradeCommand)
        {
            var result = await Mediator.Send(gradeCommand);
            return RedirectToAction(nameof(GetAllGrades));
        }
    }
}
