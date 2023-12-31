﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolMonitoringSystem.Api.Filters;
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
        [ActionModelValidationAttribute]
        public async Task<IActionResult> CreateStudent(CreateStudentCommand studentCommand)
        {
            await Mediator.Send(studentCommand);
			return RedirectToAction(nameof(GetAllStudents));
		}
        [HttpGet]
        public async Task<IActionResult> GetByStudentId(GetStudentQuery studentQuery)
        {
            var subjects = await Mediator.Send(new GetAllSubjectQuery());
            ViewData["subjects"] = subjects;
            return View(await Mediator.Send(studentQuery));
        }
        [HttpGet]
        [LazyCache(2, 5)]
        [EnableRateLimiting("Token")]
        public async ValueTask<IActionResult> GetAllStudents(int page = 1)
        {
            IPagedList<StudentDto> query = (await Mediator
                .Send(new GetAllStudentQuery()))
                .ToPagedList(page, 5);
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
