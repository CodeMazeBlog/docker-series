using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AccountOwnerServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private ILoggerManager _logger;
		private IRepositoryWrapper _repository;
		private IMapper _mapper;

		public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
		{
			_logger = logger;
			_repository = repository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAllAccounts()
		{
			try
			{
				var accounts = _repository.Account.GetAllAccounts();

				_logger.LogInfo($"Returned all accounts from database.");

				var accountDtos = _mapper.Map<IEnumerable<OwnerDto>>(accounts);
				return Ok(accountDtos);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside GetAllAccounts action: {ex}");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost]
		public IActionResult CreateAccount([FromBody]AccountForCreationDto account)
		{
			try
			{
				if (account == null)
				{
					_logger.LogError("Object sent from client is null.");
					return BadRequest("Object is null");
				}

				if (!ModelState.IsValid)
				{
					_logger.LogError("Invalid object sent from client.");
					return BadRequest("Invalid model object");
				}

				var accountEntity = _mapper.Map<Account>(account);

				_repository.Account.CreateAccount(accountEntity);
				_repository.Save();

				var createdAccount = _mapper.Map<AccountDto>(accountEntity);

				return CreatedAtRoute("AccountById", new { id = createdAccount.Id }, createdAccount);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Something went wrong inside CreateAccount action: {ex}");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
