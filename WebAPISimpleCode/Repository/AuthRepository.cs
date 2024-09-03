using Microsoft.EntityFrameworkCore;

namespace WebAPISimpleCode.Repository
{
    public class AuthRepository(TutorialDbContext context)
    {
        /// <summary>
        /// 創建帳號
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> CreateAccountAsync(Account account)
        {
            // 檢查是否已存在相同的帳號
            if (context.Accounts.Any(a => a.Username == account.Username))
            {
                return false;
            }

            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            return true; // 帳號創建成功
        }

        /// <summary>
        /// 根據用戶名查詢帳號
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Account?> GetAccountByUsernameAsync(int accountId)
        {
            return await context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        /// <summary>
        /// 更新帳號資訊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAccountAsync(Account account)
        {
            context.Accounts.Update(account);
            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            var account = await context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return false;
            }

            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
