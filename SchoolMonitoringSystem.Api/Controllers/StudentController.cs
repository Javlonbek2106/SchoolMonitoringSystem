using Microsoft.AspNetCore.Mvc;
using SchoolMonitoringSystem.Application.UseCases;
using X.PagedList;

namespace SchoolMonitoringSystem.Api.Controllers
{
    public class StudentController : ApiBaseController
    {
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentCommand studentCommand)
        {
            await Mediator.Send(studentCommand);
			return RedirectToAction(nameof(GetAllStudents));
		}
        [HttpGet]
        //[LazyCache(5, 10)]
        public async Task<IActionResult> GetByStudentId(GetStudentQuery studentQuery)
        {
            var subjects = await Mediator.Send(new GetAllSubjectQuery());
            ViewData["subjects"] = subjects;
            return View(await Mediator.Send(studentQuery));
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAllStudents(int page = 1)
        {
            IPagedList<StudentDto> query = (await Mediator
                .Send(new GetAllStudentQuery()))
                .ToPagedList(page, 10);
            return View(query);
        }
        [HttpGet]
        public IActionResult UpdateStudent(StudentDto studentDto)
        {
            return View(studentDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStudent([FromForm] UpdateStudentCommand studentCommand)
        {
            await Mediator.Send(studentCommand);
            return RedirectToAction(nameof(GetAllStudents));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(DeleteStudentCommand studentCommand)
        {
            var result = await Mediator.Send(studentCommand);
            return RedirectToAction(nameof(GetAllStudents));
        }
    }
}
