﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Presentation.Abstractions;
using System.Security.Claims;

namespace PawFund.Presentation.Controller.V1
{
    public class VolunteerApplicationDetailController : ApiController
    {
        public VolunteerApplicationDetailController(ISender sender) : base(sender)
        {
        }

        [Authorize]
        [HttpPost("create_volunteer_application", Name = "CreateVolunteerApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateVolunteerApplication([FromBody] Command.FormRegisterVolunteerCommand form)
        {
            var userId = User.FindFirstValue("UserId");

            var result = await Sender.Send(new Command.CreateVolunteerApplicationDetailCommand(form,Guid.Parse(userId)));

            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpPut("approve_volunteer_application", Name = "ApproveVolunteerApplicationCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveVolunteerApplication([FromBody] Command.ApproveVolunteerApplicationCommand id)
        {
            var result = await Sender.Send(id);
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }

        [HttpPut("reject_volunteer_application", Name = "RejectVolunteerApplicationCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RejectVolunteerApplication([FromBody] Command.RejectVolunteerApplicationCommand reject)
        {
            var result = await Sender.Send(reject);
            if (result.IsFailure)
                return HandlerFailure(result);
            return Ok(result);
        }
    }
}