using Microsoft.AspNetCore.Mvc;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.UseCases;
using SchoolMonitoringSystem.Application.UseCases.Filters;

namespace SchoolMonitoringSystem.Api.Controllers
{
    public class FilterController : ApiBaseController
    {
        private readonly IApplicationDbContext dbContext;

        public FilterController(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> AgeStudentFilter(AgeStudentFilterQuery ageFilterQuery)
        {
            return View(await Mediator.Send(ageFilterQuery));
        }
        public async Task<IActionResult> DayMonthFilter(DayMonthFilterQuery dayMonthFilter)
        {
            return View(await Mediator.Send(dayMonthFilter));
        }
		public async Task<IActionResult> AgeTeacherFilter(AgeTeacherFilterQuery ageTeacherFilterQuery)
		{
			return View(await Mediator.Send(ageTeacherFilterQuery));
		}
        public async Task<IActionResult> IsBeelineStudentFilter(IsBeelineStudentFilterQuery isBeelineStudentFilter)
		{
			return View(await Mediator.Send(isBeelineStudentFilter));
		}
        public async Task<IActionResult> IsBeelineTeacherFilter(IsBeelineTeacherFilterQuery isBeelineTeacherFilter)
		{
			return View(await Mediator.Send(isBeelineTeacherFilter));
		}
        public async Task<IActionResult> SearchStudentFilter(SearchStudentFilterQuery searchStudentFilter)
		{
			return View(await Mediator.Send(searchStudentFilter));
		}
        public async Task<IActionResult> SearchTeacherFilter(SearchTeacherFilterQuery searchTeacherFilter)
		{
			return View(await Mediator.Send(searchTeacherFilter));
		}
        [HttpGet]
        public async Task<IActionResult> BestSubjectFilter()
        {
            ViewData["teachers"] = await Mediator.Send(new GetAllTeacherQuery());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BestSubjectsFilter(BestSubjectFilterQuery students)
        {
            ViewData["teachers"] = await Mediator.Send(new GetAllTeacherQuery());
            return View(await Mediator.Send(students));
        }
        public async Task<IActionResult> BestTeacherFilter(BestTeacherFilterQuery bestTeacherFilter)
		{
			return View(await Mediator.Send(bestTeacherFilter));
		}
	}
}
