﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.WebUI.AppCode.Extensions;
using Project.WebUI.Business.ProfessionModule;
using Project.WebUI.Business.RegisterModule;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(RegisterPagedQuery query)
        {
            var response = await mediator.Send(query);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListBody", response);
            }

            return View(response);
        }
        public async Task<IActionResult> Create()
        {
            var professions = await mediator.Send(new ProfessionsAllQuery());
            ViewBag.ProfessionId = new SelectList(professions, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            var professions = await mediator.Send(new ProfessionsAllQuery());
            ViewBag.ProfessionId = new SelectList(professions, "Id", "Name",command.ProfessionId);
            return View(command);

        }
        public async Task<IActionResult> Details(RegisterSingleQuery query)
        {
            var response = await mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return View(response);
        }

        public async Task<IActionResult> Edit(RegisterSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            var professions = await mediator.Send(new ProfessionsAllQuery());
            ViewBag.ProfessionId = new SelectList(professions, "Id", "Name",response.ProfessionId);
            var command = new RegisterEditCommand();
            command.Name = response.Name;
            command.Surname = response.Surname;
            command.UserName = response.UserName;
            command.UserPassword = response.UserPassword;
            command.RegisterDate = response.RegisterDate;
            return View(command);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterEditCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                var professions = await mediator.Send(new ProfessionsAllQuery());
                ViewBag.ProfessionId = new SelectList(professions, "Id", "Name", response.ProfessionId);
                return View(command);
            }

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Remove(RegisterRemoveCommand command)
        {
            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }
            var newQuery = new RegisterPagedQuery
            {
                PageIndex = command.PageIndex,
                PageSize = command.PageSize
            };
            var data = await mediator.Send(newQuery);
            return PartialView("_ListBody", data);
        }
    }
}
