﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SchoolMonitoringSystem.Api.Filters;
using SchoolMonitoringSystem.Application.UseCases;
using X.PagedList;

namespace SchoolMonitoringSystem.Api.Controllers
{
    public class SubjectController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> CreateSubject()
        {
            var teachers = await Mediator.Send(new GetAllTeacherQuery());
            ViewData["teachers"] = teachers;
            return View();
        }
        [HttpPost]
        [ActionModelValidationAttribute]
        public async Task<IActionResult> CreateSubject(CreateSubjectCommand subjectCommand)
        {
            var result = await Mediator.Send(subjectCommand);
            return RedirectToAction(nameof(GetAllSubjects));
        }
        [HttpGet]
        [LazyCache(2, 5)]
        [EnableRateLimiting("Token")]
        public async ValueTask<IActionResult> GetAllSubjects(int page = 1)
        {
            ViewData["teachers"] = await Mediator.Send(new GetAllTeacherQuery());
            IPagedList<SubjectDto> query = (await Mediator
                .Send(new GetAllSubjectQuery()))
                .ToPagedList(page, 5);
            return View(query);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSubject([FromForm] UpdateSubjectCommand subjectCommand)
        {
            var result = await Mediator.Send(subjectCommand);
            return RedirectToAction(nameof(GetAllSubjects));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSubject(DeleteSubjectCommand subjectCommand)
        {
            var result = await Mediator.Send(subjectCommand);
            return RedirectToAction(nameof(GetAllSubjects));
        }
    }
}
