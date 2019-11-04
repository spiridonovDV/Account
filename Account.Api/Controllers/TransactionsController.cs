using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Account.Api.Models.Values;
using Account.Repository;


namespace Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository db;

        public TransactionsController(IRepository repository)
        {
            db = repository;
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateTransaction([FromBody] CreateTransaction model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var transaction = db.CreateTransaction(model.UserFromId, model.UserToId, model.Amount);
                return Ok(transaction.Id);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
