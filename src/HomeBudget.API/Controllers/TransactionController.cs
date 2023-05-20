using System.Text.Json;
using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    /// <summary>
    /// Handle transaction-related operations
    /// </summary>
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        const int MaxTransactionPageSize = 100;

        public TransactionController(
            ITransactionRepository transactionRepository,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this._transactionRepository = transactionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <param name="searchQuery">Optional: Search filter</param>
        /// <param name="month">Month filter</param>
        /// <param name="year">Year filter</param>
        /// <param name="pageNumber">Page of the results to return</param>
        /// <param name="pageSize">Amount of results to return</param>
        /// <returns>A list of transactions</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(
            string? searchQuery,
            int? month,
            int? year,
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (pageSize > MaxTransactionPageSize)
            {
                pageSize = MaxTransactionPageSize;
            }

            var (transactionEntities, paginationMetadata) =
                await _transactionRepository.GetTransactionsAsync(searchQuery, month, year, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<TransactionDto>>(transactionEntities));
        }

        /// <summary>
        /// Get a transaction by id
        /// </summary>
        /// <param name="transactionId">The id of the transaction to get</param>
        /// <returns>A transaction when it exists</returns>
        [HttpGet("{transactionId}", Name = "GetTransaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionDto>> GetTransactionAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            return Ok(_mapper.Map<TransactionDto>(transaction));
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        /// <param name="transaction">Transaction to create</param>
        /// <returns>The created transaction and the corresponding route</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionDto>> CreateTransactionAsync(TransactionForCreationDto transaction)
        {
            var user = await _userRepository.GetUserByIdAsync(transaction.UserId);
            
            if (user == null)
            {
                return BadRequest();
            }

            var transactionToAdd = _mapper.Map<Transaction>(transaction);
            transactionToAdd.User = user;

            await _transactionRepository.AddTransactionAsync(transactionToAdd);

            var createdTransaction = _mapper.Map<TransactionDto>(transactionToAdd);
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
        [HttpPut("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTransactionAsync(int transactionId, TransactionForUpdateDto transaction)
        {
            var transactionEntity = await _transactionRepository.GetTransactionByIdAsync(transactionId);

            if (transactionEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(transaction, transactionEntity);

            await _transactionRepository.SaveChangedAsync();

            return NoContent();
        }

        /// <summary>
        /// Partially update a transaction
        /// </summary>
        /// <param name="transactionId">The id of the transaction to update</param>
        /// <param name="patchDocument">The patch document describing the update</param>
        /// <returns>No Content</returns>
        [HttpPatch("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PartialUpdateTransactionAsync(
            int transactionId,
            JsonPatchDocument<TransactionForUpdateDto> patchDocument)
        {
            var transactionEntity = await _transactionRepository.GetTransactionByIdAsync(transactionId);

            if (transactionEntity == null)
            {
                return NotFound();
            }

            var transactionToPatch = _mapper.Map<TransactionForUpdateDto>(transactionEntity);

            patchDocument.ApplyTo(transactionToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(transactionToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(transactionToPatch, transactionEntity);
            await _transactionRepository.SaveChangedAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a transaction
        /// </summary>
        /// <param name="transactionId">The id of the transaction to delete</param>
        /// <returns>No Content</returns>
        [HttpDelete("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTransaction(int transactionId)
        {
            var transactionEntity = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transactionEntity == null)
            {
                return NotFound();
            }

            await _transactionRepository.DeleteTransactionAsync(transactionEntity);

            return NoContent();
        }
    }
}