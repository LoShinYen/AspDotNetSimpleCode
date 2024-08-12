using WebAPISimpleCode.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace WebAPISimpleCode.Repository
{
    public class AuthRepository
    {
        private readonly TutorialDbContext _context;

        public AuthRepository(TutorialDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 創建帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> CreateAccountAsync(Account account)
        {
            // 檢查是否已存在相同的帳號
            if (_context.Accounts.Any(a => a.Username == account.Username))
            {
                return false;
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return true; // 帳號創建成功
        }

        /// <summary>
        /// 根據用戶名查詢帳號
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Account?> GetAccountByUsernameAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Username == username);
        }

        /// <summary>
        /// 更新帳號資訊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return false;
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
