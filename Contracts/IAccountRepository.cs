using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
	public interface IAccountRepository : IRepositoryBase<Account>
	{
		IEnumerable<Account> GetAllAccounts();
		void CreateAccount(Account account);
	}
}
