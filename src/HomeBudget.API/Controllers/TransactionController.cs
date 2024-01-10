using HomeBudget.API.CQRS.Command.Settlement;
using HomeBudget.API.CQRS.Command.Transaction;
using HomeBudget.API.CQRS.Events;
using HomeBudget.API.CQRS.Query.Settlement;
using HomeBudget.API.CQRS.Query.Transaction;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.Settlement;
using HomeBudget.API.Models.Transaction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    /// <summary>
    /// Handle transaction-related operations
    /// </summary>
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TransactionController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <param name="searchQuery">Optional: Search filter</param>
        /// <param name="month">Month filter</param>
        /// <param name="year">Year filter</param>
        /// <returns>A list of transactions</returns>
        [HttpGet("transaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(
            string? searchQuery,
            int? month,
            int? year)
        {
            var query = new GetTransactionsQuery(searchQuery, month, year);
            var transactionEntities = await _mediator.Send(query);

            return Ok(transactionEntities);
        }

        /// <summary>
        /// Get a transaction by id
        /// </summary>
        /// <param name="transactionId">The id of the transaction to get</param>
        /// <returns>A transaction when it exists</returns>
        [HttpGet("transaction/{transactionId}", Name = "GetTransaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionDto>> GetTransactionAsync(int transactionId)
        {
            var query = new GetTransactionQuery(transactionId);
            var transaction = await _mediator.Send(query);

            if (transaction is null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        /// <param name="transaction">Transaction to create</param>
        /// <returns>The created transaction and the corresponding route</returns>
        [HttpPost("transaction")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionDto>> CreateTransactionAsync(TransactionForCreationDto transaction)
        {
            var command = new CreateTransactionCommand(transaction);
            var createdTransaction = await _mediator.Send(command);

            if (createdTransaction is null)
            {
                return BadRequest();
            }

            return CreatedAtRoute(
                "GetTransaction",
                new { transactionId = createdTransaction.Id },
                createdTransaction);
        }

        /// <summary>
        /// Update a transaction
        /// </summary>
        /// <param name="transactionId">The id of the transaction to update</param>
        /// <param name="transaction">The updated transaction</param>
        /// <returns>No Content</returns>
        [HttpPut("transaction/{transactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTransactionAsync(int transactionId, TransactionDto transaction)
        {
            var command = new UpdateTransactionCommand(transaction);
            var @event = await _mediator.Send(command);

            if (@event is TransactionNotFoundEvent)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a transaction
        /// </summary>
        /// <param name="transactionId">The id of the transaction to delete</param>
        /// <returns>No Content</returns>
        [HttpDelete("transaction/{transactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTransaction(int transactionId)
        {
            var command = new DeleteTransactionCommand(transactionId);
            var @event = await _mediator.Send(command);

            if (@event is TransactionNotFoundEvent)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Get a list of settlements, calculated for each user and the unsettled transactions
        /// </summary>
        /// <returns>A list of settlements</returns>
        [HttpGet("transaction/settlement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SettlementDto>>> GetSettlementAsync()
        {
            var query = new GetSettlementsQuery();
            var settlements = await _mediator.Send(query);

            return Ok(settlements);
        }

        /// <summary>
        /// Updates all transactions to be settled by the transaction IDs passed.
        /// </summary>
        /// <param name="transactionIds">List of IDs of transactions to be settled</param>
        /// <returns>No Content</returns>
        [HttpPut("transaction/settlement")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SettleTransactions([FromBody] List<int> transactionIds)
        {
            var command = new SettleTransactionsCommand(transactionIds);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("transaction/statistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TransactionStatisticsDto>>> GetTransactionStatisticsAsync([FromQuery]int year)
        {
            var query = new GetTransactionStatisticsQuery(year);
            var statistics = await _mediator.Send(query);

            return Ok(statistics);
        }
    }
}