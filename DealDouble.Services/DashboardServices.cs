using DealDouble.Database;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Services
{
    public class DashboardServices
    {
        #region Singletone
        public static DashboardServices Instance
        {
            get
            {
                if (instance == null) instance = new DashboardServices();
                return instance;
            }
        }
        private static DashboardServices instance { get; set; }
        #endregion

        public int GetUserCount()
        {
            using (var contaxt = new Context())
            {
                return contaxt.Users.Count();
            }
        }
        public int GetAuctionCount()
        {
            using (var contaxt = new Context())
            {
                return contaxt.Auctions.Count();
            }
        }
        public int GetBidCount()
        {
            using (var contaxt = new Context())
            {
                return contaxt.Bids.Count();
            }
        }

        public List<Comment> GetCommentByUser(string userId, int entityId)
        {
            using (var contaxt = new Context())
            {
                return contaxt.Comments.Where(x => x.UserId == userId).Where(x=>x.EntityID == entityId).OrderByDescending(x => x.TimeStamp).ToList();
            }
        }



    }
}
