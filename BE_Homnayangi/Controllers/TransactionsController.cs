using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BE_Homnayangi.Modules.TransactionModule.Interface;
//using BE_Homnayangi.Modules.TransactionModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using BE_Homnayangi.Modules.VoucherModule.Response;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_Homnayangi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _TransactionService;
        private readonly IUserService _userService;

        public TransactionsController(IMapper mapper, ITransactionService TransactionService, IUserService userService)
        {
            _mapper = mapper;
            _TransactionService = TransactionService;
            _userService = userService;
        }

        #region Get all Transactions for staff include deleted, without paging
        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            try
            {
                var res = await _TransactionService.GetAll();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        // POST: api/Transactions
        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] CreateTransactionRequest request)
        //{
        //    try
        //    {
        //        Transaction Transaction = _mapper.Map<Transaction>(request);
        //        Transaction.CustomerId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;

        //        await _TransactionService.AddNewTransaction(Transaction);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpPut("complete/{id}")]
        public async Task<ActionResult> CompleteTransaction([FromRoute] Guid id)
        {
            try
            {
                await _TransactionService.CompleteTransaction(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deny/{id}")]
        public async Task<ActionResult> DenyTransaction([FromRoute] Guid id)
        {
            try
            {
                await _TransactionService.DenyTransaction(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
