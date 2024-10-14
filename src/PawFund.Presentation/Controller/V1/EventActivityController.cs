﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.EventActivity;
using PawFund.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Presentation.Controller.V1
{
    public class EventActivityController : ApiController
    {
        public EventActivityController(ISender sender) : base(sender)
        {
        }

        [HttpPost("create_event_activity", Name = "CreateEventActivity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEvents([FromBody] Command.CreateEventActivityCommand createEventActivityCommand)
        {
            var result = await Sender.Send(createEventActivityCommand);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("get_event_activity_by_id", Name = "GetEventActivityById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventById([FromQuery] Guid Id)
        {
            var result = await Sender.Send(new Query.GetEventActivityByIdQuery(Id));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpPut(Name = "UpdateEventActivityById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEventById([FromBody] Command.UpdateEventActivityCommand updateEventActivityCommand)
        {
            var result = await Sender.Send(updateEventActivityCommand);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpDelete(Name = "DeleteEventActivityById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventById([FromBody] Command.DeleteEventActivityCommand Id)
        {
            var result = await Sender.Send(Id);
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet(Name = "GetAllEventActivity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEvent([FromQuery] Guid id)
        {
            var result = await Sender.Send(new Query.GetAllEventActivity(id));
            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

    }
}