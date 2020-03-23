using DealDouble.Database;
using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DealDouble.Services
{
    public class CommentServices
    {

        #region Singletone
        public static CommentServices Instance
        {
            get
            {
                if (instance == null) instance = new CommentServices();
                return instance;
            }
        }
        private static CommentServices instance { get; set; }
        #endregion
       
        public List<Comment> GetAllComments(string searchTearm, int pageNo,int pageSize)
        {
            using (var contaxt = new Context())
            {
                var comments = contaxt.Comments.ToList();
                if (!string.IsNullOrEmpty(searchTearm))
                {
                    comments = comments.Where(x => x.Text.ToLower().ToString().Contains(searchTearm.ToLower())).ToList();
                }
                return comments.OrderByDescending(x => x.TimeStamp).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int GetTotalComments(string searchTearm)
        {
            using (var context = new Context())
            {
                return context.Comments.Count();
            }
        }
        public bool DeleteComment(int id)
        {
            using (var context = new Context())
            {
                var comment = context.Comments.Find(id);
                context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                context.Comments.Remove(comment);
                return context.SaveChanges()>0;
            }
        }
    }
}
