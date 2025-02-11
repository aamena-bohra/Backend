using BlueHarvestAPI;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace BlueHarvest.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Account), 200)]
        public async Task<IActionResult> OpenCurrentAccount([FromBody] AccountRequest request)
        {
            try
            {
                Account account = await _accountService.OpenAccountAsync(request.CustomerId, request.InitialDeposit, AccountType.Current);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    
}
